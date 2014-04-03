#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:16:15
// 文件名：Supplier
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg
{
    /// <summary>
    ///     供应商（所有权人）聚合根
    /// </summary>
    public class Supplier : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Supplier()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     供应商类型
        ///     <remarks>
        ///         国外、国内
        ///     </remarks>
        /// </summary>
        public SupplierType SupplierType { get; protected set; }

        /// <summary>
        ///     组织机构代码
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        ///     供应商中文名称
        /// </summary>
        public string CnName { get; protected set; }

        /// <summary>
        ///     供应商英文名称
        /// </summary>
        public string EnName { get; protected set; }

        /// <summary>
        ///     供应商中文简称
        /// </summary>
        public string CnShortName { get; protected set; }

        /// <summary>
        ///     供应商英文简称
        /// </summary>
        public string EnShortName { get; protected set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        ///     更改日期
        /// </summary>
        public DateTime UpdateDate { get; protected set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; protected set; }

        /// <summary>
        ///     航空公司Guid
        /// </summary>
        public Guid? AirlineGuid { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}