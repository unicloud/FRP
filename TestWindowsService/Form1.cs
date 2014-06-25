#region 命名空间

using System;
using System.Windows.Forms;
using UniCloud.DataService.DataSync;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace TestWindowsService
{
    public partial class Form1 : Form
    {
        private AircraftConfigBCUnitOfWork _acConfigUnitOfWork;
        private FlightLogBCUnitOfWork _flightLogUnitofWork;
        private PartBCUnitOfWork _partUnitofWork;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InitialContext();
                //AircraftSeriesSync();
                //1、同步FlightLog数据
                FlightLogSync();
                //2、同步PnReg数据
                PnRegSync();
                //3、同步SnRegs数据
                SnRegSync();
                //4、同步SnRemInstRecord数据(拆换记录)
                //SnRemInstRecordSync();
                //5.同步装机历史
                //SnHistorySync();
                DisposeContext();
            }
            catch (Exception exception)
            {
                DisposeContext();
                MessageBox.Show(exception.Message);
            }
        }

        private void InitialContext()
        {
            _acConfigUnitOfWork = new AircraftConfigBCUnitOfWork();
            _partUnitofWork = new PartBCUnitOfWork();
            _flightLogUnitofWork = new FlightLogBCUnitOfWork();
        }

        private void DisposeContext()
        {
            _acConfigUnitOfWork.Dispose();
            _partUnitofWork.Dispose();
            _flightLogUnitofWork.Dispose();
        }

        #region

        private void ItemSync()
        {
            var dataSync = new ItemSync();
            dataSync.DataSynchronous();
        }

        private void FlightLogSync()
        {
            var dataSync = new FlightLogSync();
            dataSync.DataSynchronous();
        }

        private void AircraftSeriesSync()
        {
            var dataSync = new AircraftSeriesSync();
            dataSync.DataSynchronous();
        }

        private void PnRegSync()
        {
            var dataSync = new PnRegSync();
            dataSync.DataSynchronous();
        }

        private void SnRegSync()
        {
            var dataSync = new SnRegSync();
            dataSync.DataSynchronous();
        }

        private void SnRemInstRecordSync()
        {
            var dataSync = new SnRemInstRecordSync();
            dataSync.DataSynchronous();
        }

        private void SnHistorySync()
        {
            var dataSync = new SnHistorySync();
            dataSync.DataSynchronous();
        }

        #endregion
    }
}