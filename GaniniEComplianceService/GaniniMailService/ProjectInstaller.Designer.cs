namespace GaniniMailService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceInstallerMailingService = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstallerMailingService = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // serviceInstallerMailingService
            // 
            this.serviceInstallerMailingService.Description = "Generates Mails";
            this.serviceInstallerMailingService.DisplayName = "GaniniMailService";
            this.serviceInstallerMailingService.ServiceName = "GaniniMailService";
            this.serviceInstallerMailingService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // serviceProcessInstallerMailingService
            // 
            this.serviceProcessInstallerMailingService.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstallerMailingService.Password = null;
            this.serviceProcessInstallerMailingService.Username = null;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceInstallerMailingService,
            this.serviceProcessInstallerMailingService});

        }

        #endregion

        private System.ServiceProcess.ServiceInstaller serviceInstallerMailingService;
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerMailingService;
    }
}