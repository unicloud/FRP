#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 15:14:59
// 文件名：SnRegSync
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
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class SnRegSync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork;

        public SnRegSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<SnRegDTO> AmasisDatas { get; protected set; }
        public IEnumerable<SnRegDTO> FrpDatas { get; protected set; }
        public IEnumerable<PnReg> PnRegDatas { get; protected set; }

        public IEnumerable<Aircraft> AircraftDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                @"SELECT RTRIM(fr.PSPN) PN,RTRIM(fr.PSSN) SN,Date(RTRIM(fr.PSA4)||'-'||RTRIM(fr.PSMM)||'-'||RTRIM(fr.PSJJ))  INSTALLDATE,0 ISSTOP,RTRIM(ac.avofficiel) RegNumber  FROM amsfcscval.FRPS as fr left join amsfcscval.frav as ac on fr.PSNUMAP=ac.avnumap";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<SnRegDTO>(strSql);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql =
                @"SELECT ID,SN,INSTALLDATE,PN,ISSTOP,PNREGID,AIRCRAFTID FROM FRP.FRP.SNREG";
            const string strPnSql =
                @"SELECT [ID],[PN],[ISLIFE] FROM [FRP].[FRP].[PNREG]";
            const string strAcSql =
                @"SELECT ID,REGNUMBER FROM FRP.FRP.AIRCRAFT";
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<SnRegDTO>(strSql);
                PnRegDatas = conn.GetSqlDatas<PnReg>(strPnSql);
                AircraftDatas = conn.GetSqlDatas<Aircraft>(strAcSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                DbSet<SnReg> datas = _unitOfWork.CreateSet<SnReg>();
                foreach (SnRegDTO snReg in AmasisDatas)
                {
                    if (snReg.RegNumber != null)
                    {
                        Aircraft aircraft = AircraftDatas.FirstOrDefault(p =>
                                p.RegNumber.Substring(p.RegNumber.Length - 4, 4) ==
                                snReg.RegNumber.Substring(snReg.RegNumber.Length - 4, 4));
                        PnReg pnReg = PnRegDatas.ToList().FirstOrDefault(p => p.Pn == snReg.Pn.Trim());
                        if (pnReg != null)
                        {
                            SnReg sn = SnRegFactory.CreateSnReg(aircraft, snReg.InstallDate, snReg.IsStop, pnReg, snReg.Sn);
                            _unitOfWork.SnRegs.Add(sn);
                        }
                    }
                }
            }
            _unitOfWork.SaveChanges();
        }
    }
}