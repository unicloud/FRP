#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 21:45:57
// 文件名：AircraftCabinTypeData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 21:45:57
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class AircraftCabinTypeData : InitialDataBase
    {
        public AircraftCabinTypeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化飞机舱位类型相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var aircraftCabinTypes = new List<AircraftCabinType>
            {
                AircraftCabinTypeFactory.CreateAircraftCabinType(1,"头等舱"),
                AircraftCabinTypeFactory.CreateAircraftCabinType(2,"商务舱"),
                AircraftCabinTypeFactory.CreateAircraftCabinType(3,"经济舱"),
            };
            aircraftCabinTypes.ForEach(a => Context.AircraftCabinTypes.AddOrUpdate(u => u.Id, a));
        }
    }
}
