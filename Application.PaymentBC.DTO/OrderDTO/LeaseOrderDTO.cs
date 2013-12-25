#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 21:46:31
// 文件名：LeaseOrderDTO
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
    ///    租赁订单DTO，包含飞机租赁订单、发动机租赁订单
    /// </summary>
    [DataServiceKey("Id")]
    public class LeaseOrderDTO
    {
        public LeaseOrderDTO()
        {
        }
        /// <summary>
        ///     订单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }


        /// <summary>
        ///     生效日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     供应商外键
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

    }
}
