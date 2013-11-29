
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Documents.Fixed.Model;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(AircraftLeaseReceptionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AircraftLeaseReceptionManager : UserControl
    {
        private bool enableGrouping = true;
        private GroupDescriptionCollection groupDescriptions;
        private Func<object, bool> groupFilter;
        private ObservableCollection<Appointment> appointments;
        private CategoryCollection categories;
        private TimeMarkerCollection timeMarkers;
        private Appointment _selectedAppointment;

        public AircraftLeaseReceptionManager()
        {
            InitializeComponent();
        }
        [Import]
        public AircraftLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftLeaseReceptionManagerVM; }
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
            //_pdfDocument = this.pdfViewer.Document;
            //Storyboard expand = this.Resources["Expand"] as Storyboard;
            //Storyboard collapse = this.Resources["Collapse"] as Storyboard;

            //if (this.PageButton.IsChecked == true && expand != null)
            //{
            //    expand.Begin();
            //}
            //if (this.PageButton.IsChecked == false && collapse != null)
            //{
            //    collapse.Begin();
            //    UncheckButton(this.PageButton);
            //}
        }

        #endregion
    }
}
