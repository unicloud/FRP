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
            };

            plans.ForEach(p => Context.Plans.Add(p));
        }
    }
}
