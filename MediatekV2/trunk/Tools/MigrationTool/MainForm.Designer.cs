namespace MigrationTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.dlgSource = new System.Windows.Forms.OpenFileDialog();
            this.dlgDestination = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.btnBrowseDestination = new System.Windows.Forms.Button();
            this.bgwMigration = new System.ComponentModel.BackgroundWorker();
            this.prgOverall = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source database (SQLite)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Destination database (SQLCE)";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(180, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(314, 20);
            this.txtSource.TabIndex = 1;
            this.txtSource.Text = "D:\\Docs\\Visual Studio 2008\\Projects\\MediaTek\\dvd.db";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(180, 38);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(314, 20);
            this.txtDestination.TabIndex = 1;
            this.txtDestination.Text = "D:\\Docs\\Visual Studio 2010\\Projects\\Mediatek\\Database\\Mediatek.sdf";
            // 
            // dlgSource
            // 
            this.dlgSource.Filter = "SQLite database file (*.db, *.sqlite)|*.db;*.sqlite";
            // 
            // dlgDestination
            // 
            this.dlgDestination.Filter = "SQL Server Compact database file (*.sdf)|*.sdf";
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Location = new System.Drawing.Point(500, 10);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseSource.TabIndex = 2;
            this.btnBrowseSource.Text = "...";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // btnBrowseDestination
            // 
            this.btnBrowseDestination.Location = new System.Drawing.Point(500, 36);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseDestination.TabIndex = 2;
            this.btnBrowseDestination.Text = "...";
            this.btnBrowseDestination.UseVisualStyleBackColor = true;
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
            // 
            // bgwMigration
            // 
            this.bgwMigration.WorkerReportsProgress = true;
            this.bgwMigration.WorkerSupportsCancellation = true;
            this.bgwMigration.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMigration_DoWork);
            this.bgwMigration.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwMigration_ProgressChanged);
            this.bgwMigration.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwMigration_RunWorkerCompleted);
            // 
            // prgOverall
            // 
            this.prgOverall.Location = new System.Drawing.Point(180, 94);
            this.prgOverall.Name = "prgOverall";
            this.prgOverall.Size = new System.Drawing.Size(345, 23);
            this.prgOverall.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Overall progress";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(450, 65);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 129);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.prgOverall);
            this.Controls.Add(this.btnBrowseDestination);
            this.Controls.Add(this.btnBrowseSource);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Migration tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.OpenFileDialog dlgSource;
        private System.Windows.Forms.OpenFileDialog dlgDestination;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Button btnBrowseDestination;
        private System.ComponentModel.BackgroundWorker bgwMigration;
        private System.Windows.Forms.ProgressBar prgOverall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGo;
    }
}

