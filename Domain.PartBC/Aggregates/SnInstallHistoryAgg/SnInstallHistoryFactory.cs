#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 15:51:13
// 文件名：SnInstallHistoryFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnInstallHistoryAgg
{
    /// <summary>
    ///     SnInstallHistory工厂。
    /// </summary>
    public static class SnInstallHistoryFactory
    {
        /// <summary>
        ///     创建序号件装机历史。
        /// </summary>
        /// <returns>SnInstallHistory</returns>
        public static SnInstallHistory CreateSnInstallHistory()
        {
            var snInstallHistory = new SnInstallHistory();
            snInstallHistory.GenerateNewIdentity();
            return snInstallHistory;
        }

        /// <summary>
        ///     创建序号件装机历史。
        /// </summary>
        /// <param name="snReg">序号件</param>
        /// <param name="pnReg">附件</param>
        /// <param name="csn">CSN，自装机以来使用循环</param>
        /// <param name="csr">CSR，自上一次修理以来使用循环</param>
        /// <param name="tsn">TSN，自装机以来使用小时数</param>
        /// <param name="tsr">TSR，自上一次修理以来使用小时数</param>
        /// <param name="aircraft">装机所在飞机</param>
        /// <param name="installDate">装上日期</param>
        /// <param name="removeDate">拆下日期</param>
        /// <param name="installReason">装上日期</param>
        /// <param name="removeReason">拆下日期</param>
        /// <returns></returns>
        public static SnInstallHistory CreateSnInstallHistory(SnReg snReg, PnReg pnReg, int csn, int csr, decimal tsn,
            decimal tsr, Aircraft aircraft,
            DateTime installDate, DateTime? removeDate,string installReason,string removeReason)
        {
            var snInstallHistory = new SnInstallHistory();
            snInstallHistory.CreateDate = DateTime.Now;
            snInstallHistory.GenerateNewIdentity();
            snInstallHistory.SetAircraft(aircraft);
            snInstallHistory.SetInstallDate(installDate);
            snInstallHistory.SetRemoveDate(removeDate);
            snInstallHistory.SetSn(snReg);
            snInstallHistory.SetPn(pnReg);
            snInstallHistory.SetCSN(csn);
            snInstallHistory.SetCSR(csr);
            snInstallHistory.SetTSN(tsn);
            snInstallHistory.SetTSR(tsr);
            snInstallHistory.SetInstallReason(installReason);
            snInstallHistory.SetRemoveReason(installReason);
            return snInstallHistory;
        }
    }
}