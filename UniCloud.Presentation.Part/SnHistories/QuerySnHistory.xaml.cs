#region NameSpace

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.SnHistories
{
    [Export]
    public partial class QuerySnHistory : UserControl
    {
        public QuerySnHistory()
        {
            InitializeComponent();
        }

        [Import(typeof (QuerySnHistoryVm))]
        public QuerySnHistoryVm ViewModel
        {
            get { return DataContext as QuerySnHistoryVm; }
            set { DataContext = value; }
        }
    }
}