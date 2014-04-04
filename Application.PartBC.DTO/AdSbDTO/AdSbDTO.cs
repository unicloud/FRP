#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 17:25:20
// 文件名：AdSbDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 17:25:20
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// AdSb
    /// </summary>
    [DataServiceKey("Id")]
    public class AdSbDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 系列(10)
        /// </summary>
        public String AircraftSeries
        {
            get;
            set;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            get;
            set;
        }

        /// <summary>      
        /// 文件号               
        /// </summary>     
        public string FileNo
        {
            get;
            set;
        }

        /// <summary>      
        /// 文件版本            
        /// </summary>     
        public string FileVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 执行飞机
        /// </summary>
        public string ComplyAircraft
        {
            get;
            set;
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public string ComplyStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime? ComplyDate
        {
            get;
            set;
        }

        /// <summary>
        /// 执行反馈
        /// </summary>
        public string ComplyNotes
        {
            get;
            set;
        }

        /// <summary>
        /// 执行监控关闭情况
        /// </summary>
        public string ComplyClose
        {
            get;
            set;
        }

        /// <summary>
        /// 执行费用
        /// </summary>
        public decimal? ComplyFee
        {
            get;
            set;
        }

        /// <summary>
        /// 执行费用备注
        /// </summary>
        public string ComplyFeeNotes
        {
            get;
            set;
        }

        /// <summary>
        /// 执行费用币种
        /// </summary>
        public string ComplyFeeCurrency
        {
            get;
            set;
        }

        #endregion
    }
}
