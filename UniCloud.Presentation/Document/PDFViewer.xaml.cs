#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export]
    public partial class PDFViewer 
    {
        public PDFViewer()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }


        [Import(typeof(PDFViewerVm))]
        public PDFViewerVm ViewModel
        {
            get { return DataContext as PDFViewerVm; }
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
