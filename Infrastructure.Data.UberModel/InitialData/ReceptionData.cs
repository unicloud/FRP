#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/11/22 10:16:40
// 文件名：ReceptionData
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.ReceptionAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class ReceptionData : InitialDataBase
    {
        public ReceptionData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var recetptions = new List<AircraftLeaseReception>
            {
                new AircraftLeaseReception(),
            };
            recetptions.ForEach(m => Context.Receptions.AddOrUpdate(u => u.ReceptionNumber, m));
        }
    }
}