#region 命名空间

using System;
using System.ServiceProcess;
using System.Threading;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using Unicloud.DocumentIndexService;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.Repositories;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace DocumentIndexWinService
{
    public partial class DocumentIndexWinService : ServiceBase
    {
        private Timer _indexTimer; // 事件定时器

        public DocumentIndexWinService()
        {
            InitializeComponent();
            InitializeContainer();
        }

        protected override void OnStart(string[] args)
        {
            var intervalTime = new TimeSpan(1, 0, 0, 0, 0);
            var delayTime = DateTime.Now.AddDays(1).Date - DateTime.Now;
            _indexTimer = new Timer(UpdateDocumentIndex, null, delayTime.Ticks, intervalTime.Ticks);
        }

        protected override void OnStop()
        {
            _indexTimer.Dispose();
        }

        private void UpdateDocumentIndex(object sender)
        {
            var updateIndex = new DocumentIndex();
            updateIndex.UpdateIndex();
        }

        private static void InitializeContainer()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, CommonServiceBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IDocumentAppService, DocumentAppService>()
                .Register<IDocumentQuery, DocumentQuery>()
                .Register<IDocumentRepository, DocumentRepository>()
                .Register<IDocumentTypeRepository, DocumentTypeRepository>();
        }
    }
}