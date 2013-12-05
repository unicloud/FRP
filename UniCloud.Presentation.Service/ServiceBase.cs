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
        private EventHandler<DataServiceSubmittedChangesEventArgs> _submitChanges;

        protected ServiceBase(DataServiceContext context)
        {
            _context = context;
            _dataServiceCollectionViews = new List<QueryableDataServiceCollectionViewBase>();
            _context.MergeOption = MergeOption.NoTracking;
        }

        /// <summary>
        ///     域上下文对象
        /// </summary>
        public DataServiceContext Context
        {
            get { return _context; }
        }

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
        /// <param name="collectionView">数据集合</param>
        public void SubmitChanges(QueryableDataServiceCollectionViewBase collectionView)
        {
            SubmitChanges(collectionView, SaveChangesOptions.Batch, p => { });
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
            SubmitChanges(collectionView, SaveChangesOptions.Batch, callback, state);
        }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        /// <param name="saveChangesOptions">保存方式</param>
        /// <param name="callback">回调</param>
        /// <param name="state">状态</param>
        public void SubmitChanges(QueryableDataServiceCollectionViewBase collectionView,
            SaveChangesOptions saveChangesOptions, Action<SubmitChangesResult> callback,
            object state = null)
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
        ///     撤销改变
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        public void RejectChanges(QueryableDataServiceCollectionViewBase collectionView)
        {
            collectionView.RejectChanges();
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
            }
        }
    }
}