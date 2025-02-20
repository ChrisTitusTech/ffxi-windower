namespace Clipper
{
    using System;
    using System.ComponentModel;

    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkStatusAutoDisable = new System.Windows.Forms.CheckBox();
            this.rbStatusMaintenance = new System.Windows.Forms.RadioButton();
            this.rbStatusChocobo = new System.Windows.Forms.RadioButton();
            this.rbStatusNone = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUseGMFlag = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPlayerSpeedDisabled = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPlayerSpeedDisabled = new System.Windows.Forms.TrackBar();
            this.lblPlayerSpeed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAutoDisableSpeedHack = new System.Windows.Forms.CheckBox();
            this.tbPlayerSpeed = new System.Windows.Forms.TrackBar();
            this.chkEnableSpeedHack = new System.Windows.Forms.CheckBox();
            this.chkUseExclusions = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkAutoDisablePositionHacks = new System.Windows.Forms.CheckBox();
            this.chkLockZCoord = new System.Windows.Forms.CheckBox();
            this.btnPosSE = new System.Windows.Forms.Button();
            this.btnPosS = new System.Windows.Forms.Button();
            this.btnPosSW = new System.Windows.Forms.Button();
            this.btnPosD = new System.Windows.Forms.Button();
            this.btnPosE = new System.Windows.Forms.Button();
            this.btnPosW = new System.Windows.Forms.Button();
            this.btnPosU = new System.Windows.Forms.Button();
            this.btnPosNE = new System.Windows.Forms.Button();
            this.btnPosN = new System.Windows.Forms.Button();
            this.btnPosNW = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoDisableJAWait0 = new System.Windows.Forms.CheckBox();
            this.chkUseJAWait0 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDetectStatus = new System.Windows.Forms.Label();
            this.niTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ssMainStrip = new System.Windows.Forms.StatusStrip();
            this.ssCharacterNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssCharacterName = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayerSpeedDisabled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayerSpeed)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.msMainMenu.SuspendLayout();
            this.ssMainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkStatusAutoDisable);
            this.groupBox1.Controls.Add(this.rbStatusMaintenance);
            this.groupBox1.Controls.Add(this.rbStatusChocobo);
            this.groupBox1.Controls.Add(this.rbStatusNone);
            this.groupBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status Hacks";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(6, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 2);
            this.label1.TabIndex = 4;
            // 
            // chkStatusAutoDisable
            // 
            this.chkStatusAutoDisable.AutoSize = true;
            this.chkStatusAutoDisable.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkStatusAutoDisable.Location = new System.Drawing.Point(6, 88);
            this.chkStatusAutoDisable.Name = "chkStatusAutoDisable";
            this.chkStatusAutoDisable.Size = new System.Drawing.Size(86, 17);
            this.chkStatusAutoDisable.TabIndex = 3;
            this.chkStatusAutoDisable.Text = "Auto-Disable";
            this.chkStatusAutoDisable.UseVisualStyleBackColor = true;
            this.chkStatusAutoDisable.CheckedChanged += new System.EventHandler(this.chkStatusAutoDisable_CheckedChanged);
            // 
            // rbStatusMaintenance
            // 
            this.rbStatusMaintenance.AutoSize = true;
            this.rbStatusMaintenance.ForeColor = System.Drawing.Color.DarkOrange;
            this.rbStatusMaintenance.Location = new System.Drawing.Point(6, 65);
            this.rbStatusMaintenance.Name = "rbStatusMaintenance";
            this.rbStatusMaintenance.Size = new System.Drawing.Size(87, 17);
            this.rbStatusMaintenance.TabIndex = 2;
            this.rbStatusMaintenance.TabStop = true;
            this.rbStatusMaintenance.Text = "Maintenance";
            this.rbStatusMaintenance.UseVisualStyleBackColor = true;
            this.rbStatusMaintenance.CheckedChanged += new System.EventHandler(this.radioStatus_CheckedChanged);
            // 
            // rbStatusChocobo
            // 
            this.rbStatusChocobo.AutoSize = true;
            this.rbStatusChocobo.ForeColor = System.Drawing.Color.DarkOrange;
            this.rbStatusChocobo.Location = new System.Drawing.Point(6, 42);
            this.rbStatusChocobo.Name = "rbStatusChocobo";
            this.rbStatusChocobo.Size = new System.Drawing.Size(68, 17);
            this.rbStatusChocobo.TabIndex = 1;
            this.rbStatusChocobo.TabStop = true;
            this.rbStatusChocobo.Text = "Chocobo";
            this.rbStatusChocobo.UseVisualStyleBackColor = true;
            this.rbStatusChocobo.CheckedChanged += new System.EventHandler(this.radioStatus_CheckedChanged);
            // 
            // rbStatusNone
            // 
            this.rbStatusNone.AutoSize = true;
            this.rbStatusNone.ForeColor = System.Drawing.Color.DarkOrange;
            this.rbStatusNone.Location = new System.Drawing.Point(6, 19);
            this.rbStatusNone.Name = "rbStatusNone";
            this.rbStatusNone.Size = new System.Drawing.Size(74, 17);
            this.rbStatusNone.TabIndex = 0;
            this.rbStatusNone.TabStop = true;
            this.rbStatusNone.Text = "None (Off)";
            this.rbStatusNone.UseVisualStyleBackColor = true;
            this.rbStatusNone.CheckedChanged += new System.EventHandler(this.radioStatus_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkUseGMFlag);
            this.groupBox2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox2.Location = new System.Drawing.Point(118, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 42);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Misc. Hacks";
            // 
            // chkUseGMFlag
            // 
            this.chkUseGMFlag.AutoSize = true;
            this.chkUseGMFlag.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkUseGMFlag.Location = new System.Drawing.Point(6, 19);
            this.chkUseGMFlag.Name = "chkUseGMFlag";
            this.chkUseGMFlag.Size = new System.Drawing.Size(88, 17);
            this.chkUseGMFlag.TabIndex = 7;
            this.chkUseGMFlag.Text = "Use GM Flag";
            this.chkUseGMFlag.UseVisualStyleBackColor = true;
            this.chkUseGMFlag.CheckedChanged += new System.EventHandler(this.chkUseGMFlag_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblPlayerSpeedDisabled);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbPlayerSpeedDisabled);
            this.groupBox3.Controls.Add(this.lblPlayerSpeed);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.chkAutoDisableSpeedHack);
            this.groupBox3.Controls.Add(this.tbPlayerSpeed);
            this.groupBox3.Controls.Add(this.chkEnableSpeedHack);
            this.groupBox3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox3.Location = new System.Drawing.Point(12, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(206, 190);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Speed Hack";
            // 
            // lblPlayerSpeedDisabled
            // 
            this.lblPlayerSpeedDisabled.AutoSize = true;
            this.lblPlayerSpeedDisabled.Location = new System.Drawing.Point(170, 166);
            this.lblPlayerSpeedDisabled.Name = "lblPlayerSpeedDisabled";
            this.lblPlayerSpeedDisabled.Size = new System.Drawing.Size(22, 13);
            this.lblPlayerSpeedDisabled.TabIndex = 14;
            this.lblPlayerSpeedDisabled.Text = "5.0";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Location = new System.Drawing.Point(6, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 2);
            this.label7.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkOrange;
            this.label6.Location = new System.Drawing.Point(91, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Disable Speed:";
            // 
            // tbPlayerSpeedDisabled
            // 
            this.tbPlayerSpeedDisabled.LargeChange = 1;
            this.tbPlayerSpeedDisabled.Location = new System.Drawing.Point(6, 118);
            this.tbPlayerSpeedDisabled.Maximum = 151;
            this.tbPlayerSpeedDisabled.Minimum = 1;
            this.tbPlayerSpeedDisabled.Name = "tbPlayerSpeedDisabled";
            this.tbPlayerSpeedDisabled.Size = new System.Drawing.Size(194, 42);
            this.tbPlayerSpeedDisabled.TabIndex = 11;
            this.tbPlayerSpeedDisabled.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbPlayerSpeedDisabled.Value = 1;
            this.tbPlayerSpeedDisabled.Scroll += new System.EventHandler(this.tbPlayerSpeedDisabled_Scroll);
            // 
            // lblPlayerSpeed
            // 
            this.lblPlayerSpeed.AutoSize = true;
            this.lblPlayerSpeed.ForeColor = System.Drawing.Color.Green;
            this.lblPlayerSpeed.Location = new System.Drawing.Point(88, 94);
            this.lblPlayerSpeed.Name = "lblPlayerSpeed";
            this.lblPlayerSpeed.Size = new System.Drawing.Size(22, 13);
            this.lblPlayerSpeed.TabIndex = 10;
            this.lblPlayerSpeed.Text = "0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(6, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Current Speed:";
            // 
            // chkAutoDisableSpeedHack
            // 
            this.chkAutoDisableSpeedHack.AutoSize = true;
            this.chkAutoDisableSpeedHack.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkAutoDisableSpeedHack.Location = new System.Drawing.Point(6, 165);
            this.chkAutoDisableSpeedHack.Name = "chkAutoDisableSpeedHack";
            this.chkAutoDisableSpeedHack.Size = new System.Drawing.Size(86, 17);
            this.chkAutoDisableSpeedHack.TabIndex = 2;
            this.chkAutoDisableSpeedHack.Text = "Auto-Disable";
            this.chkAutoDisableSpeedHack.UseVisualStyleBackColor = true;
            this.chkAutoDisableSpeedHack.CheckedChanged += new System.EventHandler(this.chkAutoDisableSpeedHack_CheckedChanged);
            // 
            // tbPlayerSpeed
            // 
            this.tbPlayerSpeed.LargeChange = 1;
            this.tbPlayerSpeed.Location = new System.Drawing.Point(6, 42);
            this.tbPlayerSpeed.Maximum = 151;
            this.tbPlayerSpeed.Minimum = 1;
            this.tbPlayerSpeed.Name = "tbPlayerSpeed";
            this.tbPlayerSpeed.Size = new System.Drawing.Size(194, 6);
            this.tbPlayerSpeed.TabIndex = 1;
            this.tbPlayerSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbPlayerSpeed.Value = 1;
            this.tbPlayerSpeed.Scroll += new System.EventHandler(this.tbPlayerSpeed_Scroll);
            // 
            // chkEnableSpeedHack
            // 
            this.chkEnableSpeedHack.AutoSize = true;
            this.chkEnableSpeedHack.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkEnableSpeedHack.Location = new System.Drawing.Point(6, 19);
            this.chkEnableSpeedHack.Name = "chkEnableSpeedHack";
            this.chkEnableSpeedHack.Size = new System.Drawing.Size(117, 17);
            this.chkEnableSpeedHack.TabIndex = 0;
            this.chkEnableSpeedHack.Text = "Enable Speedhack";
            this.chkEnableSpeedHack.UseVisualStyleBackColor = true;
            this.chkEnableSpeedHack.CheckedChanged += new System.EventHandler(this.chkEnableSpeedHack_CheckedChanged);
            // 
            // chkUseExclusions
            // 
            this.chkUseExclusions.AutoSize = true;
            this.chkUseExclusions.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkUseExclusions.Location = new System.Drawing.Point(120, 511);
            this.chkUseExclusions.Name = "chkUseExclusions";
            this.chkUseExclusions.Size = new System.Drawing.Size(98, 17);
            this.chkUseExclusions.TabIndex = 3;
            this.chkUseExclusions.Text = "Use Exclusions";
            this.chkUseExclusions.UseVisualStyleBackColor = true;
            this.chkUseExclusions.CheckedChanged += new System.EventHandler(this.chkUseExclusions_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.chkAutoDisablePositionHacks);
            this.groupBox4.Controls.Add(this.chkLockZCoord);
            this.groupBox4.Controls.Add(this.btnPosSE);
            this.groupBox4.Controls.Add(this.btnPosS);
            this.groupBox4.Controls.Add(this.btnPosSW);
            this.groupBox4.Controls.Add(this.btnPosD);
            this.groupBox4.Controls.Add(this.btnPosE);
            this.groupBox4.Controls.Add(this.btnPosW);
            this.groupBox4.Controls.Add(this.btnPosU);
            this.groupBox4.Controls.Add(this.btnPosNE);
            this.groupBox4.Controls.Add(this.btnPosN);
            this.groupBox4.Controls.Add(this.btnPosNW);
            this.groupBox4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox4.Location = new System.Drawing.Point(12, 343);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(206, 155);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Position Hacks";
            // 
            // chkAutoDisablePositionHacks
            // 
            this.chkAutoDisablePositionHacks.AutoSize = true;
            this.chkAutoDisablePositionHacks.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkAutoDisablePositionHacks.Location = new System.Drawing.Point(6, 130);
            this.chkAutoDisablePositionHacks.Name = "chkAutoDisablePositionHacks";
            this.chkAutoDisablePositionHacks.Size = new System.Drawing.Size(195, 17);
            this.chkAutoDisablePositionHacks.TabIndex = 21;
            this.chkAutoDisablePositionHacks.Text = "Auto-Disable (Prevents Movements)";
            this.chkAutoDisablePositionHacks.UseVisualStyleBackColor = true;
            this.chkAutoDisablePositionHacks.CheckedChanged += new System.EventHandler(this.chkAutoDisablePositionHacks_CheckedChanged);
            // 
            // chkLockZCoord
            // 
            this.chkLockZCoord.AutoSize = true;
            this.chkLockZCoord.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkLockZCoord.Location = new System.Drawing.Point(6, 107);
            this.chkLockZCoord.Name = "chkLockZCoord";
            this.chkLockZCoord.Size = new System.Drawing.Size(137, 17);
            this.chkLockZCoord.TabIndex = 20;
            this.chkLockZCoord.Text = "Lock Z Coord (Floating)";
            this.chkLockZCoord.UseVisualStyleBackColor = true;
            this.chkLockZCoord.CheckedChanged += new System.EventHandler(this.chkLockZCoord_CheckedChanged);
            // 
            // btnPosSE
            // 
            this.btnPosSE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosSE.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosSE.Location = new System.Drawing.Point(127, 73);
            this.btnPosSE.Name = "btnPosSE";
            this.btnPosSE.Size = new System.Drawing.Size(43, 23);
            this.btnPosSE.TabIndex = 19;
            this.btnPosSE.Text = "SE";
            this.btnPosSE.UseVisualStyleBackColor = true;
            this.btnPosSE.Click += new System.EventHandler(this.btnPosSE_Click);
            // 
            // btnPosS
            // 
            this.btnPosS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosS.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosS.Location = new System.Drawing.Point(81, 73);
            this.btnPosS.Name = "btnPosS";
            this.btnPosS.Size = new System.Drawing.Size(45, 23);
            this.btnPosS.TabIndex = 18;
            this.btnPosS.Text = "S";
            this.btnPosS.UseVisualStyleBackColor = true;
            this.btnPosS.Click += new System.EventHandler(this.btnPosS_Click);
            // 
            // btnPosSW
            // 
            this.btnPosSW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosSW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosSW.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosSW.Location = new System.Drawing.Point(36, 73);
            this.btnPosSW.Name = "btnPosSW";
            this.btnPosSW.Size = new System.Drawing.Size(43, 23);
            this.btnPosSW.TabIndex = 17;
            this.btnPosSW.Text = "SW";
            this.btnPosSW.UseVisualStyleBackColor = true;
            this.btnPosSW.Click += new System.EventHandler(this.btnPosSW_Click);
            // 
            // btnPosD
            // 
            this.btnPosD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosD.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosD.Location = new System.Drawing.Point(149, 46);
            this.btnPosD.Name = "btnPosD";
            this.btnPosD.Size = new System.Drawing.Size(43, 23);
            this.btnPosD.TabIndex = 16;
            this.btnPosD.Text = "Dn";
            this.btnPosD.UseVisualStyleBackColor = true;
            this.btnPosD.Click += new System.EventHandler(this.btnPosD_Click);
            // 
            // btnPosE
            // 
            this.btnPosE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosE.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosE.Location = new System.Drawing.Point(104, 46);
            this.btnPosE.Name = "btnPosE";
            this.btnPosE.Size = new System.Drawing.Size(43, 23);
            this.btnPosE.TabIndex = 15;
            this.btnPosE.Text = "E";
            this.btnPosE.UseVisualStyleBackColor = true;
            this.btnPosE.Click += new System.EventHandler(this.btnPosE_Click);
            // 
            // btnPosW
            // 
            this.btnPosW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosW.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosW.Location = new System.Drawing.Point(59, 46);
            this.btnPosW.Name = "btnPosW";
            this.btnPosW.Size = new System.Drawing.Size(43, 23);
            this.btnPosW.TabIndex = 14;
            this.btnPosW.Text = "W";
            this.btnPosW.UseVisualStyleBackColor = true;
            this.btnPosW.Click += new System.EventHandler(this.btnPosW_Click);
            // 
            // btnPosU
            // 
            this.btnPosU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosU.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosU.Location = new System.Drawing.Point(14, 46);
            this.btnPosU.Name = "btnPosU";
            this.btnPosU.Size = new System.Drawing.Size(43, 23);
            this.btnPosU.TabIndex = 13;
            this.btnPosU.Text = "Up";
            this.btnPosU.UseVisualStyleBackColor = true;
            this.btnPosU.Click += new System.EventHandler(this.btnPosU_Click);
            // 
            // btnPosNE
            // 
            this.btnPosNE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosNE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosNE.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosNE.Location = new System.Drawing.Point(127, 19);
            this.btnPosNE.Name = "btnPosNE";
            this.btnPosNE.Size = new System.Drawing.Size(43, 23);
            this.btnPosNE.TabIndex = 12;
            this.btnPosNE.Text = "NE";
            this.btnPosNE.UseVisualStyleBackColor = true;
            this.btnPosNE.Click += new System.EventHandler(this.btnPosNE_Click);
            // 
            // btnPosN
            // 
            this.btnPosN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosN.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosN.Location = new System.Drawing.Point(80, 19);
            this.btnPosN.Name = "btnPosN";
            this.btnPosN.Size = new System.Drawing.Size(45, 23);
            this.btnPosN.TabIndex = 11;
            this.btnPosN.Text = "N";
            this.btnPosN.UseVisualStyleBackColor = true;
            this.btnPosN.Click += new System.EventHandler(this.btnPosN_Click);
            // 
            // btnPosNW
            // 
            this.btnPosNW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosNW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPosNW.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnPosNW.Location = new System.Drawing.Point(35, 19);
            this.btnPosNW.Name = "btnPosNW";
            this.btnPosNW.Size = new System.Drawing.Size(43, 23);
            this.btnPosNW.TabIndex = 10;
            this.btnPosNW.Text = "NW";
            this.btnPosNW.UseVisualStyleBackColor = true;
            this.btnPosNW.Click += new System.EventHandler(this.btnPosNW_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.chkAutoDisableJAWait0);
            this.groupBox5.Controls.Add(this.chkUseJAWait0);
            this.groupBox5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox5.Location = new System.Drawing.Point(118, 73);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(100, 68);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "JAWait0 Hack";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 2);
            this.label2.TabIndex = 11;
            // 
            // chkAutoDisableJAWait0
            // 
            this.chkAutoDisableJAWait0.AutoSize = true;
            this.chkAutoDisableJAWait0.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkAutoDisableJAWait0.Location = new System.Drawing.Point(6, 42);
            this.chkAutoDisableJAWait0.Name = "chkAutoDisableJAWait0";
            this.chkAutoDisableJAWait0.Size = new System.Drawing.Size(86, 17);
            this.chkAutoDisableJAWait0.TabIndex = 10;
            this.chkAutoDisableJAWait0.Text = "Auto-Disable";
            this.chkAutoDisableJAWait0.UseVisualStyleBackColor = true;
            this.chkAutoDisableJAWait0.CheckedChanged += new System.EventHandler(this.chkAutoDisableJAWait0_CheckedChanged);
            // 
            // chkUseJAWait0
            // 
            this.chkUseJAWait0.AutoSize = true;
            this.chkUseJAWait0.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkUseJAWait0.Location = new System.Drawing.Point(6, 19);
            this.chkUseJAWait0.Name = "chkUseJAWait0";
            this.chkUseJAWait0.Size = new System.Drawing.Size(88, 17);
            this.chkUseJAWait0.TabIndex = 9;
            this.chkUseJAWait0.Text = "Use JAWait0";
            this.chkUseJAWait0.UseVisualStyleBackColor = true;
            this.chkUseJAWait0.CheckedChanged += new System.EventHandler(this.chkUseJAWait0_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DeepPink;
            this.label3.Location = new System.Drawing.Point(9, 512);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Clipper - by Kenshi";
            // 
            // msMainMenu
            // 
            this.msMainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(230, 24);
            this.msMainMenu.TabIndex = 6;
            this.msMainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem2.Text = "Select Character";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.selectCharacterToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 545);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Detect Status:";
            // 
            // lblDetectStatus
            // 
            this.lblDetectStatus.AutoSize = true;
            this.lblDetectStatus.Location = new System.Drawing.Point(100, 545);
            this.lblDetectStatus.Name = "lblDetectStatus";
            this.lblDetectStatus.Size = new System.Drawing.Size(66, 13);
            this.lblDetectStatus.TabIndex = 8;
            this.lblDetectStatus.Text = "Undetected!";
            // 
            // niTrayIcon
            // 
            this.niTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niTrayIcon.Icon")));
            this.niTrayIcon.Text = "Clipper";
            this.niTrayIcon.Visible = true;
            this.niTrayIcon.Click += new System.EventHandler(this.niTrayIcon_Click);
            // 
            // ssMainStrip
            // 
            this.ssMainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssCharacterNameLabel,
            this.ssCharacterName});
            this.ssMainStrip.Location = new System.Drawing.Point(0, 570);
            this.ssMainStrip.Name = "ssMainStrip";
            this.ssMainStrip.Size = new System.Drawing.Size(230, 22);
            this.ssMainStrip.SizingGrip = false;
            this.ssMainStrip.TabIndex = 9;
            // 
            // ssCharacterNameLabel
            // 
            this.ssCharacterNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ssCharacterNameLabel.Name = "ssCharacterNameLabel";
            this.ssCharacterNameLabel.Size = new System.Drawing.Size(76, 17);
            this.ssCharacterNameLabel.Text = "Attached to:";
            // 
            // ssCharacterName
            // 
            this.ssCharacterName.Name = "ssCharacterName";
            this.ssCharacterName.Size = new System.Drawing.Size(32, 17);
            this.ssCharacterName.Text = "None";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 592);
            this.Controls.Add(this.ssMainStrip);
            this.Controls.Add(this.chkUseExclusions);
            this.Controls.Add(this.lblDetectStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.msMainMenu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMainMenu;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clipper";
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayerSpeedDisabled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayerSpeed)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.ssMainStrip.ResumeLayout(false);
            this.ssMainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkStatusAutoDisable;
        private System.Windows.Forms.RadioButton rbStatusMaintenance;
        private System.Windows.Forms.RadioButton rbStatusChocobo;
        private System.Windows.Forms.RadioButton rbStatusNone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUseGMFlag;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar tbPlayerSpeed;
        private System.Windows.Forms.CheckBox chkEnableSpeedHack;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPosSE;
        private System.Windows.Forms.Button btnPosS;
        private System.Windows.Forms.Button btnPosSW;
        private System.Windows.Forms.Button btnPosD;
        private System.Windows.Forms.Button btnPosE;
        private System.Windows.Forms.Button btnPosW;
        private System.Windows.Forms.Button btnPosU;
        private System.Windows.Forms.Button btnPosNE;
        private System.Windows.Forms.Button btnPosN;
        private System.Windows.Forms.Button btnPosNW;
        private System.Windows.Forms.CheckBox chkLockZCoord;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAutoDisableJAWait0;
        private System.Windows.Forms.CheckBox chkUseJAWait0;
        private System.Windows.Forms.CheckBox chkUseExclusions;
        private System.Windows.Forms.CheckBox chkAutoDisableSpeedHack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDetectStatus;
        private System.Windows.Forms.NotifyIcon niTrayIcon;
        private System.Windows.Forms.CheckBox chkAutoDisablePositionHacks;
        private System.Windows.Forms.Label lblPlayerSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip ssMainStrip;
        private System.Windows.Forms.ToolStripStatusLabel ssCharacterNameLabel;
        private System.Windows.Forms.ToolStripStatusLabel ssCharacterName;
        private System.Windows.Forms.Label lblPlayerSpeedDisabled;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tbPlayerSpeedDisabled;
    }

    /// <summary>
    /// InvokerExtensions Class Implementation
    /// </summary>
    public static class InvokerExtensions
    {
        /// <summary>
        /// Extends Invoke to allow cross-thread invoking safely.
        /// 
        /// Credits: http://stackoverflow.com/a/711419/1080150
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            try
            {
                if (@this.InvokeRequired)
                    @this.Invoke(action, new object[] { @this });
                else
                    action(@this);
            }
            catch { /* Swallow exceptions from this.. */ }
        }
    }
}
