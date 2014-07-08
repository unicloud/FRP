#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/27 18:17:43
// 文件名：AdSb
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/27 18:17:43
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AdSbAgg
{
    /// <summary>
    /// AdSb聚合根。
    /// </summary>
    public class AdSb : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AdSb()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 系列(10)
        /// </summary>
        public String AircraftSeries
        {
            get;
            private set;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            get;
            private set;
        }

        /// <summary>      
        /// 文件号               
        /// </summary>     
        public string FileNo
        {
            get;
            private set;
        }

        /// <summary>      
        /// 文件版本            
        /// </summary>     
        public string FileVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行飞机
        /// </summary>
        public string ComplyAircraft
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public string ComplyStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime? ComplyDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行反馈
        /// </summary>
        public string ComplyNotes
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行监控关闭情况
        /// </summary>
        public string ComplyClose
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行费用
        /// </summary>
        public decimal? ComplyFee
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行费用备注
        /// </summary>
        public string ComplyFeeNotes
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行费用币种
        /// </summary>
        public string ComplyFeeCurrency
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Mod号
        /// </summary>
        public string ModNumber
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性
        

       
        #endregion

        #region 导航属性
       
        #endregion

        #region 操作

        /// <summary>
        /// 系列(10)
        /// </summary>
        /// 系列(10)
        public void SetAircraftSeries(string aircraftSeries)
        {
            AircraftSeries = aircraftSeries;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public void SetFile(string fileType, string fileNo, string fileVersion)
        {
            FileType = fileType;
            FileNo = fileNo;
            FileVersion = fileVersion;
        }

        /// <summary>
        /// 执行飞机
        /// </summary>
        public void SetComplyAircraft(string complyAircraft)
        {
            ComplyAircraft = complyAircraft;
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public void SetComplyStatus(string complyStatus)
        {
            ComplyStatus = complyStatus;
        }

        /// <summary>
        /// 执行日期
        /// </summary>
        public void SetComplyDate(DateTime? complyDate)
        {
            ComplyDate = complyDate;
        }

        /// <summary>
        /// 执行反馈
        /// </summary>
        public void SetComplyNotes(string complyNotes)
        {
            ComplyNotes = complyNotes;
        }

        /// <summary>
        /// 执行监控关闭情况
        /// </summary>
        public void SetComplyClose(string complyClose)
        {
            ComplyClose = complyClose;
        }

        /// <summary>
        /// 执行费用
        /// </summary>
        public void SetComplyFee(decimal? complyFee,string complyFeeNotes,string complyFeeCurrency)
        {
            ComplyFee = complyFee;
            ComplyFeeNotes = complyFeeNotes;
            ComplyFeeCurrency = complyFeeCurrency;
        }

        /// <summary>
        ///     设置MOD号
        /// </summary>
        /// <param name="modNumber">MOD号</param>
        public void SetModNumber(string modNumber)
        {
            if (string.IsNullOrWhiteSpace(modNumber))
            {
                throw new ArgumentException("MOD号参数为空！");
            }

            ModNumber = modNumber;
        }
        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
