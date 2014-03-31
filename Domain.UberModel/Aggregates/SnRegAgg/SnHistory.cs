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
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;

namespace UniCloud.Domain.UberModel.Aggregates.SnRegAgg
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
            internal set;
        }

        /// <summary>
        /// 装上时间
        /// </summary>
        public DateTime InstallDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 拆下时间
        /// </summary>
        public DateTime? RemoveDate
        {
            get;
            private set;
        }

        /// <summary>
        /// FI号
        /// </summary>
        public string FiNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// CSN
        /// </summary>
        public string CSN
        {
            get;
            private set;
        }

        /// <summary>
        /// CSR
        /// </summary>
        public string CSR
        {
            get;
            private set;
        }

        /// <summary>
        /// TSN
        /// </summary>
        public string TSN
        {
            get;
            private set;
        }

        /// <summary>
        /// TSR
        /// </summary>
        public string TSR
        {
            get;
            private set;
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            private set;
        }

        /// <summary>
        /// Sn外键
        /// </summary>
        public int SnRegId
        {
            get;
            internal set;
        }
        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置装上时间
        /// </summary>
        /// <param name="date">装上时间</param>
        public void SetInstallDate(DateTime date)
        {
            InstallDate = date;
        }

        /// <summary>
        ///     设置拆下时间
        /// </summary>
        /// <param name="date">拆下时间</param>
        public void SetRemoveDate(DateTime? date)
        {
            RemoveDate = date;
        }

        /// <summary>
        ///     设置FI号
        /// </summary>
        /// <param name="fiNumber">FI号</param>
        public void SetFiNumber(string fiNumber)
        {
            if (string.IsNullOrWhiteSpace(fiNumber))
            {
                throw new ArgumentException("FI号参数为空！");
            }

            FiNumber = fiNumber;
        }

        /// <summary>
        ///     设置CSN
        /// </summary>
        /// <param name="csn">CSN</param>
        public void SetCSN(string csn)
        {
            if (string.IsNullOrWhiteSpace(csn))
            {
                throw new ArgumentException("CSN参数为空！");
            }

            CSN = csn;
        }


        /// <summary>
        ///     设置CSR
        /// </summary>
        /// <param name="csr">CSR</param>
        public void SetCSR(string csr)
        {
            if (string.IsNullOrWhiteSpace(csr))
            {
                throw new ArgumentException("CSR参数为空！");
            }

            CSR = csr;
        }

        /// <summary>
        ///     设置TSN
        /// </summary>
        /// <param name="tsn">TSN</param>
        public void SetTSN(string tsn)
        {
            if (string.IsNullOrWhiteSpace(tsn))
            {
                throw new ArgumentException("TSN参数为空！");
            }

            TSN = tsn;
        }

        /// <summary>
        ///     设置TSR
        /// </summary>
        /// <param name="tsr">TSR</param>
        public void SetTSR(string tsr)
        {
            if (string.IsNullOrWhiteSpace(tsr))
            {
                throw new ArgumentException("TSR参数为空！");
            }

            TSR = tsr;
        }

        /// <summary>
        ///     设置飞机
        /// </summary>
        /// <param name="aircraft">飞机</param>
        public void SetAircraft(Aircraft aircraft)
        {
            if (aircraft == null || aircraft.IsTransient())
            {
                throw new ArgumentException("飞机参数为空！");
            }

            AircraftId = aircraft.Id;
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
