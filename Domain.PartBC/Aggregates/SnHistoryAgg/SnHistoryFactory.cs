#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 15:51:13
// 文件名：SnHistoryFactory
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
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg
{
    /// <summary>
    ///     SnHistory工厂。
    /// </summary>
    public static class SnHistoryFactory
    {
        /// <summary>
        ///     创建序号件装机历史。
        /// </summary>
        /// <returns>SnHistory</returns>
        public static SnHistory CreateSnHistory()
        {
            var snHistory = new SnHistory();
            snHistory.GenerateNewIdentity();
            return snHistory;
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
        /// <param name="installRecord">装上记录</param>
        /// <param name="removeRecord">拆下记录</param>
        /// <returns></returns>
        public static SnHistory CreateSnHistory(SnReg snReg, PnReg pnReg, int csn, int csr, decimal tsn,
            decimal tsr, Aircraft aircraft,
            DateTime installDate, DateTime? removeDate, SnRemInstRecord installRecord, SnRemInstRecord removeRecord)
        {
            var snHistory = new SnHistory();
            snHistory.CreateDate = DateTime.Now;
            snHistory.GenerateNewIdentity();
            snHistory.SetAircraft(aircraft);
            snHistory.SetInstallDate(installDate);
            snHistory.SetRemoveDate(removeDate); 
            snHistory.SetSn(snReg);
            snHistory.SetPn(pnReg);
            snHistory.SetCSN(csn);
            snHistory.SetCSR(csr);
            snHistory.SetTSN(tsn);
            snHistory.SetTSR(tsr);
            snHistory.SetInstallRecord(installRecord);
            snHistory.SetRemoveRecord(removeRecord);
            return snHistory;
        }
    }
}