using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Document
{
    [Export]
    public partial class DocumentViewer
    {
        public DocumentViewer()
        {
            InitializeComponent();
            ViewModel = new DocumentViewerVm(this);
            DataContext = ViewModel;
        }

        [Import(typeof(DocumentViewerVm))]
        public DocumentViewerVm ViewModel
        {
            get { return DataContext as DocumentViewerVm; }
            set { DataContext = value; }
        }

        #region EventHandlers

        private void TbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }
        #endregion
    }
}
