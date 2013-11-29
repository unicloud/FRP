
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(AircraftPurchaseReceptionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AircraftPurchaseReceptionManager : UserControl
    {
        public AircraftPurchaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }
        [Import]
        public AircraftPurchaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftPurchaseReceptionManagerVM; }
            set { DataContext = value; }
        }

        #region Methods

        private void UncheckButton(RadToggleButton btn)
        {
            if (btn.IsChecked == true)
            {
                btn.IsChecked = false;
            }
        }

        #endregion

        #region EventHandlers
        private RadFixedDocument _pdfDocument;

        private void TbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        private void ChangePageNavigationPanelVisibility(object sender, RoutedEventArgs e)
        {
            _pdfDocument = this.pdfViewer.Document;
            Storyboard expand = this.Resources["Expand"] as Storyboard;
            Storyboard collapse = this.Resources["Collapse"] as Storyboard;

            if (this.PageButton.IsChecked == true && expand != null)
            {
                expand.Begin();
            }
            if (this.PageButton.IsChecked == false && collapse != null)
            {
                collapse.Begin();
                UncheckButton(this.PageButton);
            }
        }

        #endregion
    }
}
