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
using System.Collections.ObjectModel;
using System.Linq;
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
           //var reports = from report in _serviceContext.UtilizationReports
           //              select report;
           //return reports.AsEnumerable();
           return _reports;
       }
       public IEnumerable<UtilizationReportDTO> GetUtilizationReport(string regNumber)
       {
           if (string.IsNullOrEmpty(regNumber))
           {
               return null;
           }
           //var reports = from report in _serviceContext.UtilizationReports
           //                   where report.RegNumber.StartsWith(regNumber)
           //                   select report;
           var reports = _reports.Where(p => p.RegNumber.StartsWith(regNumber));
           return reports.ToArray();
       }

       public IEnumerable<SubUtilizationReportDTO> GetFirstSubReports(string regNumber)
       {
           if (string.IsNullOrEmpty(regNumber))
           {
               return null;
           }
           //var reports = from report in _serviceContext.UtilizationReports
           //              where report.RegNumber.StartsWith(regNumber)
           //              select report;
           var reports = _reports.Where(p => p.RegNumber.StartsWith(regNumber));
           return reports.FirstOrDefault().FirstSubReports;
       }

       public IEnumerable<SubUtilizationReportDTO> GetSecondSubReports(string regNumber)
       {
           if (string.IsNullOrEmpty(regNumber))
           {
               return null;
           }
           //var reports = from report in _serviceContext.UtilizationReports
           //              where report.RegNumber.StartsWith(regNumber)
           //              select report;
           var reports = _reports.Where(p => p.RegNumber.StartsWith(regNumber));
           return reports.FirstOrDefault().SecondSubReports;
       }

       public IEnumerable<SubUtilizationReportDTO> GetThirdSubReports(string regNumber)
       {
           if (string.IsNullOrEmpty(regNumber))
           {
               return null;
           }
           //var reports = from report in _serviceContext.UtilizationReports
           //              where report.RegNumber.StartsWith(regNumber)
           //              select report;
           var reports = _reports.Where(p => p.RegNumber.StartsWith(regNumber));
           return reports.FirstOrDefault().ThirdSubReports;
       }

       public IEnumerable<SubUtilizationReportDTO> GetForthSubReports(string regNumber)
       {
           if (string.IsNullOrEmpty(regNumber))
           {
               return null;
           }
           //var reports = from report in _serviceContext.UtilizationReports
           //              where report.RegNumber.StartsWith(regNumber)
           //              select report;
           var reports = _reports.Where(p => p.RegNumber.StartsWith(regNumber));
           return reports.FirstOrDefault().ForthSubReports;
       }


       private List<UtilizationReportDTO> _reports = new List<UtilizationReportDTO>
                                                    {
                                                        new UtilizationReportDTO
                                                        {
                                                            Id = 1,Title = "MONTHLY UTILIZATION REPORT",
                                                            RegNumber = "B-6518",
                                                            FirstSubReports =new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " Aircraft type",FirstValue = "A330"},
                                                                                new SubUtilizationReportDTO{Title = " Registration",FirstValue = "B-6518"},
                                                                                 new SubUtilizationReportDTO{Title = " Aircraft Serial Number",FirstValue = "1082"},
                                                                                  new SubUtilizationReportDTO{Title = " Period",FirstValue = "From 1st Jan 2013 to 31st Jan 2013"},
                                                                             },
                                                                             SecondSubReports = new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " Aircraft total time since new (TSN)",FirstValue = ""},
                                                                                new SubUtilizationReportDTO{Title = " Aircraft total Cycles since new (CSN)",FirstValue = ""},
                                                                                 new SubUtilizationReportDTO{Title = " Airframe Flight Hours flown during quarter",FirstValue = ""},
                                                                                  new SubUtilizationReportDTO{Title = " Airframe Cycles/landings during quarter",FirstValue = ""},
                                                                                   new SubUtilizationReportDTO{Title = " Scheduled  next “C” and Month",FirstValue = ""},
                                                                             },
                                                                             ThirdSubReports = new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " S/N of Engine Installed",FirstValue = "",SecondValue = ""},
                                                                                new SubUtilizationReportDTO{Title = " S/N of Original Engine's",FirstValue = "",SecondValue = ""},
                                                                                 new SubUtilizationReportDTO{Title = " Present Location of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                  new SubUtilizationReportDTO{Title = " TSN of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                   new SubUtilizationReportDTO{Title = " CSN of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                    new SubUtilizationReportDTO{Title = " Hours flown during Quarter of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                     new SubUtilizationReportDTO{Title = " Cycles During Quarter of Original Engine",FirstValue = "",SecondValue = ""},
                                                                             },
                                                                             ForthSubReports = new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " S/N of  The Installed One",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                new SubUtilizationReportDTO{Title = " TSN of  The Installed One",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                 new SubUtilizationReportDTO{Title = " CSN of  The Installed One",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                  new SubUtilizationReportDTO{Title = " Total Hours Flown During Quarter",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                   new SubUtilizationReportDTO{Title = " Total Cycles Made  During Quarter",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                             },
                                                        },
                                                         new UtilizationReportDTO
                                                        {
                                                            Id = 1,Title = "MONTHLY UTILIZATION REPORT",
                                                            RegNumber = "B-2286",
                                                            FirstSubReports =new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " Aircraft type",FirstValue = "A321-100"},
                                                                                new SubUtilizationReportDTO{Title = " Registration",FirstValue = "B-2286"},
                                                                                 new SubUtilizationReportDTO{Title = " Aircraft Serial Number",FirstValue = "550"},
                                                                                  new SubUtilizationReportDTO{Title = " Period",FirstValue = "From 19th Dec 2012 to 18th Mar 2013"},
                                                                             },
                                                                             SecondSubReports = new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " Aircraft total time since new (TSN)",FirstValue = ""},
                                                                                new SubUtilizationReportDTO{Title = " Aircraft total Cycles since new (CSN)",FirstValue = ""},
                                                                                 new SubUtilizationReportDTO{Title = " Airframe Flight Hours flown during quarter",FirstValue = ""},
                                                                                  new SubUtilizationReportDTO{Title = " Airframe Cycles/landings during quarter",FirstValue = ""},
                                                                                   new SubUtilizationReportDTO{Title = " Scheduled  next “C” and Month",FirstValue = ""},
                                                                             },
                                                                             ThirdSubReports = new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " S/N of Engine Installed",FirstValue = "",SecondValue = ""},
                                                                                new SubUtilizationReportDTO{Title = " S/N of Original Engine's",FirstValue = "",SecondValue = ""},
                                                                                 new SubUtilizationReportDTO{Title = " Present Location of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                  new SubUtilizationReportDTO{Title = " TSN of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                   new SubUtilizationReportDTO{Title = " CSN of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                    new SubUtilizationReportDTO{Title = " Hours flown during Quarter of Original Engine",FirstValue = "",SecondValue = ""},
                                                                                     new SubUtilizationReportDTO{Title = " Cycles During Quarter of Original Engine",FirstValue = "",SecondValue = ""},
                                                                             },
                                                                             ForthSubReports = new ObservableCollection<SubUtilizationReportDTO>
                                                                             {
                                                                               new SubUtilizationReportDTO{Title = " S/N of  The Installed One",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                new SubUtilizationReportDTO{Title = " TSN of  The Installed One",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                 new SubUtilizationReportDTO{Title = " CSN of  The Installed One",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                  new SubUtilizationReportDTO{Title = " Total Hours Flown During Quarter",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                                   new SubUtilizationReportDTO{Title = " Total Cycles Made  During Quarter",FirstValue = "",SecondValue = "",ThirdValue = "",ForthValue = ""},
                                                                             },
                                                        },
                                                    };
   }
}
