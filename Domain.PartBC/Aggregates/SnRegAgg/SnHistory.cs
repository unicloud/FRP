#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：SnHistory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    /// SnReg聚合根。
    /// SnHistory
    /// 装机历史
    /// </summary>
    public class SnHistory : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public string Sn
        {
            get;
            set;
        }

        /// <summary>
        /// 装上时间
        /// </summary>
        public DateTime InstallDate
        {
            get;
            set;
        }

        /// <summary>
        /// 拆下时间
        /// </summary>
        public DateTime? RemoveDate
        {
            get;
            set;
        }

        /// <summary>
        /// FI号
        /// </summary>
        public string FiNumber
        {
            get;
            set;
        }

        /// <summary>
        /// CSN
        /// </summary>
        public string CSN
        {
            get;
            set;
        }

        /// <summary>
        /// CSR
        /// </summary>
        public string CSR
        {
            get;
            set;
        }

        /// <summary>
        /// TSN
        /// </summary>
        public string TSN
        {
            get;
            set;
        }

        /// <summary>
        /// TSR
        /// </summary>
        public string TSR
        {
            get;
            set;
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            set;
        }

        /// <summary>
        /// Sn外键
        /// </summary>
        public int SnId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        #endregion

        #region 操作

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
