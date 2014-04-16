﻿#region 版本信息
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.PartBC.DTO;
using UniCloud.DataService.Connection;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataSync
{
    public class ItemSync : DataSync
    {
        private readonly PartBCUnitOfWork _unitOfWork;

        public ItemSync(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<ItemDTO> AmasisDatas { get; protected set; }
        public IEnumerable<ItemDTO> FrpDatas { get; protected set; }

        public override void ImportAmasisData()
        {
            const string strSql =
                "SELECT 1 as ID,RTRIM(KXLIBELLE) NAME,RTRIM(KXPOSAV) ITEMNO,RTRIM(KXFIN) FINUMBER, 1 ISLIFE,RTRIM(KXLIBELLE) DESCRIPTION FROM AMSFCSCVAL.FRKXS";

            using (var conn = new Db2Conn(GetDb2Connection()))
            {
                AmasisDatas = conn.GetSqlDatas<ItemDTO>(strSql);
            }
        }

        public override void ImportFrpData()
        {
            const string strSql =
                @"SELECT [Id],[Name],[ItemNo],[FiNumber],[IsLife],[Description] FROM [FRP].[FRP].[Item]";
            //从FRP中取数据
            using (var conn = new SqlServerConn(GetSqlServerConnection()))
            {
                FrpDatas = conn.GetSqlDatas<ItemDTO>(strSql);
            }
        }

        public override void DataSynchronous()
        {
            ImportAmasisData();
            ImportFrpData();
            if (AmasisDatas.Any())
            {
                var datas = _unitOfWork.CreateSet<Item>();

                foreach (ItemDTO itemDto in AmasisDatas)
                {
                    Item item = ItemFactory.CreateItem(itemDto.Name, itemDto.ItemNo, itemDto.FiNumber, itemDto.Description, itemDto.IsLife);
                    datas.Add(item);
                }
            }
            _unitOfWork.Commit();
        }
    }
}
