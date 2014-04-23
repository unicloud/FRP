#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/22 18:02:44
// 文件名：IssuedUnitData
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
using UniCloud.Domain.UberModel.Aggregates.IssuedUnitAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class IssuedUnitData : InitialDataBase
    {
        public IssuedUnitData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var issuedUnits = new List<IssuedUnit>
            {
                IssuedUnitFactory.CreateIssuedUnit("民航局",false),
                IssuedUnitFactory.CreateIssuedUnit("发改委",false),
                IssuedUnitFactory.CreateIssuedUnit("川航集团",true),
                IssuedUnitFactory.CreateIssuedUnit("川航公司",true),
                IssuedUnitFactory.CreateIssuedUnit("企管部",true),
                IssuedUnitFactory.CreateIssuedUnit("财务部",true),
                IssuedUnitFactory.CreateIssuedUnit("机务工程部",true),
            };
            issuedUnits.ForEach(a => Context.IssuedUnits.AddOrUpdate(u => u.CnName, a));
        }
    }
}
