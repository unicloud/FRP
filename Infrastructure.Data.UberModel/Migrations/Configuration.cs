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
            //InitialContainer.CreateInitialContainer()
            //                .Register(new SupplierData(context))
            //                .Register(new AcTypeData(context))
            //                .Register(new ForwardData(context))
            //                .Register(new TradeData(context))
            //                .Register(new PartData(context))
            //                .Register(new ActionCategoryData(context))
            //                .InitialData();
        }
    }
}