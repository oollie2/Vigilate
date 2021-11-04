using System.ServiceProcess;

namespace Vigilate
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
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;
        private void InitializeComponent()
        {
            this.vigilateServiceInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.vigilateService = new System.ServiceProcess.ServiceInstaller();
            // 
            // vigilateServiceInstaller
            // 
            this.vigilateServiceInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.vigilateServiceInstaller.Password = null;
            this.vigilateServiceInstaller.Username = null;
            // 
            // vigilateService
            // 
            this.vigilateService.Description = "GA Prevent PC from locking";
            this.vigilateService.DisplayName = "Vigilate Service";
            this.vigilateService.ServiceName = "Vigilate";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.vigilateServiceInstaller,
            this.vigilateService});
        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller vigilateServiceInstaller;
        private System.ServiceProcess.ServiceInstaller vigilateService;
    }
}