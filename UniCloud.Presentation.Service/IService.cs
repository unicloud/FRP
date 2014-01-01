#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/15，10:11
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
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Data;

#endregion

namespace UniCloud.Presentation.Service
{
    /// <summary>
    ///     服务接口
    ///     用于域上下文处理。
    /// </summary>
    public interface IService : INotifyPropertyChanged
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        DataServiceContext Context { get; }

        /// <summary>
        ///     是否有变化
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        ///     DataServiceCollectionView集合
        /// </summary>
        List<QueryableDataServiceCollectionViewBase> DataServiceCollectionViews { get; }

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        void SubmitChanges(QueryableDataServiceCollectionViewBase collectionView);

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        /// <param name="callback">回调</param>
        /// <param name="state">状态</param>
        void SubmitChanges(QueryableDataServiceCollectionViewBase collectionView, Action<SubmitChangesResult> callback,
            object state = null);

        /// <summary>
        ///     保存实体变化
        /// </summary>
        /// <param name="callback">回调</param>
        /// <param name="state">状态</param>
        /// <param name="saveChangesOptions">保存方式</param>
        void SubmitChanges(Action<SubmitChangesResult> callback, object state = null,
            SaveChangesOptions saveChangesOptions = SaveChangesOptions.Batch);

        /// <summary>
        ///     撤销改变
        /// </summary>
        void RejectChanges();

        /// <summary>
        ///     撤销改变
        /// </summary>
        /// <param name="collectionView">数据集合</param>
        void RejectChanges(QueryableDataServiceCollectionViewBase collectionView);

        /// <summary>
        ///     创建数据集合
        /// </summary>
        /// <typeparam name="TService">实体类型</typeparam>
        /// <param name="query">查询</param>
        /// <param name="changed">变更的处理</param>
        /// <returns>数据集合</returns>
        QueryableDataServiceCollectionView<TService> CreateCollection<TService>(
            DataServiceQuery<TService> query,
            params Func<TService, object>[] changed)
            where TService : class, INotifyPropertyChanged;

        /// <summary>
        ///     创建数据集合
        /// </summary>
        /// <typeparam name="TService">实体类型</typeparam>
        /// <param name="query">查询</param>
        /// <param name="options">保存选项</param>
        /// <param name="changed">变更的处理</param>
        /// <returns>数据集合</returns>
        QueryableDataServiceCollectionView<TService> CreateCollection<TService>(
            DataServiceQuery<TService> query,
            SaveChangesOptions options,
            params Func<TService, object>[] changed)
            where TService : class, INotifyPropertyChanged;

        /// <summary>
        ///     注册数据集合
        /// </summary>
        /// <typeparam name="TService">实体类型</typeparam>
        /// <param name="collectionView">对象</param>
        void RegisterCollectionView<TService>(QueryableDataServiceCollectionView<TService> collectionView)
            where TService : class, INotifyPropertyChanged;
    }
}