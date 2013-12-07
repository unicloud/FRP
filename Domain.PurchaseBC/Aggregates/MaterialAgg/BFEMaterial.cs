#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Domain.PurchaseBC.Aggregates.PartAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg
{
    /// <summary>
    ///     采购物料聚合根
    ///     BFE物料
    /// </summary>
    public class BFEMaterial : Material
    {
                #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BFEMaterial()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件ID
        /// </summary>
        public int PartID { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     附件
        /// </summary>
        public virtual Part Part { get; set; }

        #endregion

        #region 操作

        #endregion
    }
}