using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniCloud.Fleet.XmlConfigs;

namespace FleetXmlWinService
{
    partial class XmlConfigWindowsService : ServiceBase
    {

        private Thread XmlConfigThread;
        private bool XmlConfigFlag = true;

        public XmlConfigWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            StartUpdateXmlConfig();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            StopUpdateXmlConfig();

        }

        private void WriteLog(string Content)
        {
            try
            {
                System.Diagnostics.Trace.Write(Content);
            }
            catch
            {
            }
        }

        private void StopUpdateXmlConfig()
        {
            try
            {
                XmlConfigThread.Abort();

                XmlConfigFlag = false;

                WriteLog("线程停止");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void StartUpdateXmlConfig()
        {
            try
            {

                XmlConfigThread = new Thread(new ThreadStart(UpdateXmlConfig));
                XmlConfigThread.Start();
                WriteLog("线程任务开始");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                WriteLog("线程任务开始失败");
            }
        }


        private void UpdateXmlConfig()
        {
            while (XmlConfigFlag)
            {
                try
                {
                    XmlConfigService _XmlService = new XmlConfigService();
                    _XmlService.UpdateAllXmlConfigContent();
                }
                catch (Exception e)
                {
                    WriteLog(e.Message);

                }
                Thread.Sleep(30000);
            }
        }

    }
}
