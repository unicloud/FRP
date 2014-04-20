#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/17 9:24:24
// 文件名：SnHistorySync
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.DataService.Connection;
using UniCloud.DataService.DataSync.Model;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class SnHistorySync : DataSync
    {
        private const int Size = 300;
        private readonly PartBCUnitOfWork _unitOfWork;

        public SnHistorySync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<Movement> Removals { get; protected set; }

        public List<Movement> Installations { get; protected set; }

        public List<SnHistory> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            GetSnHistoryFromAmasis();
        }

        public override void ImportFrpData()
        {
            FrpDatas = _unitOfWork.CreateSet<SnHistory>().ToList();
        }

        public void GetSnHistoryFromAmasis()
        {
            //依照AMASIS中的拆装类型来换分，拆下的类型mpcodtdoc包括：X3,X6,TODO:川航提供相应的信息之后，待完善
            const string strSqlForRemoval =
                "select mp.pspn PN,mp.pssn SN,Date(RTRIM(mp.mpA4)||'-'||RTRIM(mp.mpMM)||'-'||RTRIM(mp.mpJJ))  MOVEmentDATE,mp.mpnumdoc1||mp.mpnumdoc2 ACtionNo,AC.AVIMMATR REGNUMBER,mp.mpcodtdoc DocType,parm.parlib DOCDescription,mp.mpcodtop MOVEType,mv.mvtlib Description,mp.Usrcre CreateDate,mp.HEucre CreateTIme,mp.DatMaj UpdateDate,mp.HEUMAJ updateTIme  from amsfcscval.frmp mp left join mglg091561.frmvl as mv on mv.mvtcod=mp.mpcodtop left join mglg091561.ifrparm as parm on mp.mpcodtdoc=parm.parfic and parm.parnom='MVTDOC'  left join AMSFCSCVAL.FRBOPF AS P ON mp.mpNUMDOC1=P.BONUMDOC1 AND mp.mpNUMDOC2=P.BONUMDOC2 LEFT JOIN AMSFCSCVAL.FRAV AS  AC ON P.BONUMAP=AC.AVNUMAP WHERE P.BOCODMM<='80' AND  P.BOCODMM>='70' ORDER BY MPNUMDOC2;";
            //依照AMASIS中的拆装类型来换分，装机相关的移动类型mpcodtdoc包括：Y1,TODO：川航提供相应的信息之后，待完善
            const string strSqlForInstallation =
                "select mp.pspn PN,mp.pssn SN,Date(RTRIM(mp.mpA4)||'-'||RTRIM(mp.mpMM)||'-'||RTRIM(mp.mpJJ))  MOVEmentDATE,mp.mpnumdoc1||mp.mpnumdoc2 ACtionNo,AC.AVIMMATR REGNUMBER,mp.mpcodtdoc DocType,parm.parlib DOCDescription,mp.mpcodtop MOVEType,mv.mvtlib Description,mp.Usrcre CreateDate,mp.HEucre CreateTIme,mp.DatMaj UpdateDate,mp.HEUMAJ updateTIme  from amsfcscval.frmp mp left join mglg091561.frmvl as mv on mv.mvtcod=mp.mpcodtop left join mglg091561.ifrparm as parm on mp.mpcodtdoc=parm.parfic and parm.parnom='MVTDOC'  left join AMSFCSCVAL.FRBOPF AS P ON mp.mpNUMDOC1=P.BONUMDOC1 AND mp.mpNUMDOC2=P.BONUMDOC2 LEFT JOIN AMSFCSCVAL.FRAV AS  AC ON P.BONUMAP=AC.AVNUMAP WHERE P.BOCODMM<='80' AND  P.BOCODMM>='70' ORDER BY MPNUMDOC2;";
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                Removals = conn.GetSqlDatas<Movement>(strSqlForRemoval).ToList();

                Installations = conn.GetSqlDatas<Movement>(strSqlForInstallation).ToList();
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            //现将获取的装机记录更新到数据库
            if (Installations.Any())
            {
                int times = Installations.Count / Size;
                for (int i = 0; i < times + 1; i++)
                {
                    int count = i == times ? Installations.Count - i * Size : Size;
                    foreach (var move in Installations.Skip(i * Size).Take(count))
                    {
                        //装机对应的拆换指令
                        var snRemInstRecord =
                            _unitOfWork.CreateSet<SnRemInstRecord>().FirstOrDefault(p => p.ActionNo == move.ActionNo);
                        if (snRemInstRecord != null)
                        {
                            SnHistory dbSnHistory =
                                FrpDatas.FirstOrDefault(p => p.Sn == move.Sn && p.RemoveRecordId == snRemInstRecord.Id);
                            var aircraft =
                                _unitOfWork.CreateSet<Aircraft>()
                                    .FirstOrDefault(p => p.RegNumber == move.RegNumber);
                            var pnReg = _unitOfWork.CreateSet<PnReg>().FirstOrDefault(p => p.Pn == move.Pn);
                            var snReg = _unitOfWork.CreateSet<SnReg>().FirstOrDefault(p => p.Sn == move.Sn);

                            if (dbSnHistory != null) //数据库已有对应的装机历史，更新相应的装机记录
                            {
                                if (aircraft == null)
                                {
                                    //记录错误日志
                                }
                                else if (dbSnHistory.AircraftId != aircraft.Id) //更新飞机
                                {
                                    dbSnHistory.SetAircraft(aircraft);
                                }
                                if (pnReg == null)
                                {
                                    //记录错误日志
                                }
                                else if (dbSnHistory.Pn != pnReg.Pn) //更新件号
                                {
                                    dbSnHistory.SetPn(pnReg);
                                }

                                if (dbSnHistory.InstallDate != move.MovementDate)
                                {
                                    dbSnHistory.SetInstallDate(move.MovementDate);
                                }
                            }
                            else
                            {
                                var newSnHistory = SnHistoryFactory.CreateSnHistory(snReg, pnReg, 0, 0, 0, 0, aircraft,
                                    move.MovementDate, null, snRemInstRecord, null);
                                _unitOfWork.SnHistories.Add(newSnHistory);
                            }
                        }
                    }
                    _unitOfWork.Commit();
                }
            }

            ImportFrpData();//重新获取数据库中的所有装机历史
            //拆下的记录需要从数据库找到相同拆换指令号、相同飞机的的装机历史，再更新这个装机历史的拆下记录
            if (Removals.Any())
            {
                int times = Removals.Count / Size;
                for (int i = 0; i < times + 1; i++)
                {
                    int count = i == times ? Removals.Count - i * Size : Size;
                    foreach (Movement move in Removals.Skip(i * Size).Take(count))
                    {
                        //拆件对应的拆换指令
                        var snRemInstRecord =
                            _unitOfWork.CreateSet<SnRemInstRecord>().FirstOrDefault(p => p.ActionNo == move.ActionNo);
                        if (snRemInstRecord != null)
                        {
                            var aircraft = _unitOfWork.CreateSet<Aircraft>().FirstOrDefault(p => p.RegNumber == move.RegNumber);
                            //根据序号件、飞机和拆下时间，先找到所有在这个拆下时间之前的装上记录，按时间排序，选择最接近这次拆下的上次装机记录
                            if (aircraft == null)
                            {
                                //记录错误日志
                            }
                            else
                            {
                                var dbSnHistory =
                                    FrpDatas.Where(p => p.Sn == move.Sn && p.AircraftId == aircraft.Id && p.InstallDate <= move.MovementDate).ToList().OrderBy(p => p.InstallDate).LastOrDefault();
                                if (dbSnHistory != null)//更新这条记录的拆下历史
                                {
                                    dbSnHistory.SetRemoveDate(move.MovementDate);
                                    dbSnHistory.SetRemoveRecord(snRemInstRecord);
                                }
                            }

                        }
                    }
                    _unitOfWork.Commit();
                }
            }
        }
    }
}