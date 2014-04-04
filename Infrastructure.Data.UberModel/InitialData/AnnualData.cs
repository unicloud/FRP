#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:38:12
// 文件名：AnnualData
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
using UniCloud.Domain.UberModel.Aggregates.AnnualAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class AnnualData : InitialDataBase
    {
        public AnnualData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var annuals = new List<Annual>
            {
                AnnualFactory.CreateAnnual(Guid.Parse("7FB161A0-03A9-469E-8760-9BB71F077CF9"),Guid.Parse("58F6DAA2-F09F-4991-9D04-25ED94916A75"),2011),
                AnnualFactory.CreateAnnual(Guid.Parse("C538D43C-7767-4702-9E6C-2CC6DDD7763A"),Guid.Parse("58F6DAA2-F09F-4991-9D04-25ED94916A75"),2012),
                AnnualFactory.CreateAnnual(Guid.Parse("3B33DB65-A404-4D77-9885-F259854D0FC4"),Guid.Parse("58F6DAA2-F09F-4991-9D04-25ED94916A75"),2013),
                AnnualFactory.CreateAnnual(Guid.Parse("CA19B813-4945-4016-8A4A-EAD22088EA67"),Guid.Parse("58F6DAA2-F09F-4991-9D04-25ED94916A75"),2014),
                AnnualFactory.CreateAnnual(Guid.Parse("661195B2-BCD8-4112-9F75-354636753767"),Guid.Parse("58F6DAA2-F09F-4991-9D04-25ED94916A75"),2015),
                AnnualFactory.CreateAnnual(Guid.Parse("41ED96E5-6040-49BB-89BF-9124866010FB"),Guid.Parse("A408C4AF-40C4-4944-A5A7-A3FFA3F765D2"),2016),
                AnnualFactory.CreateAnnual(Guid.Parse("601154A5-F40E-4D3A-B7AE-ABA177DA85EF"),Guid.Parse("A408C4AF-40C4-4944-A5A7-A3FFA3F765D2"),2017),
                AnnualFactory.CreateAnnual(Guid.Parse("764F8A6E-8A62-417A-9360-17E89473CBA0"),Guid.Parse("A408C4AF-40C4-4944-A5A7-A3FFA3F765D2"),2018),
                AnnualFactory.CreateAnnual(Guid.Parse("E025438F-6FE6-4257-9FBE-3C3761B38DCA"),Guid.Parse("A408C4AF-40C4-4944-A5A7-A3FFA3F765D2"),2019),
                AnnualFactory.CreateAnnual(Guid.Parse("4504ED7B-31E3-4D44-9725-CE357240FCB5"),Guid.Parse("A408C4AF-40C4-4944-A5A7-A3FFA3F765D2"),2020),
                AnnualFactory.CreateAnnual(Guid.Parse("D6E690B8-CDA6-479F-A4EA-15B2D9D5DD71"),Guid.Parse("AC883FB1-6F61-4BE5-AECD-08D624FE536F"),2021),
                AnnualFactory.CreateAnnual(Guid.Parse("10126124-EA36-4684-A93B-1FB81A586EED"),Guid.Parse("AC883FB1-6F61-4BE5-AECD-08D624FE536F"),2022),
                AnnualFactory.CreateAnnual(Guid.Parse("F37A6748-444E-4D28-9543-1EAECDC39927"),Guid.Parse("AC883FB1-6F61-4BE5-AECD-08D624FE536F"),2023),
                AnnualFactory.CreateAnnual(Guid.Parse("96E0DDBC-0DE7-4DB8-BE4B-4BEF4141FA7A"),Guid.Parse("AC883FB1-6F61-4BE5-AECD-08D624FE536F"),2024),
                AnnualFactory.CreateAnnual(Guid.Parse("08ED931A-94F7-4006-B501-96E5BB609E3C"),Guid.Parse("AC883FB1-6F61-4BE5-AECD-08D624FE536F"),2025),
            };
            annuals.ForEach(a => Context.Annuals.AddOrUpdate(u => u.Id, a));
        }
    }
}
