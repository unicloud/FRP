#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export]
    public partial class DocumentViewer
    {
        public DocumentViewer()
        {
            InitializeComponent();
        }

        [Import(typeof (DocumentViewerVm))]
        public DocumentViewerVm ViewModel
        {
            get { return DataContext as DocumentViewerVm; }
            set { DataContext = value; }
        }

        #region EventHandlers

        private void TbCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        #endregion
    }
}