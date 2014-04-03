namespace AmasisSyncWinService
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
            this.AmasisWinServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AmasisWinServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AmasisWinServiceProcessInstaller
            // 
            this.AmasisWinServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.AmasisWinServiceProcessInstaller.Password = null;
            this.AmasisWinServiceProcessInstaller.Username = null;
            // 
            // AmasisWinServiceInstaller
            // 
            this.AmasisWinServiceInstaller.Description = "从AMASIS数据库中同步数据的服务";
            this.AmasisWinServiceInstaller.DisplayName = "同步FRP\\AMASIS数据服务";
            this.AmasisWinServiceInstaller.ServiceName = "AmasisSyncWinService";
            this.AmasisWinServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AmasisWinServiceProcessInstaller,
            this.AmasisWinServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller AmasisWinServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller AmasisWinServiceInstaller;
    }
}