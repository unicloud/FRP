#region 命名空间

using System;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Security;
using log4net.Config;
using UniCloud.DistributedServices.BaseManagement.InstanceProviders;

#endregion

namespace UniCloud.DistributedServices.BaseManagement
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Container.ConfigureContainer();
            XmlConfigurator.Configure();
            AuthenticationService.CreatingCookie += AuthenticationService_CreatingCookie;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        private static void AuthenticationService_CreatingCookie(object sender, CreatingCookieEventArgs e)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                e.UserName,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                e.IsPersistent,
                e.CustomCredential,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket =
                FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie
                (FormsAuthentication.FormsCookieName,
                    encryptedTicket) {Expires = DateTime.Now.AddMinutes(30)};

            HttpContext.Current.Response.Cookies.Add(cookie);
            e.CookieIsSet = true;
        }
    }
}