#region 命名空间

using System;
using System.Windows;

#endregion

namespace UniCloud.Presentation.Service
{
    /// <summary>
    ///     代理帮助类。用来处理静态的URl。
    /// </summary>
    public static class AgentHelper
    {
        /// <summary>
        ///     采购Uri。
        /// </summary>
        public static Uri PurchaseUri
        {
            get
            {
                return new Uri(Application.Current.Resources["PurchaseDataService"].ToString(),
                    UriKind.Absolute);
            }
        }

        public static Uri PaymentUri
        {
            get
            {
                return new Uri(Application.Current.Resources["PaymentDataService"].ToString(),
                    UriKind.Absolute);
            }
        }

        public static Uri CommonServiceUri
        {
            get
            {
                return new Uri(Application.Current.Resources["CommonServiceDataService"].ToString(),
                    UriKind.Absolute);
            }
        }

        public static Uri FleetPlanServiceUri
        {
            get
            {
                return new Uri(Application.Current.Resources["FleetPlanDataService"].ToString(),
                    UriKind.Absolute);
            }
        }

        public static Uri ProjectServiceUri
        {
            get
            {
                return new Uri(Application.Current.Resources["ProjectDataService"].ToString(),
                    UriKind.Absolute);
            }
        }

        public static Uri AircraftConfigUri
        {
            get
            {
                return new Uri(Application.Current.Resources["AircraftConfigDataService"].ToString(),
                    UriKind.Absolute);
            }
        }
    }
}