#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 16:16:13
// 文件名：AircraftSeriesSync
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.DataService.Connection;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class AircraftSeriesSync : DataSync
    {
        public IEnumerable<AircraftSeriesDTO> AmasisDatas { get; protected set; }
        public IEnumerable<AircraftSeriesDTO> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql = "SELECT RTRIM(R.PROTCOD) NAME,RTRIM(R.PROTLIB) DESCRIPTION FROM AMSFCSCVAL.FMPROT R";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<AircraftSeriesDTO>(strSql);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql = @"SELECT [ID],[NAME],[DESCRIPTION] FROM [FRP].[FRP].[AircraftSeries]"; 

            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<AircraftSeriesDTO>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
        }
    }
}