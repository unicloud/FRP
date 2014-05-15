#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:26:12
// 文件名：Owner
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:26:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    /// <summary>
    /// 所有权人
    /// </summary>
    [KnownType(typeof(Manager))]
    [KnownType(typeof(Airlines))]
    [KnownType(typeof(Manufacturer))]
    [Serializable]
    public class Owner
    {
        public Owner()
        {
            this.OwnershipHistorys = new HashSet<OwnershipHistory>();
            this.MailAddresses = new HashSet<MailAddress>();
        }
        public Guid OwnerID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ShortName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public bool IsValid { get; set; } //是否有效
        public int SupplierType { get; set; } //供应商类型 0-非供应商 1- 国内 2- 国外

        public virtual ICollection<OwnershipHistory> OwnershipHistorys { get; set; }
        public virtual ICollection<MailAddress> MailAddresses { get; set; }
    }
}
