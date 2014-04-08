#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 9:50:12
// 文件名：ProgrammingData
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
using UniCloud.Domain.UberModel.Aggregates.ProgrammingAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class ProgrammingData : InitialDataBase
    {
        public ProgrammingData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var programming = new List<Programming>
            {
                ProgrammingFactory.CreateProgramming(Guid.Parse("58F6DAA2-F09F-4991-9D04-25ED94916A75"),"十二五规划",new DateTime(2011, 1, 1),new DateTime(2015, 12, 31)),
                ProgrammingFactory.CreateProgramming(Guid.Parse("A408C4AF-40C4-4944-A5A7-A3FFA3F765D2"),"十三五规划",new DateTime(2016, 1, 1),new DateTime(2020, 12, 31)),
                ProgrammingFactory.CreateProgramming(Guid.Parse("AC883FB1-6F61-4BE5-AECD-08D624FE536F"),"十四五规划",new DateTime(2021, 1, 1),new DateTime(2025, 12, 31)),
            };
            programming.ForEach(a => Context.Programmings.AddOrUpdate(u => u.Id, a));
        }
    }
}