using System;
using System.Windows.Forms;
using UniCloud.DataService.DataSync;

namespace TestWindowsService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //1、同步FlightLog数据
                FlightLogSync();
            }
            catch
            {
            }
        }

        #region

        private void FlightLogSync()
        {
            var dataSync = new AircraftSeriesSync();
            dataSync.DataSynchronous();
        }

        #endregion
    }
}