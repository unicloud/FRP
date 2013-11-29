#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，21:11
// 文件名：Manufacturer.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ManufacturerAgg
{
    /// <summary>
    ///     制造商聚合根
    /// </summary>
    public class Manufacturer : EntityGuid
    {
        #region 属性

        /// <summary>
        ///     中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     公司英文名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        ///     公司中文简称
        /// </summary>
        public string CnShortName { get; set; }

        /// <summary>
        ///     公司英文简称
        /// </summary>
        public string EnShortName { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}