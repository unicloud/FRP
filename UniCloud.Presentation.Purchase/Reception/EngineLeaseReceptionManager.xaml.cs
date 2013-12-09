
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(EngineLeaseReceptionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class EngineLeaseReceptionManager : UserControl
    {
        public EngineLeaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import]
        public EngineLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EngineLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }

        #region Methods

        #endregion

        private void RadScheduleView_OnAppointmentDeleted(object sender, AppointmentDeletedEventArgs e)
        {

        }

        private void RadScheduleView_OnAppointmentEdited(object sender, AppointmentEditedEventArgs e)
        {
        }

        private void RadScheduleView_OnAppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
        }
    }
}
