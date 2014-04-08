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

namespace AmasisSyncWinService
{
    public partial class AmasisWinService : ServiceBase
    {
        private Thread AmasisAsyncThread;
        private bool AmasisAsyncFlag = true;
        private int Count; //计数器
        private const int StanderTime = 360000; //基准时间为一小时

        public AmasisWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            Count = 0;
            StartUpdateAmasis();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            StopUpdateAmasis();
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

        private void StopUpdateAmasis()
        {
            try
            {
                AmasisAsyncThread.Abort();
                AmasisAsyncFlag = false;
                WriteLog("线程停止");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void StartUpdateAmasis()
        {
            try
            {
                AmasisAsyncThread = new Thread(new ThreadStart(AsyncAmasis));
                AmasisAsyncThread.Start();
                WriteLog("线程任务开始");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                WriteLog("线程任务开始失败");
            }
        }

        private void AsyncAmasis()
        {
            while (AmasisAsyncFlag)
            {
                try
                {
                    Count += 1;
                    if (Count >= 60) Count = 0; //防止计数器无限增加
                    if (Count % 2 == 0)
                    {
                        AsyncFlightLog();
                    }
                    if (Count % 3 == 0)
                    {

                    }
                }
                catch (Exception e)
                {
                    WriteLog(e.Message);

                }

                Thread.Sleep(StanderTime); //两分钟运算一次
            }
        }

        #region DataAsync

        private void AsyncFlightLog()
        {
            //var flightLog = new FlightLog();
            ////从AMASIS中取数据
            //flightLog.Import();
            ////往UniCloud.FRP插入数据
            //var flightAsync = new FlightLogAsync();
            //var strSql = flightLog.GenerateSql();
            //flightAsync.Async(strSql);
        }
        #endregion
    }
}
