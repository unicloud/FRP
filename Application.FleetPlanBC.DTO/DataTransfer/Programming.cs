#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:41:50
// 文件名：Programming
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:41:50
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
    public class Programming
    {
        public Programming()
        {
            this.PlanAnnuals = new HashSet<Annual>();
        }

        public Guid ProgrammingID { get; set; }
        [StringLength(20), Display(Name = "规划期间")]
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Annual> PlanAnnuals { get; set; }
    }
}
