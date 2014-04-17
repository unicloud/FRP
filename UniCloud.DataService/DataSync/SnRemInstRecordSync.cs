#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/16 22:32:58
// 文件名：SnRemInstRecordSync
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
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class SnRemInstRecordSync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork;

        public SnRemInstRecordSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<SnRemInstRecordDTO> AmasisDatas { get; protected set; }
        public IEnumerable<SnRemInstRecordDTO> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                @"SELECT RTRIM(P.BONUMDOC1)||RTRIM(P.BONUMDOC2) ACTIONNO,RTRIM(K.KXLIBELLE) POSITION FROM AMSFCSCVAL.FRBOPF AS P LEFT JOIN AMSFCSCVAL.FRKXS AS K ON P.BOPOSAV=K.KXPOSAV WHERE P.BOCODMM<='80' AND  P.BOCODMM>='70';";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<SnRemInstRecordDTO>(strSql);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql =
                @"SELECT ID,SN,INSTALLDATE,PN,ISSTOP,PNREGID,AIRCRAFTID FROM FRP.FRP.SNREG";

            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<SnRemInstRecordDTO>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                var datas = _unitOfWork.CreateSet<SnRemInstRecord>();
                foreach (SnRemInstRecordDTO snRemInstRecord in AmasisDatas)
                {
                    if (snRemInstRecord.RegNumber != null)
                    {
                        if (snRemInstRecord != null)
                        {
                            SnRemInstRecord sn = SnRemInstRecordFactory.CreateSnRemInstRecord(snRemInstRecord.ActionNo, snRemInstRecord.ActionDate, snRemInstRecord.ActionType,
                                snRemInstRecord.Position, snRemInstRecord.Reason,aircraft);
                            _unitOfWork.SnRegs.Add(sn);
                        }
                    }
                }
            }
            _unitOfWork.SaveChanges();
        }
    }
}
