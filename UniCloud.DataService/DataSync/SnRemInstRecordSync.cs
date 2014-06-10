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
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class SnRemInstRecordSync : DataSync
    {
        private const int Size = 300;
        private readonly PartBCUnitOfWork _unitOfWork;

        public SnRemInstRecordSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<SnRemInstRecordDTO> AmasisDatas { get; protected set; }
        public List<SnRemInstRecord> FrpDatas { get; protected set; }
        public List<Aircraft> AircraftDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                @"SELECT RTRIM(P.BONUMDOC1)||RTRIM(P.BONUMDOC2) ACTIONNO,RTRIM(K.KXLIBELLE) POSITION,RTRIM(AC.AVOFFICIEL) REGNUMBER FROM AMSFCSCVAL.FRBOPF AS P LEFT JOIN AMSFCSCVAL.FRAV AC ON P.BONUMAP=AC.AVNUMAP LEFT JOIN AMSFCSCVAL.FRKXS AS K ON P.BOPOSAV=K.KXPOSAV WHERE P.BOCODMM<='80' AND  P.BOCODMM>='70';";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<SnRemInstRecordDTO>(strSql).ToList();
            }
        }

        public override void ImportFrpData()
        {
            FrpDatas = _unitOfWork.CreateSet<SnRemInstRecord>().ToList();
            AircraftDatas = _unitOfWork.CreateSet<Aircraft>().ToList();
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                var times = AmasisDatas.Count/Size;
                for (var i = 0; i < times + 1; i++)
                {
                    var count = i == times ? AmasisDatas.Count - i*Size : Size;
                    foreach (var snRemInstRecord in AmasisDatas.Skip(i*Size).Take(count))
                    {
                        var aircraft = AircraftDatas.FirstOrDefault(p =>
                            p.RegNumber.Substring(p.RegNumber.Length - 4, 4) ==
                            snRemInstRecord.RegNumber.Substring(snRemInstRecord.RegNumber.Length - 4, 4));
                        var dbSnRemInstRecord = FrpDatas.FirstOrDefault(p => p.ActionNo == snRemInstRecord.ActionNo);
                        if (dbSnRemInstRecord != null) //数据库已有对应的拆换记录
                        {
                            if (dbSnRemInstRecord.ActionDate != snRemInstRecord.ActionDate)
                            {
                                dbSnRemInstRecord.SetActionDate(snRemInstRecord.ActionDate); //更新已有拆换记录的拆换时间
                            }

                            if (dbSnRemInstRecord.AircraftId != snRemInstRecord.AircraftId)
                            {
                                dbSnRemInstRecord.SetActionDate(snRemInstRecord.ActionDate); //更新已有拆换记录的拆换时间
                            }
                            if (aircraft == null)
                            {
                                //日志记录“拆装记录关联的飞机出错”
                            }
                            else if (dbSnRemInstRecord.AircraftId != aircraft.Id)
                                dbSnRemInstRecord.SetAircraft(aircraft);
                        }
                        else
                        {
                            var newSnRemInstRecord =
                                SnRemInstRecordFactory.CreateSnRemInstRecord(snRemInstRecord.ActionNo, DateTime.Now, 1,
                                    null, aircraft); //创建新的拆换记录
                            FrpDatas.Add(newSnRemInstRecord);
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
            _unitOfWork.SaveChanges();
        }
    }
}