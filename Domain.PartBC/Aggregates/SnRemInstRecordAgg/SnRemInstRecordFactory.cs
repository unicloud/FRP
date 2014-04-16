#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/15 21:30:25
// 文件名：SnRemInstRecordFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg
{
    /// <summary>
    ///     SnRemInstRecord工厂。
    /// </summary>
    public static class SnRemInstRecordFactory
    {
        /// <summary>
        ///     创建序号件拆装记录。
        /// </summary>
        /// <returns>SnRemInstRecord</returns>
        public static SnRemInstRecord CreateSnRemInstRecord()
        {
            var snRemInstRecord = new SnRemInstRecord();
            snRemInstRecord.GenerateNewIdentity();
            return snRemInstRecord;
        }

        /// <summary>
        /// 创建序号件拆装记录
        /// </summary>
        /// <param name="actionNo">拆装指令号</param>
        /// <param name="actionDate">拆装日期</param>
        /// <param name="actionType">拆装类型</param>
        /// <param name="position">拆装位置</param>
        /// <param name="reason">拆装原因</param>
        /// <param name="aircraft">飞机</param>
        /// <returns></returns>
        public static SnRemInstRecord CreateSnRemInstRecord(string actionNo, DateTime actionDate, int actionType, string position,
            string reason, Aircraft aircraft)
        {
            var snRemInstRecord = new SnRemInstRecord();
            snRemInstRecord.GenerateNewIdentity();
            snRemInstRecord.SetActionDate(actionDate);
            snRemInstRecord.SetActionNo(actionNo);
            snRemInstRecord.SetActionType((ActionType)actionType);
            snRemInstRecord.SetPosition(position);
            snRemInstRecord.SetReason(reason);
            snRemInstRecord.SetAircraft(aircraft);
            return snRemInstRecord;
        }
    }
}
