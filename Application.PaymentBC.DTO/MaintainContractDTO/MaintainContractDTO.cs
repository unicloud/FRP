#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/27，13:12
// 文件名：MaintainContractDTO.cs
// 程序集：UniCloud.Application.PaymentBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


using System;
using System.Data.Services.Common;

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 维修合同基类
    /// </summary>
       [DataServiceKey("MaintainContractId")]

    public class MaintainContractDTO
    {
        public int MaintainContractId { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 签约对象
        /// </summary>
        public string Signatory { get; set; }

        /// <summary>
        /// 签约对象ID
        /// </summary>
        public int SignatoryId { get; set; }

    }
}
