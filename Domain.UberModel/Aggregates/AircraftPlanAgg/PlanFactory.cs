#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/7/4 15:35:42
// 文件名：PlanFactory
// 版本：V1.0.0
//
// 修改者：  时间：2014/7/4 15:35:42
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftPlanAgg
{
    /// <summary>
    ///     运力增减计划工厂
    /// </summary>
    public static class PlanFactory
    {
        /// <summary>
        /// 创建运力增减计划
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title">标题</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="versionNumber">版本号</param>
        /// <param name="isCurrentVersion">是否当前版本</param>
        /// <param name="submitDate">提交日期</param>
        /// <param name="createDate">创建日期</param>
        /// <param name="isFinished">是否完成</param>
        /// <param name="status">状态</param>
        /// <param name="publishStatus"></param>
        /// <param name="airlinesId">飞机外键</param>
        /// <param name="annualId"></param>
        /// <returns></returns>
        public static Plan CreatePlan(Guid id,string title,bool isValid,int versionNumber,bool isCurrentVersion,DateTime submitDate,
            DateTime createDate,bool isFinished,int status,int publishStatus,Guid airlinesId,Guid annualId)
        {
            var plan = new Plan();
            plan.ChangeCurrentIdentity(id);
            plan.SetTitle(title);
            plan.IsValid = isValid;
            plan.VersionNumber = versionNumber;
            plan.IsCurrentVersion = isCurrentVersion;
            plan.SubmitDate = submitDate;
            plan.CreateDate = createDate;
            plan.IsFinished = isFinished;
            plan.Status = (PlanStatus)status;
            plan.PublishStatus = (PlanPublishStatus) publishStatus;
            plan.AirlinesId = airlinesId;
            plan.AnnualId = annualId;

            return plan;
        }
    }
}
