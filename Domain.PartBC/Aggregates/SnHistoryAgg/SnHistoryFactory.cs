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
using UniCloud.Domain.Common.Enums;
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
        /// <param name="tsn">TSN，自装机以来使用小时数</param>
        /// <param name="actionType">操作类型</param>
        /// <param name="aircraft">装机所在飞机</param>
        /// <param name="actionDate">操作日期</param>
        /// <param name="remInstRecord">拆换记录</param>
        /// <param name="csn2">的基础上再累加在库时间折算的使用循环数</param>
        /// <param name="tsn2">的基础上再累加在库时间折算的使用小时数</param>
        /// <param name="status">序号件在历史节点上的状态</param>
        /// <param name="position">位置信息</param>
        /// <returns></returns>
        public static SnHistory CreateSnHistory(SnReg snReg, PnReg pnReg, int csn, decimal tsn,int csn2,decimal tsn2, int actionType,
             Aircraft aircraft,DateTime actionDate, SnRemInstRecord remInstRecord,int status,int position)
        {
            var snHistory = new SnHistory();
            snHistory.CreateDate = DateTime.Now;
            snHistory.GenerateNewIdentity();
            snHistory.SetAircraft(aircraft);
            snHistory.SetActionDate(actionDate);
            snHistory.SetActionType((ActionType)actionType);
            snHistory.SetSn(snReg);
            snHistory.SetPn(pnReg);
            snHistory.SetCSN(csn);
            snHistory.SetTSN(tsn);
            snHistory.SetCSN2(csn2);
            snHistory.SetTSN2(tsn2);
            snHistory.SetSnStatus((SnStatus)status);
            snHistory.SetRemInstRecord(remInstRecord);
            snHistory.SetPosition((Position)position);
            return snHistory;
        }
    }
}