#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export]
    public partial class SelectAircrafts
    {
        public SelectAircrafts()
        {
            InitializeComponent();
        }

        [Import(typeof (SelectAircraftsVm))]
        public SelectAircraftsVm ViewModel
        {
            get { return DataContext as SelectAircraftsVm; }
            set { DataContext = value; }
        }
    }
}