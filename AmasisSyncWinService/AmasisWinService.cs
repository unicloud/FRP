#region 命名空间

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using UniCloud.DataService.DataSync;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace AmasisSyncWinService
{
    public partial class AmasisWinService : ServiceBase
    {
        private AircraftConfigBCUnitOfWork _acConfigUnitOfWork;
        private FlightLogBCUnitOfWork _flightLogUnitofWork;
        private PartBCUnitOfWork _partUnitofWork;

        public AmasisWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            timer1.Enabled = true;
            timer2.Enabled = true;
            WriteLog("服务启动！");
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            WriteLog("服务停止！");
        }

        private void WriteLog(string content)
        {
            try
            {
                Trace.Write(content);
            }
            catch
            {
            }
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy) //每天同步一次数据
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!backgroundWorker2.IsBusy) //每两小时同步一次数据
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _flightLogUnitofWork = new FlightLogBCUnitOfWork();
                //同步Flight数据
                FlightLogSync();
                _flightLogUnitofWork.Dispose();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                _acConfigUnitOfWork = new AircraftConfigBCUnitOfWork();
                _partUnitofWork = new PartBCUnitOfWork();
                //同步附件数据
                PnRegSync();
                //同步序号件数据
                SnRegSync();
                //同步序号件拆换记录头数据
                SnRemInstRecordSync();
                //同步拆换历史记录
                SnHistorySync();
                timer2.Enabled = true;
                _acConfigUnitOfWork.Dispose();
                _partUnitofWork.Dispose();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        private void FlightLogSync()
        {
            var dataSync = new FlightLogSync(_flightLogUnitofWork);
            dataSync.DataSynchronous();
        }


        private void PnRegSync()
        {
            var dataSync = new PnRegSync(_partUnitofWork);
            dataSync.DataSynchronous();
        }

        private void SnRegSync()
        {
            var dataSync = new SnRegSync(_partUnitofWork);
            dataSync.DataSynchronous();
        }

        private void SnRemInstRecordSync()
        {
            var dataSync = new SnRemInstRecordSync(_partUnitofWork);
            dataSync.DataSynchronous();
        }

        private void SnHistorySync()
        {
            var dataSync = new SnHistorySync(_partUnitofWork);
            dataSync.DataSynchronous();
        }
    }
}