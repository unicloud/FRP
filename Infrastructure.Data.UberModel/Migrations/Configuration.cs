#region ÃüÃû¿Õ¼ä

//using UniCloud.Infrastructure.Data.UberModel.InitialData;
using System.Data.Entity.Migrations;
using UniCloud.Infrastructure.Data.UberModel.InitialData;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UberModelUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, UberModelUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql");
        }

        protected override void Seed(UberModelUnitOfWork context)
        {
            InitialContainer.CreateInitialContainer()
                .Register(new AuthData(context))
                .Register(new XmlConfigData(context))
                .Register(new XmlSettingData(context))
                .Register(new LicenseTypeData(context))
                .Register(new SupplierData(context))
                .Register(new ManagerData(context))
                .Register(new ManufacturerData(context))
                .Register(new ProgrammingData(context))
                .Register(new AnnualData(context))
                .Register(new AircraftCabinTypeData(context))
                .Register(new AircraftCategoryData(context))
                .Register(new CAACAircraftTypeData(context))
                .Register(new AircraftTypeData(context))
                .Register(new AircraftSeriesData(context))
                .Register(new AirlinesData(context))
                //.Register(new AircraftData(context))
                .Register(new ForwardData(context))
                //.Register(new TradeData(context))
                //.Register(new PartData(context))
                .Register(new ActionCategoryData(context))
                .Register(new DocumentTypeData(context))
                .Register(new DocumentData(context))
                .Register(new IssuedUnitData(context))
                .Register(new EngineTypeData(context))
                .InitialData();
        }
    }
}