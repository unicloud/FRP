using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Document
{
    [Export]
    public partial class WordViewer
    {
        public WordViewer()
        {
            InitializeComponent();
        }

        [Import(typeof(WordViewerVm))]
        public WordViewerVm ViewModel
        {
            get { return DataContext as WordViewerVm; }
            set { DataContext = value; }
        }
    }
}
