#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/26 14:10:15
// 文件名：FlightLogQuery
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/26 14:10:15
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.FleetPlanBC.DTO.DataTransfer;
using UniCloud.Application.FlightLogBC.DTO;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FlightLogBC.Query.FlightLogQueries
{
    public class FlightLogQuery : IFlightLogQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public FlightLogQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     飞行日志查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞行日志DTO集合。</returns>
        public IQueryable<FlightLogDTO> FlightLogDTOQuery(
            QueryBuilder<FlightLog> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<FlightLog>()).Select(p => new FlightLogDTO
            {
                Id = p.Id,
                AcReg = p.AcReg,
                MSN = p.MSN,
                FlightDate = p.FlightDate,
                FlightHours = p.FlightHours,
                TotalCycles = p.TotalCycles,
            });
        }

        /// <summary>
        ///    查询近一个月飞机的飞行数据
        /// </summary>
        /// <param name="regNumber"></param>
        /// <param name="flightDate"></param>
        /// <returns></returns>
        public List<AcFlightDataDTO> QueryAcFlightData(string regNumber, DateTime flightDate)
        {
            var acFlightDatas = new List<AcFlightDataDTO>();
            DateTime start = flightDate.AddDays(-30);
            DateTime end = flightDate.AddDays(1);
            var flightLogs = _unitOfWork.CreateSet<FlightLog>()
                    .Where(p => p.AcReg == regNumber && p.FlightDate.CompareTo(end) < 0 &&
                            p.FlightDate.CompareTo(start) > 0).ToList();
            if (flightLogs.Count() != 0)
            {
                decimal sumHours = 0;
                int sumCycle = 0;
                for (int i = 0; i < 30; i++)
                {
                    var date = flightDate.AddDays(-i);
                    var selFlightLogs = flightLogs.Where(
                           p => p.FlightDate.CompareTo(date.AddDays(1)) < 0 && p.FlightDate.CompareTo(date.AddDays(-1)) > 0).ToList();
                    selFlightLogs.ForEach(p =>
                    {
                        sumHours += p.FlightHours;
                        sumCycle += 1;
                    });
                    acFlightDatas.Add(new AcFlightDataDTO
                    {
                        FlightDate = date,
                        FlightCycle = sumCycle,
                        FlightHour = sumHours,
                        RegNumber = regNumber,
                    });
                }
            }
            return acFlightDatas;
        }
    }
}
