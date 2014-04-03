#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 17:25:28
// 文件名：SnInstallHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.PnRegAgg;
using UniCloud.Domain.UberModel.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.SnInstallHistoryAgg
{
    /// <summary>
    ///     SnInstallHistory聚合根。
    ///     序号件装机历史
    /// </summary>
    public class SnInstallHistory : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnInstallHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     装机序号
        /// </summary>
        public string Sn { get; private set; }

        /// <summary>
        ///     装机件号
        /// </summary>
        public string Pn { get; private set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     装上时间
        /// </summary>
        public DateTime InstallDate { get; private set; }

        /// <summary>
        ///     拆下时间
        /// </summary>
        public DateTime? RemoveDate { get; private set; }

        /// <summary>
        ///     CSN，自装机以来使用循环
        /// </summary>
        public int CSN { get; private set; }

        /// <summary>
        ///     CSR，自上一次修理以来使用循环
        /// </summary>
        public int CSR { get; private set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; private set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机ID
        /// </summary>
        public Guid AircraftId { get; private set; }

        /// <summary>
        ///     Sn外键
        /// </summary>
        public int SnRegId { get; private set; }

        /// <summary>
        ///     Pn外键
        /// </summary>
        public int PnRegId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置装机序号
        /// </summary>
        /// <param name="snReg">装机序号</param>
        public void SetSn(SnReg snReg)
        {
            if (snReg == null || snReg.IsTransient())
            {
                throw new ArgumentException("装机序号参数为空！");
            }

            Sn = snReg.Sn;
            SnRegId = snReg.Id;
        }

        /// <summary>
        ///     设置装机件号
        /// </summary>
        /// <param name="pnReg">装机件号</param>
        public void SetPn(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("装机件号参数为空！");
            }

            Pn = pnReg.Pn;
            PnRegId = pnReg.Id;
        }

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
        ///     设置CSN
        /// </summary>
        /// <param name="csn">CSN</param>
        public void SetCSN(int csn)
        {
            if (csn < 0)
            {
                throw new ArgumentException("CSN参数不能为负数！");
            }

            CSN = csn;
        }


        /// <summary>
        ///     设置CSR
        /// </summary>
        /// <param name="csr">CSR</param>
        public void SetCSR(int csr)
        {
            if (csr < 0)
            {
                throw new ArgumentException("CSR参数不能为负数！");
            }

            CSR = csr;
        }

        /// <summary>
        ///     设置TSN
        /// </summary>
        /// <param name="tsn">TSN</param>
        public void SetTSN(decimal tsn)
        {
            if (tsn < 0)
            {
                throw new ArgumentException("TSN参数不能为负数！");
            }

            TSN = tsn;
        }

        /// <summary>
        ///     设置TSR
        /// </summary>
        /// <param name="tsr">TSR</param>
        public void SetTSR(decimal tsr)
        {
            if (tsr < 0)
            {
                throw new ArgumentException("TSR参数不能为负数！");
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