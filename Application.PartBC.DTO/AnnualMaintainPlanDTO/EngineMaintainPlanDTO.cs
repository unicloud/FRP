#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:39:32
// 文件名：EngineMaintainPlanDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:39:32
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// EngineMaintainPlanDTO
    /// </summary>
    [DataServiceKey("Id")]
    public class EngineMaintainPlanDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 0 是 发动机非FHA；1是 发动机超包修
        /// </summary>
        public int MaintainPlanType { get; set; }

        /// <summary>
        /// 美元汇率
        /// </summary>
        public decimal DollarRate { get; set; }
        /// <summary>
        /// 公司分管领导
        /// </summary>
        public string CompanyLeader { get; set; }
        /// <summary>
        /// 部门领导
        /// </summary>
        public string DepartmentLeader { get; set; }
        /// <summary>
        /// 预算管理员
        /// </summary>
        public string BudgetManager { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 年度ID
        /// </summary>
        public Guid AnnualId { get; set; }

        private List<EngineMaintainPlanDetailDTO> _engineMaintainPlanDetails;
        public List<EngineMaintainPlanDetailDTO> EngineMaintainPlanDetails
        {
            get { return _engineMaintainPlanDetails ?? (_engineMaintainPlanDetails = new List<EngineMaintainPlanDetailDTO>()); }
            set { _engineMaintainPlanDetails = value; }
        }
        #endregion
    }
}
