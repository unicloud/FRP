using System;
using System.ServiceProcess;
using System.Threading;
using Unicloud.DocumentIndexService;

namespace DocumentIndexWinService
{
    public partial class DocumentIndexWinService : ServiceBase
    {
        private Timer _indexTimer;  // 事件定时器
        public DocumentIndexWinService()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            var intervalTime = new TimeSpan(1, 0, 0, 0, 0);
            TimeSpan delayTime = DateTime.Now.AddDays(1).Date - DateTime.Now;
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
    }
}
