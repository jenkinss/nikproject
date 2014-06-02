namespace SoyalEventServer
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SettingsPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textMilisec = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LogPage = new System.Windows.Forms.TabPage();
            this.logList = new System.Windows.Forms.ListBox();
            this.EventWorker = new System.ComponentModel.BackgroundWorker();
            this.BeregovoEventWorker = new System.ComponentModel.BackgroundWorker();
            this.BeganyEventWorker = new System.ComponentModel.BackgroundWorker();
            this.maintainPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SettingsPage.SuspendLayout();
            this.LogPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SettingsPage);
            this.tabControl1.Controls.Add(this.LogPage);
            this.tabControl1.Controls.Add(this.maintainPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(439, 427);
            this.tabControl1.TabIndex = 0;
            // 
            // SettingsPage
            // 
            this.SettingsPage.Controls.Add(this.label1);
            this.SettingsPage.Controls.Add(this.label5);
            this.SettingsPage.Controls.Add(this.textMilisec);
            this.SettingsPage.Controls.Add(this.button2);
            this.SettingsPage.Controls.Add(this.button1);
            this.SettingsPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsPage.Name = "SettingsPage";
            this.SettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsPage.Size = new System.Drawing.Size(431, 401);
            this.SettingsPage.TabIndex = 0;
            this.SettingsPage.Text = "Settings";
            this.SettingsPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "MS";
            // 
            // textMilisec
            // 
            this.textMilisec.Location = new System.Drawing.Point(8, 152);
            this.textMilisec.Name = "textMilisec";
            this.textMilisec.Size = new System.Drawing.Size(71, 20);
            this.textMilisec.TabIndex = 9;
            this.textMilisec.Text = "5000";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 67);
            this.button2.TabIndex = 8;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 67);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start Server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LogPage
            // 
            this.LogPage.Controls.Add(this.logList);
            this.LogPage.Location = new System.Drawing.Point(4, 22);
            this.LogPage.Name = "LogPage";
            this.LogPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogPage.Size = new System.Drawing.Size(378, 401);
            this.LogPage.TabIndex = 1;
            this.LogPage.Text = "LogPage";
            this.LogPage.UseVisualStyleBackColor = true;
            // 
            // logList
            // 
            this.logList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logList.FormattingEnabled = true;
            this.logList.Location = new System.Drawing.Point(3, 3);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(372, 395);
            this.logList.TabIndex = 0;
            // 
            // EventWorker
            // 
            this.EventWorker.WorkerSupportsCancellation = true;
            this.EventWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.EventWorker_DoWork);
            // 
            // BeregovoEventWorker
            // 
            this.BeregovoEventWorker.WorkerSupportsCancellation = true;
            this.BeregovoEventWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BeregovoEventWorker_DoWork);
            // 
            // BeganyEventWorker
            // 
            this.BeganyEventWorker.WorkerSupportsCancellation = true;
            this.BeganyEventWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BeganyEventWorker_DoWork);
            // 
            // maintainPage
            // 
            this.maintainPage.Location = new System.Drawing.Point(4, 22);
            this.maintainPage.Name = "maintainPage";
            this.maintainPage.Size = new System.Drawing.Size(431, 401);
            this.maintainPage.TabIndex = 2;
            this.maintainPage.Text = "maintain";
            this.maintainPage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 427);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "BIS - Event Server";
            this.tabControl1.ResumeLayout(false);
            this.SettingsPage.ResumeLayout(false);
            this.SettingsPage.PerformLayout();
            this.LogPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SettingsPage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage LogPage;
        private System.Windows.Forms.ListBox logList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textMilisec;
        private System.ComponentModel.BackgroundWorker EventWorker;
        private System.ComponentModel.BackgroundWorker BeregovoEventWorker;
        private System.ComponentModel.BackgroundWorker BeganyEventWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage maintainPage;
    }
}

