#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/13 22:03:39
// 文件名：EngineTypeData
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/13 22:03:39
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.EngineTypeAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class EngineTypeData : InitialDataBase
    {
        public EngineTypeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化飞机舱位类型相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var engineTypes = new List<EngineType>
            {
               EngineTypeFactory.CreateEngineType("V2500",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608D")),
               EngineTypeFactory.CreateEngineType("V2700",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608D")),
            };
            engineTypes.ForEach(a => Context.EngineTypes.AddOrUpdate(u => u.Id, a));
        }
    }
}
