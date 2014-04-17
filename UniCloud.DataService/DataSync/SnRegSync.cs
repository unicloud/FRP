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
using UniCloud.DataService.DataSync.Model;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Domain.Common.Enums;
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
        private const int Size = 300;

        public SnRegSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<PartSn> AmasisDatas { get; protected set; }
        public List<SnReg> FrpDatas { get; protected set; }
        public List<PnReg> PnRegDatas { get; protected set; }

        public List<Aircraft> AircraftDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql = "select RTRIM(Sn.PSPN) PN,RTRIM(Sn.PSSN) SN,RTRIM(SN.PSNUMAP) SERialNumber, RTRIM(AC.AVIMMATR) RegNumber,Sn.PSCODSIT Status,Date(RTRIM(Sn.PSA4)||'-'||RTRIM(Sn.PSMM)||'-'||RTRIM(Sn.PSJJ))  LatestRmoveDate,Sn.PSCSN CSN,Sn.PSCSO CSO,Sn.PSTSN TSN,Sn.PSTSO TSO,At.ATACD ATA from amsfcscval.frps as Sn" +
                                  "left join amsfcscval.frav as AC on Ac.AVNUmAP=Sn.PSNUMAP Left join AMSFCSCVAL.FRNMPF Pn On RTRIM(SN.PSPN)=RTRIM(PN.NMPN) left join Amsfcscval.FRATA as At on at.ATACD=Pn.NMATA  WHERE Pn.NMCODTYPM != '1' AND Pn.NMATA <'81' AND Pn.NMATA >='70'";
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<PartSn>(strSql).ToList();
            }
        }

        public override void ImportFrpData()
        {
            FrpDatas = _unitOfWork.CreateSet<SnReg>().ToList();
            PnRegDatas = _unitOfWork.CreateSet<PnReg>().ToList();
            AircraftDatas = _unitOfWork.CreateSet<Aircraft>().ToList();
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                var times = AmasisDatas.Count / Size;
                for (var i = 0; i < times + 1; i++)
                {
                    var count = i == times ? AmasisDatas.Count - i * Size : Size;
                    foreach (var partSn in AmasisDatas.Skip(i * Size).Take(count))
                    {
                        Aircraft aircraft = AircraftDatas.FirstOrDefault(p =>
                            p.RegNumber.Substring(p.RegNumber.Length - 4, 4) ==
                            partSn.RegNumber.Substring(partSn.RegNumber.Length - 4, 4));
                        PnReg pnReg = PnRegDatas.ToList().FirstOrDefault(p => p.Pn == partSn.Pn.Trim());

                        var dbSn = FrpDatas.FirstOrDefault(p => p.Sn == partSn.Sn);
                        if (dbSn != null) //数据库已有对应的序号件
                        {
                            if (aircraft == null)
                            {
                                //记录日志“飞机为空”
                            }
                            else if (dbSn.AircraftId != aircraft.Id)
                            {
                                dbSn.SetAircraft(aircraft); //更新已有序号件的所在飞机
                            }

                            if (pnReg == null)
                            {
                                //记录日志“对应的附件没有维护到机队系统中”
                            }
                            else if (dbSn.PnRegId != pnReg.Id)
                            {
                                dbSn.SetPnReg(pnReg); //更新已有序号件的所在飞机
                            }

                            if(partSn.Status==41 && dbSn.Status!=SnStatus.在位)
                                dbSn.SetSnStatus(SnStatus.在位);
                            //else if(partSn.Status!=21 && )

                        }
                        else
                        {
                            var newSn = SnRegFactory.CreateSnReg(partSn.LatestRemoveDate, pnReg, partSn.Sn,partSn.TSN,0,partSn.CSN,0);//创建新的附件
                            FrpDatas.Add(newSn);
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
            _unitOfWork.SaveChanges();
        }
    }
}