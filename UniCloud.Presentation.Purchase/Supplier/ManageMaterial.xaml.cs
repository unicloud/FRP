using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export]
    public partial class ManageMaterial : UserControl
    {
        public ManageMaterial()
        {
            InitializeComponent();
        }

        [Import(typeof (ManageMaterialVm))]
        public ManageMaterialVm ViewModel
        {
            get { return DataContext as ManageMaterialVm; }
            set { DataContext = value; }
        }
    }
}