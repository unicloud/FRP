#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/12 11:07:54
// 文件名：EngineMaintainContractDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 发动机维修合同
    /// </summary>
   [DataServiceKey("EngineMaintainContractId")]
    public class EngineMaintainContractDTO : MaintainContractDTO
    {
        /// <summary>
        /// 发动机维修合同主键
        /// </summary>
       public int EngineMaintainContractId { get; set; }

       /// <summary>
       /// 费率
       /// </summary>
       public string FeeRate { get; set; }

       /// <summary>
       /// 单位
       /// </summary>
       public string Units { get; set; }

       /// <summary>
       /// 费率类别
       /// </summary>
       public string FeeType { get; set; }

       /// <summary>
       /// 适用
       /// </summary>
       public string Fit { get; set; }

       /// <summary>
       /// 适用年份
       /// </summary>
       public string FitYear { get; set; }
    }
}
