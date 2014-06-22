#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：17:35
// 方案：FRP
// 项目：Infrastructure.Data.FlightLogBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork
{
    public class FlightLogBCUnitOfWork : UniContext<FlightLogBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<FlightLog> _flightLogs;

        public IDbSet<FlightLog> FlightLogs
        {
            get { return _flightLogs ?? (_flightLogs = Set<FlightLog>()); }
        }

        #endregion
    }
}