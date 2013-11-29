#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.PartAgg
{
    /// <summary>
    ///     附件聚合根
    /// </summary>
    public class Part : EntityInt
    {
        #region 属性

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}