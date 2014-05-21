namespace FleetXmlWinService
{
    partial class ServiceInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.XmlConfigServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.XmlConfigServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // XmlConfigServiceProcessInstaller
            // 
            this.XmlConfigServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.XmlConfigServiceProcessInstaller.Password = null;
            this.XmlConfigServiceProcessInstaller.Username = null;
            // 
            // XmlConfigServiceInstaller
            // 
            this.XmlConfigServiceInstaller.Description = "航空公司机队数据系统服务，生成统计分析相关数据";
            this.XmlConfigServiceInstaller.DisplayName = "航空公司机队数据系统服务";
            this.XmlConfigServiceInstaller.ServiceName = "AircraftDataService";
            this.XmlConfigServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.XmlConfigServiceProcessInstaller,
            this.XmlConfigServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller XmlConfigServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller XmlConfigServiceInstaller;
    }
}