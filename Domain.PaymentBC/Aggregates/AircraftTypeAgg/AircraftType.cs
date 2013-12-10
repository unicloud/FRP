#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，15:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.AircraftTypeAgg
{
    /// <summary>
    ///     机型聚合根
    /// </summary>
    public class AircraftType : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftType()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}