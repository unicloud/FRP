#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/24 20:42:26
// 文件名：UtilizationReport
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/24 20:42:26
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ReportViewer.Part;

#endregion

namespace ReportViewer.UtilizationReport
{
   public class UtilizationReport
   {
       private static readonly Uri ServiceRoot = null;//new Uri(Application.Current.Resources["PartDataService"].ToString());
       readonly PartData _serviceContext = new PartData(ServiceRoot);
       public IEnumerable<UtilizationReportDTO> GetUtilizationReports()
       {
           var reports = from report in _serviceContext.UtilizationReports
                         select report;
           return reports.AsEnumerable();
       }
       public IEnumerable<UtilizationReportDTO> GetUtilizationReport(string regNumber)
       {
           var reports = from report in _serviceContext.UtilizationReports
                              where report.RegNumber.StartsWith(regNumber)
                              select report;

           return reports.ToArray();
       }

       public IEnumerable<SubUtilizationReportDTO> GetFirstSubReports(string regNumber)
       {
           var reports = from report in _serviceContext.UtilizationReports
                         where report.RegNumber.StartsWith(regNumber)
                         select report;

           return reports.FirstOrDefault().FirstSubReports;
       }

       public IEnumerable<SubUtilizationReportDTO> GetSecondSubReports(string regNumber)
       {
           var reports = from report in _serviceContext.UtilizationReports
                         where report.RegNumber.StartsWith(regNumber)
                         select report;

           return reports.FirstOrDefault().SecondSubReports;
       }

       public IEnumerable<SubUtilizationReportDTO> GetThirdSubReports(string regNumber)
       {
           var reports = from report in _serviceContext.UtilizationReports
                         where report.RegNumber.StartsWith(regNumber)
                         select report;

           return reports.FirstOrDefault().ThirdSubReports;
       }

       public IEnumerable<SubUtilizationReportDTO> GetForthSubReports(string regNumber)
       {
           var reports = from report in _serviceContext.UtilizationReports
                         where report.RegNumber.StartsWith(regNumber)
                         select report;

           return reports.FirstOrDefault().ForthSubReports;
       }
    }
}
