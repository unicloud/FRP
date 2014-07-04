#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/5 11:05:34
// 文件名：AircraftPlanData
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
using UniCloud.Domain.UberModel.Aggregates.AircraftPlanAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class PlanData : InitialDataBase
    {
        public PlanData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化飞机系列相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var plans = new List<Plan>
            {
                PlanFactory.CreatePlan(Guid.Parse("3791B2DA-1E62-47CD-8115-0DC248765B89"),"2013年度机队资源规划",true,1,true,DateTime.Parse("2013-01-24 10:15:18.5568033"),DateTime.Parse("2012-11-19 09:24:23.9077292"),true,3,3,
                Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"),Guid.Parse("3B33DB65-A404-4D77-9885-F259854D0FC4"))
            };

            plans.ForEach(p => Context.Plans.Add(p));
        }
    }
}
