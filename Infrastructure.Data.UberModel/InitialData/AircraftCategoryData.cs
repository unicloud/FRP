#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:37:39
// 文件名：AircraftCategoryData
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
using UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class AircraftCategoryData : InitialDataBase
    {
        public AircraftCategoryData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var aircraftCategories = new List<AircraftCategory>
            {
                AircraftCategoryFactory.CreateAircraftCategory(Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD"),"客机","250座以上客机"),
                AircraftCategoryFactory.CreateAircraftCategory(Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1"),"客机","100-200座客机"),
                AircraftCategoryFactory.CreateAircraftCategory(Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B"),"客机","100座以下客机"),
                AircraftCategoryFactory.CreateAircraftCategory(Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC"),"货机","大型货机"),
                AircraftCategoryFactory.CreateAircraftCategory(Guid.Parse("984C4402-6FE7-42AC-894D-1C60624B2B09"),"货机","中型货机"),
            };
            aircraftCategories.ForEach(a => Context.AircraftCategories.AddOrUpdate(u => u.Id, a));
        }
    }
}
