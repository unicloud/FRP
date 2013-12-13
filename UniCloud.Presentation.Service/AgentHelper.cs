﻿#region

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
                return new Uri("http://localhost:20059/PurchaseDataService.svc",
                               UriKind.Absolute);
            }
        }

        public static Uri PaymentUri
        {
            get
            {
                return new Uri("http://localhost:20109/PaymentDataService.svc",
                    UriKind.Absolute);
            }
        }

        public static Uri CommonServiceUri
        {
            get
            {
                return new Uri("http://localhost:20074/CommonServiceDataService.svc",
                               UriKind.Absolute);
            }
        }
    }
}