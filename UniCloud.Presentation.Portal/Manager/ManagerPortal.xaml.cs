#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Windows.Data;

#endregion

namespace UniCloud.Presentation.Portal.Manager
{
    [Export]
    public partial class ManagerPortal
    {
        public ManagerPortal()
        {
            InitializeComponent();
            Calendar.SelectedDate = DateTime.Today;
        }

        [Import(typeof (ManagerPortalVm))]
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