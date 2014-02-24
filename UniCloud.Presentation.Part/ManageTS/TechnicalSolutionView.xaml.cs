using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.ManageTS
{
    [Export]
    public partial class TechnicalSolutionView : UserControl
    {
        public TechnicalSolutionView()
        {
            InitializeComponent();
        }

        [Import(typeof (TechnicalSolutionVm))]
        public TechnicalSolutionVm ViewModel
        {
            get { return DataContext as TechnicalSolutionVm; }
            set { DataContext = value; }
        }
    }
}