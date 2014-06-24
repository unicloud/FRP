#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/21，20:35
// 方案：FRP
// 项目：Application
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;

#endregion

namespace UniCloud.Application
{
    public class DataHelper
    {
        /// <summary>
        ///     从表数据处理
        ///     主键为int类型
        /// </summary>
        /// <typeparam name="TCurrent">DTO实体类型</typeparam>
        /// <typeparam name="TPersist">领域模型实体类型</typeparam>
        /// <param name="current">DTO实体集合</param>
        /// <param name="persist">持久化实体集合</param>
        /// <param name="currentKey">DTO实体键表达式</param>
        /// <param name="persistKey">持久化实体键表达式</param>
        /// <param name="insert">插入操作</param>
        /// <param name="update">更新操作</param>
        /// <param name="delete">删除操作</param>
        public static void DetailHandle<TCurrent, TPersist>(TCurrent[] current, TPersist[] persist,
            Func<TCurrent, int> currentKey, Func<TPersist, int> persistKey, Action<TCurrent> insert,
            Action<TCurrent, TPersist> update, Action<TPersist> delete)
        {
            //生成主键数组
            var currentKeys = current.Select(currentKey).ToArray();
            var persistKeys = persist.Select(persistKey).ToArray();

            //生成更新、插入、删除的主键数组
            var updateKeys = currentKeys.Intersect(persistKeys).ToArray();
            var insertKeys = currentKeys.Except(updateKeys).ToArray();
            var deleteKeys = persistKeys.Except(updateKeys).ToArray();

            //生成插入、更新、删除集合
            var inserts = current.Join(insertKeys, currentKey, r => r, (l, r) => l).ToList();
            var updates = current.Join(persist, currentKey, persistKey, Tuple.Create).ToList();
            var deletes = persist.Join(deleteKeys, persistKey, r => r, (l, r) => l).ToList();

            //执行插入、更新、删除操作
            inserts.ForEach(insert);
            updates.ForEach(u => update(u.Item1, u.Item2));
            deletes.ForEach(delete);
        }

        /// <summary>
        ///     从表数据处理
        ///     主键为Guid类型
        /// </summary>
        /// <typeparam name="TCurrent">DTO实体类型</typeparam>
        /// <typeparam name="TPersist">领域模型实体类型</typeparam>
        /// <param name="current">DTO实体集合</param>
        /// <param name="persist">持久化实体集合</param>
        /// <param name="currentKey">DTO实体键表达式</param>
        /// <param name="persistKey">持久化实体键表达式</param>
        /// <param name="insert">插入操作</param>
        /// <param name="update">更新操作</param>
        /// <param name="delete">删除操作</param>
        public static void DetailHandle<TCurrent, TPersist>(TCurrent[] current, TPersist[] persist,
            Func<TCurrent, Guid> currentKey, Func<TPersist, Guid> persistKey, Action<TCurrent> insert,
            Action<TCurrent, TPersist> update, Action<TPersist> delete)
        {
            //生成主键数组
            var currentKeys = current.Select(currentKey).ToArray();
            var persistKeys = persist.Select(persistKey).ToArray();

            //生成更新、插入、删除的主键数组
            var updateKeys = currentKeys.Intersect(persistKeys).ToArray();
            var insertKeys = currentKeys.Except(updateKeys).ToArray();
            var deleteKeys = persistKeys.Except(updateKeys).ToArray();

            //生成插入、更新、删除集合
            var inserts = current.Join(insertKeys, currentKey, r => r, (l, r) => l).ToList();
            var updates = current.Join(persist, currentKey, persistKey, Tuple.Create).ToList();
            var deletes = persist.Join(deleteKeys, persistKey, r => r, (l, r) => l).ToList();

            //执行插入、更新、删除操作
            inserts.ForEach(insert);
            updates.ForEach(u => update(u.Item1, u.Item2));
            deletes.ForEach(delete);
        }

    }
}