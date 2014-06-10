#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，17:11
// 文件名：PartData.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;
using UniCloud.Domain.UberModel.Aggregates.PartAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class PartData : InitialDataBase
    {
        public PartData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var parts = new List<Part>
                {
                    new Part
                        {
                            Name = "罗技123",
                            Pn = "PNG123456"
                        },
                    new Part
                        {
                            Name = "罗技124",
                            Pn = "PNG123457"
                        }
                };
            parts.ForEach(p => Context.Parts.Add(p));
            parts.ForEach(p => Context.Materials.Add(new BFEMaterial
            {
                Description = "描述",
                Name=p.Name,
                Pn = p.Pn,
            }));
            parts.ForEach(p => Context.Materials.Add(new EngineMaterial()
            {
                Description = "描述",
                Name = p.Name,
                Pn = p.Pn,
            }));
        }
    }
}