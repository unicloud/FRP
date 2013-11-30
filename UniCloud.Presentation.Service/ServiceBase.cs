#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/15，11:11
// 方案：FRP
// 项目：Service
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Data;

#endregion

namespace UniCloud.Presentation.Service
{
    /// <summary>
    ///     服务基类
    ///     实现IService接口。
    /// </summary>
    public abstract class ServiceBase : IService
    {
        private readonly DataServiceContext _context;

        private readonly List<QueryableDataServiceCollectionViewBase> _dataServiceCollectionViews;

        protected ServiceBase(DataServiceContext context)
        {
            _context = context;
            _dataServiceCollectionViews = new List<QueryableDataServiceCollectionViewBase>();
            _context.MergeOption = MergeOption.OverwriteChanges;
        }

        /// <summary>
        ///     域上下文对象
        /// </summary>
        public DataServiceContext Context
        {
            get { return _context; }
        }

        /// <summary>
        ///     是否有变化
        /// </summary>
        public bool HasChanges { get; private set; }

        /// <summary>
        ///     属性变更事件
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        /// <summary>
        ///     DataServiceCollectionView集合
        /// </summary>
        public List<QueryableDataServiceCollectionViewBase> DataServiceCollectionViews
        {
            get { return _dataServiceCollectionViews; }
        }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        public void SubmitChanges()
        {
            SubmitChanges(SaveChangesOptions.Batch, p => { });
        }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="callback">回调</param>
        /// <param name="state">状态</param>
        public void SubmitChanges(Action<SubmitChangesResult> callback, object state = null)
        {
            SubmitChanges(SaveChangesOptions.Batch, callback, state);
        }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="saveChangesOptions">保存方式</param>
        /// <param name="callback">回调</param>
        /// <param name="state">状态</param>
        public void SubmitChanges(SaveChangesOptions saveChangesOptions, Action<SubmitChangesResult> callback,
            object state = null)
        {
            _context.BeginSaveChanges(saveChangesOptions, p =>
            {
                var result = new SubmitChangesResult();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        DataServiceResponse response = _context.EndSaveChanges(p);
                        foreach (OperationResponse changeResponse in response)
                        {
                            result.Headers = changeResponse.Headers;
                            result.Error = changeResponse.Error;
                            result.StatusCode = changeResponse.StatusCode;
                        }
                        _dataServiceCollectionViews.ForEach(c => c.RejectChanges());
                    }
                    catch (Exception ex)
                    {
                        result.Error = ex;
                    }
                    finally
                    {
                        callback(result);
                        HasChanges = result.Error != null;
                        OnPropertyChanged("HasChanges");
                    }
                });
            }, Context);
            //_dataServiceCollectionViews.ForEach(p =>
            //{
            //                result.Headers = changeResponse.Headers;
            //                result.Error = changeResponse.Error;
            //                result.StatusCode = changeResponse.StatusCode;
            //            }
            //            _dataServiceCollectionViews.ForEach(c => c.Refresh());
            //        }
            //        catch (Exception ex)
            //    {
            //            result.Error = ex;
            //        }
            //        finally
            //            {
            //                callback(result);
            //            HasChanges = p.HasChanges;
            //            OnPropertyChanged("HasChanges");
            //        };
            //    }
            //});
            //}, Context);
            //_dataServiceCollectionViews.ForEach(p =>
            //{
            //    if (p.HasChanges)
            //    {
            //        var result = new SubmitChangesResult();
            //        p.SubmitChanges();
            //        p.SubmittedChanges += (o, e) => Deployment.Current.Dispatcher.BeginInvoke(() =>
            //            {
            //                result.Error = e.Error;
            //                callback(result);
            //            });
            //    }
            //});
        }

        /// <summary>
        ///     撤销改变
        /// </summary>
        public void RejectChanges()
        {
            _dataServiceCollectionViews.ForEach(p =>
            {
                if (p.HasChanges)
                {
                    p.RejectChanges();
                }
            });
            HasChanges = false;
            OnPropertyChanged("HasChanges");
        }

        /// <summary>
        ///     创建数据集合
        /// </summary>
        /// <typeparam name="TService">实体类型</typeparam>
        /// <param name="query">查询</param>
        /// <returns>数据集合</returns>
        public QueryableDataServiceCollectionView<TService> CreateCollection<TService>(DataServiceQuery<TService> query)
            where TService : class, INotifyPropertyChanged
        {
            var result = new QueryableDataServiceCollectionView<TService>(_context, query);
            return result;
        }

        /// <summary>
        ///     注册服务集合
        /// </summary>
        /// <typeparam name="TService">实体类型</typeparam>
        /// <param name="collectionView">对象</param>
        public void RegisterCollectionView<TService>(QueryableDataServiceCollectionView<TService> collectionView)
            where TService : class, INotifyPropertyChanged
        {
            if (!_dataServiceCollectionViews.Any(p => p.Equals(collectionView)))
            {
                _dataServiceCollectionViews.Add(collectionView);
                collectionView.PropertyChanged += OnCollectionViewPropertyChanged;
            }
        }

        /// <summary>
        ///     发布属性变更事件
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     发布属性变更事件
        /// </summary>
        /// <param name="e">属性变更事件参数</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, e);
        }

        /// <summary>
        ///     数据集合属性变更事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = sender as QueryableDataServiceCollectionViewBase;
            if (obj != null && (e.PropertyName == "HasChanges" && obj.HasChanges))
            {
                HasChanges = true;
                OnPropertyChanged("HasChanges");
            }
        }
    }
}