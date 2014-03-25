#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/24 20:27:06
// 文件名：SubUtilizationReportDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/24 20:27:06
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    [DataServiceKey("Id")]
    public class SubUtilizationReportDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
        public string ThirdValue { get; set; }
        public string ForthValue { get; set; }
        public string FifthValue { get; set; }
    }
}
