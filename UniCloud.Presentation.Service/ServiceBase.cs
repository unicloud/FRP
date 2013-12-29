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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private bool _hasChanges;
        private EventHandler<DataServiceSubmittedChangesEventArgs> _submitChanges;

        protected ServiceBase(DataServiceContext context)
        {
            _context = context;
            _dataServiceCollectionViews = new List<QueryableDataServiceCollectionViewBase>();
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasChanges = true;
        }

        #region IService 成员

        #region 属性

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
        public bool HasChanges
        {
            get { return _hasChanges; }
            protected set
            {
                if (_hasChanges == value)
                    return;
                _hasChanges = value;
                OnPropertyChanged("HasChanges");
            }
        }

        /// <summary>
        ///     DataServiceCollectionView集合
        /// </summary>
        public List<QueryableDataServiceCollectionViewBase> DataServiceCollectionViews
        {
            get { return _dataServiceCollectionViews; }
        }

        #endregion

        #region 事件

        /// <summary>
        ///     属性变化事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region 操作

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        public void SubmitChanges(QueryableDataServiceCollectionViewBase collectionView)
        {
            SubmitChanges(collectionView, p => { });
        }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        /// <param name="callback">回调</param>
        /// <param name="state">状态</param>
        public void SubmitChanges(QueryableDataServiceCollectionViewBase collectionView,
            Action<SubmitChangesResult> callback, object state = null)
        {
            var result = new SubmitChangesResult();
            collectionView.SubmitChanges();
            if (_submitChanges == null)
            {
                _submitChanges += (o, e) =>
                {
                    result.Error = e.Error;
                    callback(result);
                };
                collectionView.SubmittedChanges += _submitChanges;
            }
        }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="callback">回调</param>
        /// <param name="saveChangesOptions">保存方式</param>
        /// <param name="state">状态</param>
        public void SubmitChanges(Action<SubmitChangesResult> callback, object state = null,
            SaveChangesOptions saveChangesOptions = SaveChangesOptions.Batch)
        {
            _context.BeginSaveChanges(saveChangesOptions, p =>
            {
                var result = new SubmitChangesResult();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        var response = _context.EndSaveChanges(p);
                        foreach (var changeResponse in response)
                        {
                            result.Headers = changeResponse.Headers;
                            result.Error = changeResponse.Error;
                            result.StatusCode = changeResponse.StatusCode;
                        }
                        _dataServiceCollectionViews.ForEach(SubmitChanges);
                    }
                    catch (Exception ex)
                    {
                        result.Error = ex;
                    }
                    finally
                    {
                        callback(result);
                    }
                });
            }, Context);
        }

        /// <summary>
        ///     撤销改变
        /// </summary>
        public void RejectChanges()
        {
            _dataServiceCollectionViews.ForEach(RejectChanges);
        }

        /// <summary>
        ///     撤销改变
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        public void RejectChanges(QueryableDataServiceCollectionViewBase collectionView)
        {
            collectionView.RejectChanges();
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
            }
        }

        /// <summary>
        ///     创建数据集合
        /// </summary>
        /// <typeparam name="TService">实体类型</typeparam>
        /// <param name="query">查询</param>
        /// <param name="add">添加项的处理</param>
        /// <param name="remove">移除项的处理</param>
        /// <param name="options">保存选项</param>
        /// <returns>数据集合</returns>
        public QueryableDataServiceCollectionView<TService> CreateCollection<TService>(
            DataServiceQuery<TService> query,
            Action<IList, NotifyCollectionChangedEventHandler> add = null,
            Action<IList, NotifyCollectionChangedEventHandler> remove = null,
            SaveChangesOptions options = SaveChangesOptions.Batch)
            where TService : class, INotifyPropertyChanged
        {
            var result = new QueryableDataServiceCollectionView<TService>(_context, query);
            result.SubmittingChanges += (o, e) => { e.SaveChangesOptions = options; };
            result.LoadedData += (o, e) => { HasChanges = false; };
            result.PropertyChanged += (o, e) => { HasChanges = result.HasChanges; };
            result.CollectionChanged += (o, e) =>
            {
                if (e.NewItems != null && add != null)
                {
                    add(e.NewItems, OnViewCollectionChanged);
                }
                if (e.OldItems != null && remove != null)
                {
                    remove(e.OldItems, OnViewCollectionChanged);
                }
            };

            return result;
        }

        #endregion

        #endregion
    }
}