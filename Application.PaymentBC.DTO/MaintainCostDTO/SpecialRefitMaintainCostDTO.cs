#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 9:46:35
// 文件名：SpecialRefitMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 9:46:35
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 特修改装维修成本
    /// </summary>
    [DataServiceKey("Id")]
    public class SpecialRefitMaintainCostDTO : MaintainCostDTO
    {
        #region 属性
        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public string Project { get;  set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Info { get;  set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get;  set; }
        #endregion

        #region 外键属性
        #endregion

    }
}
