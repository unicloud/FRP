#region ÃüÃû¿Õ¼ä

using System.Data.Entity.Migrations;
using UniCloud.Infrastructure.Data.UberModel.InitialData;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;
//using UniCloud.Infrastructure.Data.UberModel.InitialData;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UberModelUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UberModelUnitOfWork context)
        {
            InitialContainer.CreateInitialContainer()
                .Register(new XmlConfigData(context))
                .Register(new SupplierData(context))
                .Register(new ManagerData(context))
                .Register(new ManufacturerData(context))
                .Register(new ProgrammingData(context))
                .Register(new AnnualData(context))
                .Register(new AircraftCategoryData(context))
                .Register(new AircraftTypeData(context))
                .Register(new AcTypeData(context))
                .Register(new AirlinesData(context))
                .Register(new ForwardData(context))
                .Register(new TradeData(context))
                .Register(new PartData(context))
                .Register(new ActionCategoryData(context))
                .Register(new DocumentData(context))
                .InitialData();        
        }
    }
}