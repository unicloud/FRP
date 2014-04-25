namespace DocumentIndexWinService
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
            this.DocumentIndexWinServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DocumentIndexWinServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DocumentIndexWinServiceProcessInstaller
            // 
            this.DocumentIndexWinServiceProcessInstaller.Password = null;
            this.DocumentIndexWinServiceProcessInstaller.Username = null;
            // 
            // DocumentIndexWinServiceInstaller
            // 
            this.DocumentIndexWinServiceInstaller.Description = "对数据库中新增的文档添加、修改索引";
            this.DocumentIndexWinServiceInstaller.DisplayName = "创建文档索引";
            this.DocumentIndexWinServiceInstaller.ServiceName = "DocumentIndexWinService";
            this.DocumentIndexWinServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DocumentIndexWinServiceProcessInstaller,
            this.DocumentIndexWinServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DocumentIndexWinServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller DocumentIndexWinServiceInstaller;
    }
}