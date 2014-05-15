#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 9:07:17
// 文件名：DeliveryRisk
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 9:07:17
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    [Serializable]
    public class DeliveryRisk
    {
        public DeliveryRisk()
        {
            this.AgreementDetails = new HashSet<AgreementDetail>();
        }
        public Guid DeliveryRiskID { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<AgreementDetail> AgreementDetails { get; set; }
    }

}
