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

namespace UniCloud.Domain.PartBC.Aggregates.AdSbAgg
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
        public string Actype
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
        public string ComplyAc
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
        public decimal? ComFee
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行费用备注
        /// </summary>
        public string ComFeeNotes
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行费用币种
        /// </summary>
        public string ComFeeCurrency
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性
        /// <summary>
        /// Adsb外键
        /// </summary>
        public  int AdsbId
        {
            get;
            private set;
        }
        #endregion

        #region 导航属性
        /// <summary>
        /// Adsb
        /// </summary>
        public virtual AdSb Adsb
        {
            get;
            set;
        }
        #endregion

        #region 操作

        /// <summary>
        /// 系列(10)
        /// </summary>
        public void SetActype()
        {
           
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public void SetFileType()
        {
           
        }

        /// <summary>      
        /// 文件号               
        /// </summary>     
        public void SetFileNo()
        {
            
        }

        /// <summary>      
        /// 文件版本            
        /// </summary>     
        public void SetFileVersion()
        {
            
        }

        /// <summary>
        /// 执行飞机
        /// </summary>
        public void SetComplyAc()
        {
           
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public void SetComplyStatus()
        {
           
        }

        /// <summary>
        /// 执行日期
        /// </summary>
        public void SetComplyDate()
        {
        }

        /// <summary>
        /// 执行反馈
        /// </summary>
        public void SetComplyNotes()
        {
           
        }

        /// <summary>
        /// 执行监控关闭情况
        /// </summary>
        public void SetComplyClose()
        {
            
        }

        /// <summary>
        /// 执行费用
        /// </summary>
        public void SetComFee()
        {
        }

        /// <summary>
        /// 执行费用备注
        /// </summary>
        public void SetComFeeNotes()
        {
        }

        /// <summary>
        /// 执行费用币种
        /// </summary>
        public void SetComFeeCurrency()
        {
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
