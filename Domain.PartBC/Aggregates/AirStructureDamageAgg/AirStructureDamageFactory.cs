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


namespace UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg
{
    /// <summary>
    /// AirStructureDamage工厂。
    /// </summary>
    public static class AirStructureDamageFactory
    {
        /// <summary>
        /// 创建AirStructureDamage。
        /// </summary>
        ///  <returns>AirStructureDamage</returns>
        public static AirStructureDamage CreateAirStructureDamage()
        {
            var airStructureDamage = new AirStructureDamage();
            airStructureDamage.GenerateNewIdentity();
            return airStructureDamage;
        }

        /// <summary>
        /// 设置结构损坏属性
        /// </summary>
        /// <param name="airStructureDamage">结构损伤</param>
        /// <param name="aircraftId">飞机Id</param>
        /// <param name="aircraftReg">飞机注册号</param>
        /// <param name="aircraftType">机型</param>
        /// <param name="aircraftSeries">系列</param>
        /// <param name="source">来源</param>
        /// <param name="reportNo">报告号</param>
        /// <param name="reportType">报告类型</param>
        /// <param name="description">描述</param>
        /// <param name="reportTime">报告日期</param>
        /// <param name="closeTime">关闭日期</param>
        /// <param name="repairDeadline">修理期限</param>
        /// <param name="status">状态</param>
        /// <param name="level">腐蚀级别</param>
        /// <param name="defer">是否保留</param>
        /// <param name="totalCost">总金额</param>
        /// <param name="tecAssess">技术评估</param>
        /// <param name="treatResult">处理结果</param>
        /// <param name="documentId">文档Id</param>
        /// <param name="documentName">文档名</param>
        public static void SetAirStructureDamage(AirStructureDamage airStructureDamage, Guid aircraftId, string aircraftReg, string aircraftType, string aircraftSeries,
            string source, string reportNo, int reportType, string description, DateTime? reportTime, DateTime? closeTime, string repairDeadline,
            int status, int level, bool defer, decimal totalCost, string tecAssess, string treatResult, Guid documentId, string documentName)
        {
            airStructureDamage.SetAircraftInfo(aircraftId, aircraftReg, aircraftType, aircraftSeries);
            airStructureDamage.SetReport(source, reportNo, (AirStructureReportType)reportType, description);
            airStructureDamage.SetReportDate(reportTime, closeTime, repairDeadline);
            airStructureDamage.SetStatus((AirStructureDamageStatus)status);
            airStructureDamage.SetLevel((AircraftDamageLevel)level);
            airStructureDamage.SetDefer(defer);
            airStructureDamage.SetCost(totalCost);
            airStructureDamage.SetResult(tecAssess, treatResult);
            airStructureDamage.SetDocument(documentId, documentName);
        }
    }
}
