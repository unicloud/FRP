#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    ///     SnReg工厂
    /// </summary>
    public static class SnRegFactory
    {
        #region 创建

        /// <summary>
        ///     创建序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>序号件</returns>
        public static SnReg CreateSnReg(
            DateTime installDate,
            PnReg pnReg,
            string sn,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var snReg = new SnReg
            {
                InstallDate = installDate,
                Sn = sn,
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            snReg.GenerateNewIdentity();
            snReg.SetPnReg(pnReg);
            snReg.CreateDate = DateTime.Now;
            snReg.UpdateDate = DateTime.Now;

            return snReg;
        }

        /// <summary>
        ///     创建发动机序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="thrust"></param>
        /// <param name="sn">序号</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>发动机序号件</returns>
        public static EngineReg CreateEngineReg(
            DateTime installDate,
            PnReg pnReg,
            Thrust thrust,
            string sn,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var engineReg = new EngineReg
            {
                InstallDate = installDate,
                Sn = sn,
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            engineReg.GenerateNewIdentity();
            engineReg.SetPnReg(pnReg);
            engineReg.SetThrust(thrust);
            engineReg.CreateDate = DateTime.Now;
            engineReg.UpdateDate = DateTime.Now;

            return engineReg;
        }

        /// <summary>
        ///     创建APU序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>APU序号件</returns>
        public static APUReg CreateAPUReg(
            DateTime installDate,
            PnReg pnReg,
            string sn,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var apuReg = new APUReg
            {
                InstallDate = installDate,
                Sn = sn,
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            apuReg.GenerateNewIdentity();
            apuReg.SetPnReg(pnReg);
            apuReg.CreateDate = DateTime.Now;
            apuReg.UpdateDate = DateTime.Now;

            return apuReg;
        }

        #endregion

        #region 更新

        /// <summary>
        ///     更新序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>序号件</returns>
        public static SnReg UpdateSnReg(
            DateTime installDate,
            PnReg pnReg,
            string sn,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var snReg = new SnReg
            {
                InstallDate = installDate,
                Sn = sn,
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            snReg.GenerateNewIdentity();
            snReg.SetPnReg(pnReg);
            snReg.UpdateDate = DateTime.Now;

            return snReg;
        }

        /// <summary>
        ///     更新发动机序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="thrust"></param>
        /// <param name="sn">序号</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>发动机序号件</returns>
        public static EngineReg UpdateEngineReg(
            DateTime installDate,
            PnReg pnReg,
            Thrust thrust,
            string sn,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var engineReg = new EngineReg
            {
                InstallDate = installDate,
                Sn = sn,
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            engineReg.GenerateNewIdentity();
            engineReg.SetPnReg(pnReg);
            engineReg.SetThrust(thrust);
            engineReg.UpdateDate = DateTime.Now;

            return engineReg;
        }

        /// <summary>
        ///     更新APU序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>APU序号件</returns>
        public static APUReg UpdateAPUReg(
            DateTime installDate,
            PnReg pnReg,
            string sn,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var apuReg = new APUReg
            {
                InstallDate = installDate,
                Sn = sn,
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            apuReg.GenerateNewIdentity();
            apuReg.SetPnReg(pnReg);
            apuReg.UpdateDate = DateTime.Now;

            return apuReg;
        }

        #endregion

    }
}