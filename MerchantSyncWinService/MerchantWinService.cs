#region 命名空间

using System;
using System.ServiceProcess;
using System.Threading;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;
using UniCloud.MerchantDataService;

#endregion

namespace MerchantSyncWinService
{
    public partial class MerchantWinService : ServiceBase
    {
        private Timer _bankAccountTimer;
        private Timer _linkmanTimer;
        private Timer _supplierTimer; // 事件定时器

        public MerchantWinService()
        {
            InitializeComponent();
            InitializeContainer();
        }

        protected override void OnStart(string[] args)
        {
            var intervalTime = new TimeSpan(1, 0, 0, 0, 0);
            var delayTime = DateTime.Now.AddDays(1).Date - DateTime.Now;
            _linkmanTimer = new Timer(AsyncLinkman, null, delayTime.Ticks, intervalTime.Ticks);
            _supplierTimer = new Timer(AsyncSupplier, null, delayTime.Add(new TimeSpan(0, 1, 0, 0)).Ticks,
                intervalTime.Ticks);
            _bankAccountTimer = new Timer(AsyncBankAccount, null, delayTime.Add(new TimeSpan(0, 3, 0, 0)).Ticks,
                intervalTime.Ticks);
        }

        protected override void OnStop()
        {
            _supplierTimer.Dispose();
            _bankAccountTimer.Dispose();
            _linkmanTimer.Dispose();
        }

        private static void InitializeContainer()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<ISupplierAppService, SupplierAppService>()
                .Register<ISupplierQuery, SupplierQuery>()
                .Register<ISupplierRepository, SupplierRepository>()
                .Register<ISupplierRoleRepository, SupplierRoleRepository>()
                .Register<ISupplierCompanyRepository, SupplierCompanyRepository>()
                .Register<ILinkmanRepository, LinkmanRepository>()
                .Register<IBankAccountRepository, BankAccountRepository>()
                .Register<ISupplierCompanyMaterialRepository, SupplierCompanyMaterialRepository>();
        }

        #region DataAsync

        private void AsyncSupplier(object sender)
        {
            var supplierDataSync = new MerchantDataSync();
            supplierDataSync.SyncMerchantInfo();
        }

        private void AsyncBankAccount(object sender)
        {
            var bankAccountDataSync = new MerchantDataSync();
            bankAccountDataSync.SyncBankAccountInfo();
        }

        private void AsyncLinkman(object sender)
        {
            var linkmanDataSync = new MerchantDataSync();
            linkmanDataSync.SyncLinkmanInfo();
        }

        #endregion
    }
}