#region NameSpace

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.SnHistories
{
    [Export]
    public partial class SnHistory : UserControl
    {
        public SnHistory()
        {
            InitializeComponent();
        }

        [Import(typeof (SnHistoryVm))]
        public SnHistoryVm ViewModel
        {
            get { return DataContext as SnHistoryVm; }
            set { DataContext = value; }
        }
    }
}