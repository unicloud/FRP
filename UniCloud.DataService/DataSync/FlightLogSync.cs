#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 15:15:12
// 文件名：FlightLogSync
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UniCloud.DataService.Connection;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork.Mapping;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class FlightLogSync : DataSync
    {
        private readonly FlightLogBCUnitOfWork _unitOfWork;

        public FlightLogSync(FlightLogBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FlightLog> AmasisDatas { get; protected set; }
        public IEnumerable<FlightLog> FrpDatas { get; protected set; }

        public string QueryStr { get;protected set; }
        public override void ImportAmasisData()
        {
            const int step = 7;
            const string strSql =
                "SELECT RTRIM(A.AVSN) MSN,RTRIM(A.AVNUMAP) SN,RTRIM(A.AVOFFICIEL) ACREG,LOG.AVHVAB TotalFH,LOG.AVHVBB TotalBH," +
                "LOG.AVCY TotalCycles,LOG.RMCPT1F ApuMM,LOG.RMCPT2F ApuCycle,LEG.RMA4 FD_YEAR,LEG.RMMM FD_MONTH,LEG.RMJJ FD_DAY," +
                "Date(RTRIM(LEG.RMA4)||'-'||RTRIM(LEG.RMMM)||'-'||RTRIM(LEG.RMJJ))  FlightDate," +
                "RTRIM(LEG.RMDOC) LOGNO,RTRIM(LEG.VOORD) LEGNO,RTRIM(LEG.VOVOLNUM) FlightNum,RTRIM(LEG.VOAERODEP) DepartureAirport,RTRIM(LEG.VOBBDEP) BlockOn," +
                "RTRIM(LEG.VOABDEP) TakeOff,RTRIM(LEG.VOAEROARR) ArrivalAirport,RTRIM(LEG.VOABARR) Landing,RTRIM(LEG.VOBBARR) BlockStop,1 AS Cycle," +
                "LEG.VOTOGO ToGoNumber,RTRIM(LEG.VOABTPS) FH_HHMM,RTRIM(LEG.VOABTPSC) FlightHours,RTRIM(LEG.VOBBTPS) BLOCK_HHMM,RTRIM(LEG.VOBBTPSC) BlockHours," +
                "LEG.VOOILMO1D ENG1OilDep,LEG.VOOILMO1A ENG1OilArr,LEG.VOOILMO2D ENG2OilDep,LEG.VOOILMO2A ENG2OilArr,LEG.VOOILAPUD ApuOilDep," +
                "LEG.VOOILAPUA ApuOilArr FROM AMSFCSCVAL.FRVO AS LEG LEFT JOIN AMSFCSCVAL.FRRM AS LOG ON  LEG.AVNUMAP = LOG.AVNUMAP AND LEG.RMDOC = LOG.RMDOC," +
                "AMSFCSCVAL.FRAV A ";
            string queryConditionForSeveralDays = "WHERE A.AVNUMAP = LEG.AVNUMAP AND (Char(Date(RTRIM(LEG.RMA4)||'-'||RTRIM(LEG.RMMM)||'-'||RTRIM(LEG.RMJJ)))> Char(current date -"+step+" Day))" +
                                                  "  ORDER by AcReg,FlightDate desc,TotalFH desc ";
            QueryStr = strSql + queryConditionForSeveralDays;
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<FlightLog>(QueryStr);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql = @"SELECT ID,[FlightNum],[LogNo],[LegNo],[AcReg],[MSN],[FlightType],[FlightDate]
                                    ,[BlockOn],[TakeOff],[Landing],[BlockStop],[TotalFH],[TotalBH],[FlightHours],[ApuCycle],[BlockHours]
                                    ,[TotalCycles],[Cycle],[DepartureAirport],[ArrivalAirport],[ToGoNumber],[ApuMM],[ENG1OilDep]
                                    ,[ENG1OilArr],[ENG2OilDep],[ENG2OilArr],[ApuOilDep],[ApuOilArr] FROM [FRP].[FRP].[FlightLog]                                    
                                    where FlightDate>Dateadd(Day, -7, GETDATE()) order by AcReg,FlightDate desc,TotalFH desc";
            string queryConditionForSeveralDays = "";
            //从FRP中取FlightLog数据
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<FlightLog>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any() && !FrpDatas.Any())
            {
                DbSet<FlightLog> datas = _unitOfWork.CreateSet<FlightLog>();

                foreach (FlightLog flightLog in AmasisDatas)
                {
                    FlightLog fl = FlightLogFactory.CreateFlightLog();
                    fl.AcReg = flightLog.AcReg;
                    fl.ApuCycle = flightLog.ApuCycle;
                    fl.ApuMM = flightLog.ApuMM;
                    fl.ApuOilArr = flightLog.ApuOilArr;
                    fl.ApuOilDep = flightLog.ApuOilDep;
                    fl.ArrivalAirport = flightLog.ArrivalAirport;
                    fl.BlockHours = flightLog.BlockHours;
                    fl.BlockOn = flightLog.BlockOn;
                    fl.BlockStop = flightLog.BlockStop;
                    fl.Cycle = flightLog.Cycle;
                    fl.DepartureAirport = flightLog.DepartureAirport;
                    fl.ENG1OilArr = flightLog.ENG1OilArr;
                    fl.ENG1OilDep = flightLog.ENG1OilDep;
                    fl.ENG2OilArr = flightLog.ENG2OilArr;
                    fl.ENG2OilDep = flightLog.ENG2OilDep;
                    fl.FlightDate = flightLog.FlightDate;
                    fl.FlightHours = flightLog.FlightHours;
                    fl.FlightNum = flightLog.FlightNum;
                    fl.FlightType = flightLog.FlightType;
                    fl.Landing = flightLog.Landing;
                    fl.LegNo = flightLog.LegNo;
                    fl.LogNo = flightLog.LogNo;
                    fl.MSN = flightLog.MSN;
                    fl.TakeOff = flightLog.TakeOff;
                    fl.ToGoNumber = flightLog.ToGoNumber;
                    fl.TotalBH = flightLog.TotalBH;
                    fl.TotalCycles = flightLog.TotalCycles;
                    fl.TotalFH = flightLog.TotalFH;
                    fl.CreateDate = DateTime.Now;
                    datas.Add(fl);
                }
            }
            _unitOfWork.Commit();
        }
    }
}