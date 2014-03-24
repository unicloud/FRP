#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/24 20:26:51
// 文件名：UtilizationReportDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/24 20:26:51
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
    [DataServiceKey("Id")]
    public class UtilizationReportDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReportDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        private List<SubUtilizationReportDTO> _firstSubReports;
        public List<SubUtilizationReportDTO> FirstSubReports
        {
            get { return _firstSubReports ?? new List<SubUtilizationReportDTO>(); }
            set { _firstSubReports = value; }
        }

        private List<SubUtilizationReportDTO> _secondSubReports;
        public List<SubUtilizationReportDTO> SecondSubReports
        {
            get { return _secondSubReports ?? new List<SubUtilizationReportDTO>(); }
            set { _secondSubReports = value; }
        }

        private List<SubUtilizationReportDTO> _thirdSubReports;
        public List<SubUtilizationReportDTO> ThirdSubReports
        {
            get { return _thirdSubReports ?? new List<SubUtilizationReportDTO>(); }
            set { _thirdSubReports = value; }
        }

        private List<SubUtilizationReportDTO> _forthSubReports;
        public List<SubUtilizationReportDTO> ForthSubReports
        {
            get { return _forthSubReports ?? new List<SubUtilizationReportDTO>(); }
            set { _forthSubReports = value; }
        }
    }
}
