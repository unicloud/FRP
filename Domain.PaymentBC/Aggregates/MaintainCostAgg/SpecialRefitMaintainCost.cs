#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 9:35:56
// 文件名：SpecialRefitMaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 9:35:56
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间



#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg
{
    /// <summary>
    /// 特修改装
    /// </summary>
    public class SpecialRefitMaintainCost: MaintainCost
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SpecialRefitMaintainCost()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 项目
        /// </summary>
        public string Project { get; internal set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Info { get; internal set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; internal set; }
        #endregion

        #region 外键属性
       
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        #endregion
    }
}

