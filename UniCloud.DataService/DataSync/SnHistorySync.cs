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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.DataService.Connection;
using UniCloud.DataService.DataSync.Model;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class SnHistorySync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork;
        private const int Size = 300;

        public SnHistorySync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<SnHistory> AmasisDatas { get; protected set; }
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
            var Removals = new List<Removal>();
            var Installations = new List<Installation>();
            var SnHistories=new List<SnHistory>();

            const string strSqlForRemoval =
                "select RTRIM(D.BONUMDOC1)||RTRIM(D.BONUMDOC2) ACTIONNO,Date(RTRIM(D.BOA4)||'-'||RTRIM(D.BOMM)||'-'||RTRIM(D.BOJJ))  REMOVEDATE,D.PSPN PN,D.PSSN SN,AC.AVIMMATR REGNUMBER from amsfcscval.frbodpf AS D LEFT JOIN AMSFCSCVAL.FRBOPF AS P ON D.BONUMDOC1=P.BONUMDOC1 AND D.BONUMDOC2=P.BONUMDOC2 LEFT JOIN AMSFCSCVAL.FRAV AS  AC ON P.BONUMAP=AC.AVNUMAP WHERE P.BOCODMM<='80' AND  P.BOCODMM>='70';";
            const string strSqlForInstallation = "SELECT RTRIM(INS.BONUMDOC1)||RTRIM(INS.BONUMDOC2) ACTIONNO,Date(RTRIM(INS.BOA4)||'-'||RTRIM(INS.BOMM)||'-'||RTRIM(INS.BOJJ))  INSTALLDATE,PSPN PN,PSSN SN,AC.AVIMMATR REGNUMBER FROM AMSFCSCVAL.FRBOPPF AS INS LEFT JOIN AMSFCSCVAL.FRBOPF AS P ON INS.BONUMDOC1=P.BONUMDOC1 AND INS.BONUMDOC2=P.BONUMDOC2 LEFT JOIN AMSFCSCVAL.FRAV AS  AC ON P.BONUMAP=AC.AVNUMAP WHERE P.BOCODMM<='80' AND  P.BOCODMM>='70';";
            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                Removals = conn.GetSqlDatas<Removal>(strSqlForRemoval).ToList();
                Installations = conn.GetSqlDatas<Installation>(strSqlForInstallation).ToList();
                //拼接装机历史

            }
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
                    foreach (var pn in AmasisDatas.Skip(i * Size).Take(count))
                    {
                        var dbPn = FrpDatas.FirstOrDefault(p => p.Pn == pn.Pn);
                        if (dbPn != null) //数据库已有对应的件号
                        {

                        }
                        else
                        {

                        }
                    }
                    _unitOfWork.Commit();
                }
            }
        }
    }
}
