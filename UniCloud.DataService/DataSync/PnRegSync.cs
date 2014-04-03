#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 15:14:48
// 文件名：PnRegSync
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
using System.Data.Entity.Migrations;
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class PnRegSync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork;

        public PnRegSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


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
            const string strSql = @"SELECT [ID],[PN],[ISLIFE] FROM [FRP].[FRP].[PNREG]";
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<PnRegDTO>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                DbSet<PnReg> datas = _unitOfWork.CreateSet<PnReg>();

                foreach (PnRegDTO pnReg in AmasisDatas)
                {
                    PnReg pn = PnRegFactory.CreatePnReg(pnReg.IsLife, pnReg.Pn);
                    pn.CreateDate = DateTime.Now;
                    datas.AddOrUpdate(p => p.Pn, pn);
                }
            }
            _unitOfWork.Commit();
        }
    }
}