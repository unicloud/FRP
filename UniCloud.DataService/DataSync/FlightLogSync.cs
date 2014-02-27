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

using System.Collections.Generic;
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class FlightLogSync : DataSync
    {
        public IEnumerable<FlightLog> AmasisDatas { get; protected set; }
        public IEnumerable<FlightLog> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                "SELECT RTRIM(A.AVSN) MSN,RTRIM(A.AVNUMAP) SN,RTRIM(A.AVOFFICIEL) ACREG,LOG.AVHVAB TotalFH,LOG.AVHVBB TotalBH," +
                "LOG.AVCY TotalCycles,LOG.RMCPT1F ApuMM,LOG.RMCPT2F ApuCycle,LEG.RMA4 FD_YEAR,LEG.RMMM FD_MONTH,LEG.RMJJ FD_DAY," +
                "Date(RTRIM(LEG.RMA4)||'-'||RTRIM(LEG.RMMM)||'-'||RTRIM(LEG.RMJJ))  FlightDate," +
                "RTRIM(LEG.RMDOC) LOGNO,RTRIM(LEG.VOORD) LEGNO,RTRIM(LEG.VOVOLNUM) FlightNum,RTRIM(LEG.VOAERODEP) DepartureAirport,RTRIM(LEG.VOBBDEP) BlockOn," +
                "RTRIM(LEG.VOABDEP) TakeOff,RTRIM(LEG.VOAEROARR) ArrivalAirport,RTRIM(LEG.VOABARR) Landing,RTRIM(LEG.VOBBARR) BlockStop,1 AS Cycle," +
                "LEG.VOTOGO ToGoNumber,RTRIM(LEG.VOABTPS) FH_HHMM,RTRIM(LEG.VOABTPSC) FlightHours,RTRIM(LEG.VOBBTPS) BLOCK_HHMM,RTRIM(LEG.VOBBTPSC) BlockHours," +
                "LEG.VOOILMO1D ENG1OilDep,LEG.VOOILMO1A ENG1OilArr,LEG.VOOILMO2D ENG2OilDep,LEG.VOOILMO2A ENG2OilArr,LEG.VOOILAPUD ApuOilDep," +
                "LEG.VOOILAPUA ApuOilArr FROM AMSFCSCVAL.FRVO AS LEG LEFT JOIN AMSFCSCVAL.FRRM AS LOG ON  LEG.AVNUMAP = LOG.AVNUMAP AND LEG.RMDOC = LOG.RMDOC," +
                "AMSFCSCVAL.FRAV A WHERE A.AVNUMAP = LEG.AVNUMAP AND (Char(Date(RTRIM(LEG.RMA4)||'-'||RTRIM(LEG.RMMM)||'-'||RTRIM(LEG.RMJJ)))> Char(current date -3 Day))" +
                "  ORDER by AcReg,FlightDate desc,TotalFH desc ";
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<FlightLog>(strSql);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql = @"SELECT ID,[FlightNum],[LogNo],[LegNo],[AcReg],[MSN],[FlightType],[FlightDate]
                                    ,[BlockOn],[TakeOff],[Landing],[BlockStop],[TotalFH],[TotalBH],[FlightHours],[ApuCycle],[BlockHours]
                                    ,[TotalCycles],[Cycle],[DepartureAirport],[ArrivalAirport],[ToGoNumber],[ApuMM],[ENG1OilDep]
                                    ,[ENG1OilArr],[ENG2OilDep],[ENG2OilArr],[ApuOilDep],[ApuOilArr] FROM [FRP].[FRP].[FlightLog]                                    
                                    where FlightDate>Dateadd(Day, -3, GETDATE()) order by AcReg,FlightDate desc,TotalFH desc";//从UniCloud.FRP中取FlightLog数据
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<FlightLog>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
        }
    }
}