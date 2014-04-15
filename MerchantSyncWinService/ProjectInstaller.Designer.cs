namespace MerchantSyncWinService
{
    partial class ProjectInstaller
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
            this.MerchantWinServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MerchantWinServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MerchantWinServiceProcessInstaller
            // 
            this.MerchantWinServiceProcessInstaller.Password = null;
            this.MerchantWinServiceProcessInstaller.Username = null;
            // 
            // MerchantWinServiceInstaller
            // 
            this.MerchantWinServiceInstaller.Description = "获取客商信息、联系人以及银行账号";
            this.MerchantWinServiceInstaller.DisplayName = "客商信息同步";
            this.MerchantWinServiceInstaller.ServiceName = "MerchantSyncWinService";
            this.MerchantWinServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MerchantWinServiceProcessInstaller,
            this.MerchantWinServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MerchantWinServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MerchantWinServiceInstaller;
    }
}