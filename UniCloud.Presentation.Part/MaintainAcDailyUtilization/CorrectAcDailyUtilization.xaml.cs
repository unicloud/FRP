#region 命名空间

using System;
using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.MaintainAcDailyUtilization
{
    [Export]
    public partial class CorrectAcDailyUtilization
    {
        public CorrectAcDailyUtilization()
        {
            InitializeComponent();
            CurrentMonthTextBox.Text = DateTime.Now.ToString("yyyy-MM");
        }

        [Import]
        public CorrectAcDailyUtilizationVm ViewModel
        {
            get { return DataContext as CorrectAcDailyUtilizationVm; }
            set { DataContext = value; }
        }
    }
}