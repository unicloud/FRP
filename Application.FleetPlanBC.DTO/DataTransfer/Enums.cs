#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 9:10:35
// 文件名：Enums
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 9:10:35
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
    /// <summary>
    /// 航空公司类型
    /// </summary>
    public enum AirlinesType
    {
        [Display(Name = "运输航空公司")]
        TransportAirline,
        [Display(Name = "通用航空公司")]
        GeneralAirline
    }

    /// <summary>
    /// 管理状态
    /// </summary>
    public enum ManageStatus
    {
        [Display(Name = "预备")]
        Prepare,
        [Display(Name = "计划")]
        Plan,
        [Display(Name = "申请")]
        Request,
        [Display(Name = "批文")]
        Approval,
        [Display(Name = "签约")]
        Agreement,
        [Display(Name = "技术接收")]
        TechReceip,
        [Display(Name = "接收")]
        Receipt,
        [Display(Name = "运营")]
        Operation,
        [Display(Name = "停场待退")]
        OnGround,
        [Display(Name = "技术交付")]
        TechDelivery,
        [Display(Name = "退役")]
        Retired
    }

    /// <summary>
    /// 协议类型
    /// </summary>
    public enum AgreementType
    {
        [Display(Name = "意向")]
        Intention,
        [Display(Name = "合同")]
        Contract
    }

    /// <summary>
    /// 协议阶段
    /// </summary>
    public enum AgreementPhase
    {
        [Display(Name = "计划")]
        Plan,
        [Display(Name = "谈判")]
        Negotiate,
        [Display(Name = "签约")]
        Signed
    }

    /// <summary>
    /// 处理状态
    /// </summary>
    public enum OpStatus
    {
        [Display(Name = "1：草稿")]
        Draft,
        [Display(Name = "2：待审核")]
        Checking,
        [Display(Name = "3：已审核")]
        Checked,
        [Display(Name = "4：已提交")]
        Submited
    }

    /// <summary>
    /// 申请的处理状态
    /// </summary>
    public enum ReqStatus
    {
        [Display(Name = "1：草稿")]
        Draft,
        [Display(Name = "2：待审核")]
        Checking,
        [Display(Name = "3：已审核")]
        Checked,
        [Display(Name = "4：已提交")]
        Submited,
        [Display(Name = "5：已审批")]
        Examined
    }

    /// <summary>
    /// 计划的处理状态
    /// </summary>
    public enum PlanStatus
    {
        [Display(Name = "1：草稿")]
        Draft,
        [Display(Name = "2：待审核")]
        Checking,
        [Display(Name = "3：已审核")]
        Checked,
        [Display(Name = "4：已提交")]
        Submited,
        [Display(Name = "5：退回")]
        Returned
    }

    /// <summary>
    /// 计划发布状态
    /// </summary>
    public enum PlanPublishStatus
    {
        [Display(Name = "1：待发布")]
        Draft,
        [Display(Name = "2：待审核")]
        Checking,
        [Display(Name = "3：已审核")]
        Checked,
        [Display(Name = "4：已发布")]
        Submited,
    }


    /// <summary>
    /// 分子公司当前所处状态
    /// </summary>
    public enum FilialeStatus
    {
        [Display(Name = "在运营")]
        InUse,
        [Display(Name = "已撤销")]
        Deleted,
    }

}
