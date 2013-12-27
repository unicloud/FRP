#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:18:42
// 文件名：Manager
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg
{
    /// <summary>
    ///     管理者聚合根
    /// </summary>
    public class Manager : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Manager()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 管理单位名称
        /// </summary>
        public string Name { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        /// Owner外键
        /// </summary>
        public Guid OwnerID { get; private set; }

        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
