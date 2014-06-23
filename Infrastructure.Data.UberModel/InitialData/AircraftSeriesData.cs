#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:49:32
// 文件名：AircraftSeriesData
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
using UniCloud.Domain.UberModel.Aggregates.AircraftSeriesAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class AircraftSeriesData : InitialDataBase
    {
        public AircraftSeriesData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化飞机系列相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var aircraftSeries = new List<AircraftSeries>
            {
              AircraftSeriesFactory.CreateAircraftSeries(Guid.Parse("AB65EE49-D110-40F1-B3CE-52CADB0C6B81"), "A320系列",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C")),
              AircraftSeriesFactory.CreateAircraftSeries(Guid.Parse("5C690CB2-2D33-4006-858B-0BE610E9CB47"), "A330系列",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C")),
            };

            aircraftSeries.ForEach(p => Context.AircraftSeries.Add(p));
        }
    }
}