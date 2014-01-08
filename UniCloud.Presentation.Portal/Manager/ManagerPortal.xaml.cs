//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Windows.Data;
using Telerik.Windows.Controls;
using System.Windows;

namespace UniCloud.Presentation.Portal.Manager
{
    [Export(typeof(ManagerPortal))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManagerPortal
    {
        public ManagerPortal()
        {
            InitializeComponent();
            Calendar.SelectedDate = DateTime.Today;
        }

        [Import]
        public ManagerPortalVm ViewModel
        {
            get { return DataContext as ManagerPortalVm; }
            set { DataContext = value; }
        }

        private void EventsListFilter(object sender, FilterEventArgs e)
        {
            var item = e.Item as Event;
            if (item != null) e.Accepted = Calendar != null && Calendar.SelectedDates.Contains(item.Date);
        }
    }
}
