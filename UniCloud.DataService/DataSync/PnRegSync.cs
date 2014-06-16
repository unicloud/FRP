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
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.DataService.DataSync.Model;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class PnRegSync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork;
        private const int Size = 300;

        public PnRegSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<PartPn> AmasisDatas { get; protected set; }
        public List<PnReg> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            GetPnRegFromAmasis();
        }

        public override void ImportFrpData()
        {
            FrpDatas = _unitOfWork.CreateSet<PnReg>().ToList();
        }

        public void GetPnRegFromAmasis()
        {
            const string strSql =
                "SELECT RTRIM(NMPN) PN,RTRIM(NMDESIGN) DESCRIPTION FROM AMSFCSCVAL.FRNMPF WHERE NMATA <'81' AND NMATA >='70'";
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<PartPn>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                var times = AmasisDatas.Count() / Size;
                for (var i = 0; i < times + 1; i++)
                {
                    var count = i == times ? AmasisDatas.Count() - i * Size : Size;
                    foreach (var pn in AmasisDatas.Skip(i * Size).Take(count))
                    {
                        var dbPn = FrpDatas.FirstOrDefault(p => p.Pn == pn.Pn);
                        if (dbPn != null) //数据库已有对应的件号
                        {
                            if (pn.Description != dbPn.Description)
                            {
                                dbPn.SetDescription(pn.Description); //更新已有附件的描述信息
                                dbPn.UpdateDate = DateTime.Now;
                            }
                        }
                        else
                        {
                            var newPn = PnRegFactory.CreatePnReg(false, pn.Pn, pn.Description);//创建新的附件
                            _unitOfWork.CreateSet<PnReg>().Add(newPn);
                        }
                    }
                    _unitOfWork.SaveChanges();
                }
            }
        }
    }
}