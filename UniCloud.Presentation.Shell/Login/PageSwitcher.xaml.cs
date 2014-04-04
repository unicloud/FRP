#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace UniCloud.Presentation.Shell.Login
{
    [Export(typeof(PageSwitcher))]
    public partial class PageSwitcher 
    {
        [Import]
        public IRegionManager regionManager;

        public PageSwitcher()
        {
            InitializeComponent();
            SwitchPage(ServiceLocator.Current.GetInstance<LoginView>());
        }

        public void SwitchPage(UserControl newPage)
        {
            Content = newPage;
        }

        public void NavigateView(string viewName)
        {
            var uri = new Uri(viewName, UriKind.Relative);
            regionManager.RequestNavigate(RegionNames.MainRegion, uri);
        }
    }
}
