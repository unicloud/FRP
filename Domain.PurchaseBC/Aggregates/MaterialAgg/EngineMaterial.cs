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

namespace UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg
{
    /// <summary>
    ///     采购物料聚合根
    ///     发动机物料
    /// </summary>
    public class EngineMaterial : Material
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineMaterial()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     物料件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        ///     发动机目录价
        /// </summary>
        public decimal ListPrice { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置附件件号
        /// </summary>
        /// <param name="pn">附件件号</param>
        public void SetPart(string pn)
        {
            //if (string.IsNullOrWhiteSpace(pn))
            //{
            //    throw new ArgumentException("物料件号参数为空！");
            //}

            Pn = pn;
        }

        #endregion
    }
}