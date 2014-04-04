using System;
using System.ServiceProcess;
using System.Threading;
using UniCloud.UserDataService;

namespace UserSyncWinService
{
    public partial class UserWinService : ServiceBase
    {
        private Timer _userTimer;  // 事件定时器
        private Timer _organizationTimer;
        public UserWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            var intervalTime = new TimeSpan(1, 0, 0, 0, 0);
            TimeSpan delayTime = DateTime.Now.AddDays(1).Date - DateTime.Now;
            _userTimer = new Timer(AsyncUserData, null, delayTime.Ticks, intervalTime.Ticks);
            _organizationTimer = new Timer(AsyncOrganizationData, null, delayTime.Ticks, intervalTime.Ticks);

        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            _userTimer.Dispose();
            _organizationTimer.Dispose();
        }

        #region DataAsync

        private void AsyncUserData(object sender)
        {
            var userDataSync = new UserDataSync();
            userDataSync.SyncUserInfo();
        }

        private void AsyncOrganizationData(object sender)
        {
            var userDataSync = new UserDataSync();
            userDataSync.SyncOrganizationInfo();
        }
        #endregion
    }
}
