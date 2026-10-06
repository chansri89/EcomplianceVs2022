namespace GeneratingMail.Reports
{
    partial class ReportViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CLReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CLReportViewer
            // 
            this.CLReportViewer.ActiveViewIndex = -1;
            this.CLReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CLReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CLReportViewer.Location = new System.Drawing.Point(0, 0);
            this.CLReportViewer.Name = "CLReportViewer";
            this.CLReportViewer.SelectionFormula = "";
            this.CLReportViewer.Size = new System.Drawing.Size(292, 266);
            this.CLReportViewer.TabIndex = 0;
            this.CLReportViewer.ViewTimeSelectionFormula = "";
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.CLReportViewer);
            this.Name = "ReportViewer";
            this.Text = "ReportViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CLReportViewer;
    }
}