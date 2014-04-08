#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 16:18:42
// 文件名：AtaSync
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

#endregion

namespace UniCloud.DataService.DataSync
{
    public class AtaSync : DataSync
    {
        public IEnumerable<PnRegDTO> AmasisDatas { get; protected set; }
        public IEnumerable<PnRegDTO> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                "SELECT RTRIM(P.NMPN) PN,RTRIM(P.NMATA) ATA,RTRIM(P.NMCODFAB) VENDOR,RTRIM(P.NMDESIGN) DESCRIPTION," +
                "RTRIM(P.NMCODTYPM) FROM AMSFCSCVAL.FRNMPF P WHERE P.NMCODTYPM != '1'";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<PnRegDTO>(strSql);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql =
                @"SELECT [ID],[PN],[PN_CLASS],[VENDOR],[UPDATE_TIME] as UpdateTime,[UPDATEBY],[STATE],[SPECPN],
                                   [ISLIFE],[DESCRIPTION],[TRAIN_RATE] as TrainRate,[ATA] FROM [FRP].[FRP].[PNREG]";
                //从UniCloud.Component中取数据
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<PnRegDTO>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
        }
    }
}