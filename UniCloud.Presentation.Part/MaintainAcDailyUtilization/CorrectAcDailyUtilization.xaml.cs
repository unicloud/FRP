#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.MaintainAcDailyUtilization
{
    [Export(typeof(CorrectAcDailyUtilization))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CorrectAcDailyUtilization : UserControl
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
