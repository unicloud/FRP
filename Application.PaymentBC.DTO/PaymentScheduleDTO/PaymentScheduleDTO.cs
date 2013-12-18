#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/18 9:27:49
// 文件名：PaymentScheduleDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 发动机付款计划
    /// </summary>
    [DataServiceKey("PaymentScheduleId")]
    public class PaymentScheduleDTO
    {
        public PaymentScheduleDTO()
        {
            PaymentScheduleLines = new List<PaymentScheduleLineDTO>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int PaymentScheduleId { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///   物料名称
        /// </summary>
        //public string MaterialName { get; set; }

        /// <summary>
        ///     付款计划行
        /// </summary>
        public List<PaymentScheduleLineDTO> PaymentScheduleLines { get; set; }

    }
}
