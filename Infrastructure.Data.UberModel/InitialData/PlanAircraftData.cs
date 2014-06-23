#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/5 11:05:17
// 文件名：PlanAircraftData
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
using UniCloud.Domain.UberModel.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class PlanAircraftData : InitialDataBase
    {
        public PlanAircraftData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化飞机系列相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var planAircrafts = new List<PlanAircraft>
            {
            };

            planAircrafts.ForEach(p => Context.PlanAircrafts.Add(p));
        }
    }
}
