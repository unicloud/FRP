#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export]
    public partial class BankAccountWindow 
    {
        public BankAccountWindow()
        {
            InitializeComponent();
        }

        [Import]
        public BankAccountWindowVm ViewModel
        {
            get { return DataContext as BankAccountWindowVm; }
            set { DataContext = value; }
        }
    }
}
