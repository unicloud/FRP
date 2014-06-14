#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:37:24
// 文件名：ManufacturerData
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
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class ManufacturerData : InitialDataBase
    {
        public ManufacturerData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var manufacturers = new List<Manufacturer>
            {
                ManufacturerFactory.CreateManufacturer(Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"), "波音", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"), "空中客车", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("EC88B5C8-A882-4724-8B0C-ED1CE44DE736"), "庞巴迪", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("338CE613-E742-4A4A-85AC-7744A4D38F47"), "多尼尔", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("2A2B2E9B-ED8F-4D00-9D12-CF264012206B"), "巴西航空工业公司", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("AF10EF52-0CD3-4A4F-8AEB-03979F1B62A3"), "西安飞机工业公司", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("BC67BB37-3C52-4C6A-96FD-5EF8E88D19EA"), "中国商飞", 1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("40A9F375-A798-4B0C-A004-CCD890FDD658"), "俄罗斯图波列夫公司",1),
                ManufacturerFactory.CreateManufacturer(Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608D"), "罗罗", 2),
            };
            manufacturers.ForEach(a => Context.Manufacturers.AddOrUpdate(u => u.Id, a));
        }
    }
}