#region 命名空间

using System;
using System.ServiceProcess;
using System.Threading;
using UniCloud.Application.BaseManagementBC.OrganizationServices;
using UniCloud.Application.BaseManagementBC.Query.OrganizationQueries;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;
using UniCloud.UserDataService;

#endregion

namespace UserSyncWinService
{
    public partial class UserWinService : ServiceBase
    {
        private Timer _organizationTimer;
        private Timer _userTimer; // 事件定时器

        public UserWinService()
        {
            InitializeComponent();
            InitializeContainer();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            var intervalTime = new TimeSpan(1, 0, 0, 0, 0);
            var delayTime = DateTime.Now.AddDays(1).Date - DateTime.Now;
            _userTimer = new Timer(AsyncUserData, null, delayTime.Ticks, intervalTime.Ticks);
            _organizationTimer = new Timer(AsyncOrganizationData, null, delayTime.Ticks, intervalTime.Ticks);
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            _userTimer.Dispose();
            _organizationTimer.Dispose();
        }

        private static void InitializeContainer()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, BaseManagementBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IUserAppService, UserAppService>()
                .Register<IUserQuery, UserQuery>()
                .Register<IUserRepository, UserRepository>()
                .Register<IOrganizationAppService, OrganizationAppService>()
                .Register<IOrganizationQuery, OrganizationQuery>()
                .Register<IOrganizationRepository, OrganizationRepository>();
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