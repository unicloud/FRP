using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Part.ManageTS
{
    [Export(typeof (PnRegChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PnRegChildView
    {
        public PnRegChildView()
        {
            InitializeComponent();
        }

        [Import]
        public TechnicalSolutionVm ViewModel
        {
            get { return DataContext as TechnicalSolutionVm; }
            set { DataContext = value; }
        }
    }
}