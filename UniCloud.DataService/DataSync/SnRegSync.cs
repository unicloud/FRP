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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniCloud.DataService.Connection;
using UniCloud.DataService.DataSync.Model;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using Aircraft = UniCloud.Domain.PartBC.Aggregates.AircraftAgg.Aircraft;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class SnRegSync : DataSync
    {
        private const int Size = 300;
        private readonly PartBCUnitOfWork _unitOfWork;

        public SnRegSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<PartSn> AmasisDatas { get; protected set; }
        public List<SnReg> FrpDatas { get; protected set; }
        public List<PnReg> PnRegDatas { get; protected set; }

        public List<Aircraft> AircraftDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                "SELECT RTRIM(SN.PSPN) PN,RTRIM(SN.PSSN) SN,RTRIM(SN.PSNUMAP) SERIALNUMBER, RTRIM(AC.AVIMMATR) REGNUMBER,SN.PSCODSIT STATUS,DATE(RTRIM(SN.PSA4)||'-'||RTRIM(SN.PSMM)||'-'||RTRIM(SN.PSJJ))  LATESTREMOVEDATE,SN.PSCSN CSN,SN.PSCSO CSO,SN.PSTSN TSN,SN.PSTSO TSO,AT.ATACD ATA FROM AMSFCSCVAL.FRPS AS SN " +
                " LEFT JOIN AMSFCSCVAL.FRAV AS AC ON AC.AVNUMAP=SN.PSNUMAP LEFT JOIN AMSFCSCVAL.FRNMPF PN ON RTRIM(SN.PSPN)=RTRIM(PN.NMPN) LEFT JOIN AMSFCSCVAL.FRATA AS AT ON AT.ATACD=PN.NMATA  WHERE Pn.NMCODTYPM != '1' AND Pn.NMATA <'81' AND Pn.NMATA >='70'";
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<PartSn>(strSql);
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
                int times = AmasisDatas.Count() / Size;
                for (int i = 0; i < times + 1; i++)
                {
                    int count = i == times ? AmasisDatas.Count() - i * Size : Size;
                    foreach (PartSn partSn in AmasisDatas.Skip(i * Size).Take(count))
                    {
                        SnReg dbSn = FrpDatas.FirstOrDefault(p => p.Sn == partSn.Sn);
                        if (dbSn != null) //数据库已有对应的序号件
                        {
                            //更新飞机
                            if (partSn.RegNumber != null)
                            {
                                Aircraft aircraft = AircraftDatas.FirstOrDefault(p => 
                                    p.RegNumber.Substring(p.RegNumber.Length - 4, 4) == partSn.RegNumber.Substring(partSn.RegNumber.Length - 4, 4));
                                if (aircraft == null)
                                {
                                    //记录日志“飞机为空”
                                }
                                else if (dbSn.AircraftId != aircraft.Id)
                                {
                                    dbSn.SetAircraft(aircraft); //更新已有序号件的所在飞机
                                }
                            }

                            //更新部件
                            if (partSn.Pn != null)
                            {
                                PnReg pnReg = PnRegDatas.ToList().FirstOrDefault(p => p.Pn == partSn.Pn.Trim());

                                if (pnReg == null)
                                {
                                    //记录日志“对应的附件没有维护到机队系统中”
                                }
                                else if (dbSn.PnRegId != pnReg.Id)
                                {
                                    dbSn.SetPnReg(pnReg); //更新已有序号件的所在飞机
                                }
                            }
                            //更新序号件状态
                            if (partSn.Status != null)
                            {
                                var statusArray = partSn.Status.ToCharArray();
                                int status = (statusArray[0] - 48)*10 + (statusArray[1] - 48);
                                if (status != (int)dbSn.Status)
                                    dbSn.SetSnStatus((SnStatus)status);
                            }
                        }
                        else
                        {
                            if (partSn.Pn != null)
                            {
                                PnReg pnReg = PnRegDatas.ToList().FirstOrDefault(p => p.Pn == partSn.Pn.Trim());
                                SnReg newSn=null;
                                //SnReg newSn = SnRegFactory.CreateSnReg(DateTime.Now, pnReg, partSn.Sn, decimal.Parse(partSn.TSN),
                                //    0, decimal.Parse(partSn.CSN), 0); //创建新的附件
                                if (partSn.RegNumber != null)
                                {
                                    Aircraft aircraft = AircraftDatas.FirstOrDefault(p =>
                                        p.RegNumber.Substring(p.RegNumber.Length - 4, 4) ==
                                        partSn.RegNumber.Substring(partSn.RegNumber.Length - 4, 4));
                                    if (aircraft == null)
                                    {
                                        //记录日志“飞机为空”
                                    }
                                    else
                                    {
                                        newSn.SetAircraft(aircraft); //设置序号件的所在飞机
                                    }
                                }
                                //newSn.SetIsLife(false);//默认设置为非寿控件
                                //newSn.SetIsStop(false);//默认设置为未停用
                                //更新序号件状态
                                if (partSn.Status != null)
                                {
                                    var statusArray = partSn.Status.ToCharArray();
                                    int status = (statusArray[0]-48)*10 + (statusArray[1]-48);
                                    newSn.SetSnStatus((SnStatus)status);
                                }
                                _unitOfWork.SnRegs.Add(newSn);
                            }
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
        }
    }
}