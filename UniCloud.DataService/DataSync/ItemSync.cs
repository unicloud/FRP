#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/15 14:01:01
// 文件名：ItemSync
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
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class ItemSync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork = UniContainer.Resolve<PartBCUnitOfWork>();


        public List<Item> AmasisDatas { get; protected set; }
        public List<Item> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                "SELECT 1 as ID,RTRIM(KXLIBELLE) NAME,RTRIM(KXPOSAV) ITEMNO,RTRIM(KXFIN) FINUMBER, 1 ISLIFE,RTRIM(KXLIBELLE) DESCRIPTION FROM AMSFCSCVAL.FRKXS";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<Item>(strSql).ToList();
            }
        }

        public override void ImportFrpData()
        {
            const string strSql =
                @"SELECT [Id],[Name],[ItemNo],[FiNumber],[IsLife],[Description] FROM [FRP].[FRP].[Item]";
            //从FRP中取数据
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = _unitOfWork.CreateSet<Item>().ToList();
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                var datas = _unitOfWork.CreateSet<Item>();

                foreach (var amasisItem in AmasisDatas)
                {
                    var item = ItemFactory.CreateItem(amasisItem.Name, amasisItem.ItemNo, amasisItem.FiNumber,
                        amasisItem.Description, amasisItem.IsLife);
                    datas.Add(item);
                }
            }
            _unitOfWork.Commit();
        }
    }
}