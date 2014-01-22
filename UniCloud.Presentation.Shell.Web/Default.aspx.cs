using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniCloud.Presentation.Shell.Web
{
    public partial class Default : Page
    {

        //读取config文件
        private void SaveSilverlightDeploymentSettings(Literal litSettings)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            var sb = new StringBuilder();
            sb.Append("<param name=\"InitParams\" value=\"");

            int settingCount = appSettings.Count;
            for (int idex = 0; idex < settingCount; idex++)
            {
                sb.Append(appSettings.GetKey(idex));
                sb.Append("=");
                sb.Append(appSettings[idex]);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("\" />");

            litSettings.Text = sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            SaveSilverlightDeploymentSettings(ParamInitParams);
        }
    }
}