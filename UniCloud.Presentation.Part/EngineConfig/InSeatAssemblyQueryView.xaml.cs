#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class InSeatAssemblyQueryView
    {
        public InSeatAssemblyQueryView()
        {
            InitializeComponent();
        }

        [Import(typeof (InSeatAssemblyQueryVm))]
        public InSeatAssemblyQueryVm ViewModel
        {
            get { return DataContext as InSeatAssemblyQueryVm; }
            set { DataContext = value; }
        }
    }
}