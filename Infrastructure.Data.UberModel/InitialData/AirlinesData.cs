#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:49:04
// 文件名：AirlinesData
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class AirlinesData : InitialDataBase
    {
        public AirlinesData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var airlineses = new List<Airlines>
            {
            };
            airlineses.ForEach(a => Context.Airlineses.AddOrUpdate(u => u.Id, a));
        }
    }
}
