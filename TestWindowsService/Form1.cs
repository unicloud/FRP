using System;
using System.Windows.Forms;
using UniCloud.DataService.DataSync;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork.Mapping;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

namespace TestWindowsService
{
    public partial class Form1 : Form
    {
        private AircraftConfigBCUnitOfWork _acConfigUnitOfWork;
        private PartBCUnitOfWork _partUnitofWork;
        private FlightLogBCUnitOfWork _flightLogUnitofWork;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InitialContext();
                //1、同步FlightLog数据
                //FlightLogSync();
                //2、同步AircraftSeries数据
                //AircraftSeriesSync();
                //3、同步AircraftSeries数据
                //PnRegSync();
                //4、同步SnReg数据
                SnRegSync();
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

        private void FlightLogSync()
        {
            var dataSync = new FlightLogSync(_flightLogUnitofWork);
            dataSync.DataSynchronous();
        }

        private void AircraftSeriesSync()
        {
            var dataSync = new AircraftSeriesSync(_acConfigUnitOfWork);
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
        #endregion
    }
}