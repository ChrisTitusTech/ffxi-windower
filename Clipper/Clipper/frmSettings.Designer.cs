namespace Clipper
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnSave = new System.Windows.Forms.Button();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeletePlayer = new System.Windows.Forms.Button();
            this.btnAddPlayer = new System.Windows.Forms.Button();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.lstExcludedPlayers = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtZoneID = new System.Windows.Forms.TextBox();
            this.btnDeleteZone = new System.Windows.Forms.Button();
            this.btnAddZone = new System.Windows.Forms.Button();
            this.lstExcludedZones = new System.Windows.Forms.ListBox();
            this.txtZoneDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnSave.Location = new System.Drawing.Point(331, 346);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(12, 19);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(95, 17);
            this.chkAlwaysOnTop.TabIndex = 1;
            this.chkAlwaysOnTop.Text = "Always on top.\r\n";
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(12, 42);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(101, 17);
            this.chkMinimizeToTray.TabIndex = 2;
            this.chkMinimizeToTray.Text = "Minimize to tray.";
            this.chkMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtZoneDelay);
            this.groupBox1.Controls.Add(this.chkAlwaysOnTop);
            this.groupBox1.Controls.Add(this.chkMinimizeToTray);
            this.groupBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Configurations";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeletePlayer);
            this.groupBox2.Controls.Add(this.btnAddPlayer);
            this.groupBox2.Controls.Add(this.txtPlayerName);
            this.groupBox2.Controls.Add(this.lstExcludedPlayers);
            this.groupBox2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 259);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Excluded Players";
            // 
            // btnDeletePlayer
            // 
            this.btnDeletePlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePlayer.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnDeletePlayer.Location = new System.Drawing.Point(6, 224);
            this.btnDeletePlayer.Name = "btnDeletePlayer";
            this.btnDeletePlayer.Size = new System.Drawing.Size(110, 25);
            this.btnDeletePlayer.TabIndex = 6;
            this.btnDeletePlayer.Text = "Delete Selected";
            this.btnDeletePlayer.UseVisualStyleBackColor = true;
            this.btnDeletePlayer.Click += new System.EventHandler(this.btnDeletePlayer_Click);
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPlayer.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnAddPlayer.Location = new System.Drawing.Point(148, 224);
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(53, 25);
            this.btnAddPlayer.TabIndex = 5;
            this.btnAddPlayer.Text = "Add";
            this.btnAddPlayer.UseVisualStyleBackColor = true;
            this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlayerName.Location = new System.Drawing.Point(6, 198);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(195, 20);
            this.txtPlayerName.TabIndex = 1;
            // 
            // lstExcludedPlayers
            // 
            this.lstExcludedPlayers.FormattingEnabled = true;
            this.lstExcludedPlayers.Location = new System.Drawing.Point(6, 19);
            this.lstExcludedPlayers.Name = "lstExcludedPlayers";
            this.lstExcludedPlayers.Size = new System.Drawing.Size(195, 173);
            this.lstExcludedPlayers.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtZoneID);
            this.groupBox3.Controls.Add(this.btnDeleteZone);
            this.groupBox3.Controls.Add(this.btnAddZone);
            this.groupBox3.Controls.Add(this.lstExcludedZones);
            this.groupBox3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox3.Location = new System.Drawing.Point(225, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 259);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Excluded Zones";
            // 
            // txtZoneID
            // 
            this.txtZoneID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtZoneID.Location = new System.Drawing.Point(6, 198);
            this.txtZoneID.Name = "txtZoneID";
            this.txtZoneID.Size = new System.Drawing.Size(195, 20);
            this.txtZoneID.TabIndex = 7;
            // 
            // btnDeleteZone
            // 
            this.btnDeleteZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteZone.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnDeleteZone.Location = new System.Drawing.Point(6, 224);
            this.btnDeleteZone.Name = "btnDeleteZone";
            this.btnDeleteZone.Size = new System.Drawing.Size(110, 25);
            this.btnDeleteZone.TabIndex = 6;
            this.btnDeleteZone.Text = "Delete Selected";
            this.btnDeleteZone.UseVisualStyleBackColor = true;
            this.btnDeleteZone.Click += new System.EventHandler(this.btnDeleteZone_Click);
            // 
            // btnAddZone
            // 
            this.btnAddZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddZone.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnAddZone.Location = new System.Drawing.Point(148, 224);
            this.btnAddZone.Name = "btnAddZone";
            this.btnAddZone.Size = new System.Drawing.Size(53, 25);
            this.btnAddZone.TabIndex = 5;
            this.btnAddZone.Text = "Add";
            this.btnAddZone.UseVisualStyleBackColor = true;
            this.btnAddZone.Click += new System.EventHandler(this.btnAddZone_Click);
            // 
            // lstExcludedZones
            // 
            this.lstExcludedZones.FormattingEnabled = true;
            this.lstExcludedZones.Location = new System.Drawing.Point(6, 19);
            this.lstExcludedZones.Name = "lstExcludedZones";
            this.lstExcludedZones.Size = new System.Drawing.Size(195, 173);
            this.lstExcludedZones.TabIndex = 0;
            // 
            // txtZoneDelay
            // 
            this.txtZoneDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtZoneDelay.Location = new System.Drawing.Point(161, 67);
            this.txtZoneDelay.Name = "txtZoneDelay";
            this.txtZoneDelay.Size = new System.Drawing.Size(40, 20);
            this.txtZoneDelay.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Zone Delay:";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 383);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clipper Settings";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstExcludedPlayers;
        private System.Windows.Forms.Button btnDeletePlayer;
        private System.Windows.Forms.Button btnAddPlayer;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDeleteZone;
        private System.Windows.Forms.Button btnAddZone;
        private System.Windows.Forms.ListBox lstExcludedZones;
        private System.Windows.Forms.TextBox txtZoneID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtZoneDelay;
    }
}