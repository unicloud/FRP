using System;
using System.ServiceProcess;
using System.Threading;
using UniCloud.MerchantDataService;

namespace MerchantSyncWinService
{
    public partial class MerchantWinService : ServiceBase
    {
        private Timer _supplierTimer;  // 事件定时器
        private Timer _bankAccountTimer;
        private Timer _linkmanTimer;
        public MerchantWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var intervalTime = new TimeSpan(1, 0, 0, 0, 0);
            TimeSpan delayTime = DateTime.Now.AddDays(1).Date - DateTime.Now;
            _linkmanTimer = new Timer(AsyncLinkman, null, delayTime.Ticks, intervalTime.Ticks);
            _supplierTimer = new Timer(AsyncSupplier, null, delayTime.Add(new TimeSpan(0,1,0,0)).Ticks, intervalTime.Ticks);
            _bankAccountTimer = new Timer(AsyncBankAccount, null, delayTime.Add(new TimeSpan(0, 3, 0, 0)).Ticks, intervalTime.Ticks);
        }

        protected override void OnStop()
        {
            _supplierTimer.Dispose();
            _bankAccountTimer.Dispose();
            _linkmanTimer.Dispose();
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
