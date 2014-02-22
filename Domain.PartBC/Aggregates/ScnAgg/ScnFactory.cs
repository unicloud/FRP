#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ScnAgg
{
    /// <summary>
    /// Scn工厂。
    /// </summary>
    public static class ScnFactory
    {
        /// <summary>
        /// 创建Scn。
        /// </summary>
        ///  <returns>Scn</returns>
        public static Scn CreateScn()
        {
            var scn = new Scn();
            scn.GenerateNewIdentity();
            return scn;
        }

        /// <summary>
        ///     设置Scn属性
        /// </summary>
        /// <param name="scn">付款通知</param>
        /// <param name="type">SCN类型</param>
        /// <param name="checkDate">确认日期</param>
        /// <param name="cscNumber">批次号</param>
        /// <param name="modNumber">MOD号</param>
        /// <param name="tsNumber">TS号</param>
        /// <param name="cost">费用</param>
        /// <param name="scnNumber">SCN号</param>
        /// <param name="scnType">SCN适用类型</param>
        /// <param name="scnStatus">SCN状态</param>
        /// <param name="description">描述</param>
        /// <param name="scnDocName">Scn文档名称</param>
        /// <param name="scnDocumentId">SCN文件</param>
        /// <param name="auditOrganization">审核部门</param>
        /// <param name="auditor">审核人</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="auditNotes">审核意见</param>
        public static void SetScn(Scn scn, int type, DateTime checkDate, string cscNumber, string modNumber,
            string tsNumber, decimal cost, string scnNumber, int scnType, int scnStatus, string description, string scnDocName, Guid scnDocumentId,
            string auditOrganization, string auditor, DateTime? auditTime, string auditNotes)
        {
            scn.SetType((ScnType)type);
            scn.SetCheckDate(checkDate);
            scn.SetCscNumber(cscNumber);
            scn.SetModNumber(modNumber);
            scn.SetTsNumber(tsNumber);
            scn.SetCost(cost);
            scn.SetScnNumber(scnNumber);
            scn.SetScnType((ScnApplicableType)scnType);
            scn.SetScnStatus((ScnStatus)scnStatus);
            scn.SetDescription(description);
            scn.SetScnDocument(scnDocName, scnDocumentId);
            scn.SetAuditMsg(auditOrganization, auditor, auditTime, auditNotes);
        }

        /// <summary>
        ///     创建适用飞机
        /// </summary>
        /// <returns></returns>
        public static ApplicableAircraft CreateApplicableAircraft()
        {
            var applicableAircraft = new ApplicableAircraft();
            applicableAircraft.GenerateNewIdentity();
            return applicableAircraft;
        }

        /// <summary>
        ///     设置适用飞机属性
        /// </summary>
        /// <param name="applicableAircraft">适用飞机</param>
        /// <param name="completeDate">完成日期</param>
        /// <param name="cost">费用</param>
        /// <param name="contractAircraftId">合同飞机外键</param>
        public static void SetApplicableAircraft(ApplicableAircraft applicableAircraft, DateTime completeDate, decimal cost, int contractAircraftId)
        {
            applicableAircraft.SetCompleteDate(completeDate);
            applicableAircraft.SetCost(cost);
            applicableAircraft.SetContractAircraft(contractAircraftId);
        }
    }
}
