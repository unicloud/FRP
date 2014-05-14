#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:26:31
// 文件名：Airlines
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:26:31
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
    public class Airlines : Owner
    {
        public Airlines()
        {
            this.OperationHistories = new HashSet<OperationHistory>();
            this.SubOperationHistories = new HashSet<SubOperationHistory>();
            this.Plans = new HashSet<Plan>();
            this.Requests = new HashSet<Request>();
            this.SubAirlines = new HashSet<Airlines>();
        }

        public Guid? MasterID { get; set; } // 所属航空公司ID
        [StringLength(3)]
        public string ICAOCode { get; set; } // 三字码
        [StringLength(2)]
        public string IATACode { get; set; } // 二字码
        [StringLength(30)]
        public string LevelCode { get; set; } //用于排序，由航空公司自定规则
        public bool IsShareData { get; set; } //航空公司之间是否共享数据
        public bool IsCurrent { get; set; } // 是否当前航空公司
        public DateTime? CreateDate { get; set; } // 创建日期
        public DateTime? LogoutDate { get; set; } // 注销日期
        public DateTime? OperationDate { get; set; } // 运营日期
        public DateTime? ExportDate { get; set; } //   退出运营日期
        public int SubType { get; set; } // 航空公司类型，0-代表分公司，1-子公司，2-分子公司(但不上报计划、申请)

        public int Type { get; set; } // 航空公司类型，包括运输航空公司、通用航空公司等
        public AirlinesType AirlinesType
        {
            get { return (AirlinesType)Type; }
            set { Type = (int)value; }
        }

        public int Status { get; set; }  //主要适用于分公司编辑，分公司允许删除；0-在用，1-删除
        public FilialeStatus FilialeStatus
        {
            get { return (FilialeStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual ICollection<OperationHistory> OperationHistories { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Airlines> SubAirlines { get; set; }
        public virtual ICollection<SubOperationHistory> SubOperationHistories { get; set; }
    }

}
