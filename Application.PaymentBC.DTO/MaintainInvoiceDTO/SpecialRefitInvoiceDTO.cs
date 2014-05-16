#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 9:51:31
// 文件名：SpecialRefitInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 9:51:31
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     特修改装DTO
    /// </summary>
    [DataServiceKey("SpecialRefitId")]
    public class SpecialRefitInvoiceDTO : MaintainInvoiceDTO
    {
        #region 私有字段


        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int SpecialRefitId { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性



        #endregion
    }
}
