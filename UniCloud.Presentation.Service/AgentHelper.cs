#region

using System;

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
                return new Uri("http://192.1.1.219:6000/PurchaseDataService.svc/",
                               UriKind.Absolute);
            }
        }
    }
}