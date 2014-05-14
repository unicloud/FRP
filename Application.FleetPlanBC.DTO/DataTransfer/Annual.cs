#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:40:22
// 文件名：Annual
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:40:22
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
    public class Annual
    {
        public Annual()
        {
            this.Plans = new HashSet<Plan>();
        }
        public Guid AnnualID { get; set; }
        public Guid ProgrammingID { get; set; }
        [Display(Name = "年度")]
        public int Year { get; set; }
        [Display(Name = "打开/关闭")]
        public bool? IsOpen { get; set; }

        public virtual Programming Programming { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }

}
