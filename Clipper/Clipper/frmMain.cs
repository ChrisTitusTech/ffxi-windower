
/**
 * Clipper (c) atom0s 2004 - 2013 [wiccaan@comcast.net]
 * ---------------------------------------------------------------------------------
 * This file is part of Clipper.
 *
 *      Clipper is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      Clipper is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU Lesser General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with Clipper.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace Clipper
{
    using Classes;
    using Classes.Player;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    public partial class frmMain : Form
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            // Set always on top option..
            this.TopMost = Globals.Instance.Config.AlwaysOnTop;

            // Set minimize to tray option..
            this.niTrayIcon.Visible = Globals.Instance.Config.MinimizeToTray;

            // Disable the form controls..
            this.DisableFormControls();

            // Show character selector..
            var charSelect = new frmSelectCharacter();
            if (charSelect.ShowDialog() == DialogResult.OK)
            {
                this.EnableFormControls();
            }

            // Setup application exit event handler..
            Globals.Instance.IsClosing = false;
            Application.ApplicationExit += (sender, args) =>
                {
                    // Inform the application we are closing..
                    Globals.Instance.IsClosing = true;

                    // Cancel the JAWait0 thread..
                    if (Player.JAWait0Thread != null)
                        Player.JAWait0Thread.Join();
                };

            // Enable scan thread..
            var scanThread = new Thread(() =>
                {
                    while (!Globals.Instance.IsClosing)
                    {
                        Thread.Sleep(100);

                        // Skip if process is missing..
                        if (Globals.Instance.CurrentProcess == null || Globals.Instance.CurrentProcess.HasExited)
                        {
                            continue;
                        }

                        // Update the players speed..
                        this.InvokeEx(f =>
                            {
                                var speed = Player.Speed;
                                f.lblPlayerSpeed.Text = string.Format("{0} ({1}%)", speed.ToString(), Math.Round(100 * (speed / 5 - 1)));
                            });

                        // Scan for detections..
                        Player.ScanForPlayers();

                        // Disply status..
                        if (Player.IsDetectingPlayer)
                        {
                            this.InvokeEx(f => f.lblDetectStatus.ForeColor = Color.Red);
                            this.InvokeEx(f => f.lblDetectStatus.Text = Player.DetectedPlayerName);
                        }
                        else
                        {
                            this.InvokeEx(f => f.lblDetectStatus.ForeColor = Color.Lime);
                            this.InvokeEx(f => f.lblDetectStatus.Text = "Undetected!");
                        }

                        // Display player name attached to..
                        this.InvokeEx(f =>
                            {
                                var name = Player.Name; 
                                f.ssCharacterName.Text = (String.IsNullOrEmpty(name)) ? "None" : name;
                                f.niTrayIcon.Text = (String.IsNullOrEmpty(name)) ? "Clipper" : name; 
                            });
                    }
                })
                {
                    IsBackground = true
                };
            scanThread.Start();

            // Tooltip Initialization..
            var toolTip = new ToolTip
                {
                    AutoPopDelay = 5000,
                    InitialDelay = 500,
                    ReshowDelay = 500,
                    ShowAlways = true
                };
            toolTip.SetToolTip(this.chkUseExclusions, "Enables the use of player and zone exclusions.");
            toolTip.SetToolTip(this.chkAutoDisableJAWait0, "Enables or disables the auto-disable feature while using JAWait0.");
            toolTip.SetToolTip(this.chkAutoDisablePositionHacks, "Enables or disables the auto-disable feature while using positional hacks.");
            toolTip.SetToolTip(this.chkAutoDisableSpeedHack, "Enables or disables the auto-disable feature while using the speed hack.");
            toolTip.SetToolTip(this.chkEnableSpeedHack, "Enables or disables the use of the speed hack.");
            toolTip.SetToolTip(this.chkLockZCoord, "Enables or disables locking the players Z axis.");
            toolTip.SetToolTip(this.chkStatusAutoDisable, "Enables or disables the auto-disable feature while using status hacks.");
            toolTip.SetToolTip(this.chkUseGMFlag, "Enables or disables the use of the GM flag hack.");
            toolTip.SetToolTip(this.chkUseJAWait0, "Enables or disables the use of the JAWai0 hack.");
            toolTip.SetToolTip(this.rbStatusNone, "Sets the players status to none and disables the status hack.");
            toolTip.SetToolTip(this.rbStatusChocobo, "Sets the players status to riding a chocobo.");
            toolTip.SetToolTip(this.rbStatusMaintenance, "Sets the players status to maintenance mode. (Clipping)");
            toolTip.SetToolTip(this.tbPlayerSpeed, "Sets the players speed while using the speed hack.");
            toolTip.SetToolTip(this.tbPlayerSpeedDisabled, "Sets the players disabled speed while using the speed hack.");
            toolTip.SetToolTip(this.btnPosD, "Bumps a players position down.");
            toolTip.SetToolTip(this.btnPosE, "Bumps a players position east.");
            toolTip.SetToolTip(this.btnPosN, "Bumps a players position north.");
            toolTip.SetToolTip(this.btnPosNE, "Bumps a players position north-east.");
            toolTip.SetToolTip(this.btnPosNW, "Bumps a players position north-west.");
            toolTip.SetToolTip(this.btnPosS, "Bumps a players position south.");
            toolTip.SetToolTip(this.btnPosSE, "Bumps a players position south-east.");
            toolTip.SetToolTip(this.btnPosSW, "Bumps a players position south-west.");
            toolTip.SetToolTip(this.btnPosU, "Bumps a players position up.");
            toolTip.SetToolTip(this.btnPosW, "Bumps a players position west.");
        }

        #region "== Form Control Adjustments"
        /// <summary>
        /// Enables the various controls on the form..
        /// </summary>
        private void EnableFormControls()
        {
            // Enable groupboxes..
            foreach (var c in from Control c in this.Controls where !c.GetType().Name.ToLower().Contains("menu") select c)
            {
                c.Enabled = true;
            }
        }

        /// <summary>
        /// Disables the various controls on the form..
        /// </summary>
        private void DisableFormControls()
        {
            // Disable groupboxes..
            foreach (var c in from Control c in this.Controls where !c.GetType().Name.ToLower().Contains("menu") select c)
            {
                c.Enabled = false;
            }

            // Uncheck all checkboxes..
            foreach (var c in this.Controls)
            {
                if (c.GetType().ToString().IndexOf("groupbox", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    foreach (var cc in ((GroupBox)c).Controls)
                    {
                        if (cc.GetType().ToString().IndexOf("checkbox", StringComparison.OrdinalIgnoreCase) >= 0)
                            ((CheckBox)cc).Checked = false;
                    }
                }

                if (c.GetType().ToString().IndexOf("checkbox", StringComparison.OrdinalIgnoreCase) >= 0)
                    ((CheckBox)c).Checked = false;
            }

            // Disable status radio selection..
            rbStatusNone.Checked = true;
        }
        #endregion

        #region "== Status Hack Handlers"
        /// <summary>
        /// Status hack radio check changed callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioStatus_CheckedChanged(object sender, EventArgs e)
        {
            // Enable status hack thread..
            if (Player.UseStatusHack == false && !this.rbStatusNone.Checked)
            {
                // Set if we are using the status hack..
                Player.UseStatusHack = !this.rbStatusNone.Checked;

                var statusThread = new Thread(Player.EnableStatusHack)
                    {
                        IsBackground = true
                    };
                statusThread.Start();
            }

            // Set if we are using the status hack..
            Player.UseStatusHack = !this.rbStatusNone.Checked;

            // Adjust status mode if required..
            if (Player.UseStatusHack)
            {
                // Set status mode..
                if (this.rbStatusChocobo.Checked)
                    Player.StatusMode = 5;
                else if (this.rbStatusMaintenance.Checked)
                    Player.StatusMode = 31;
                else
                    Player.StatusMode = 0;
            }
            else
            {
                Player.StatusMode = 0;
            }
        }

        /// <summary>
        /// Enables or disables the auto-disable feature for status hacks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkStatusAutoDisable_CheckedChanged(object sender, EventArgs e)
        {
            Player.AutoDisableStatusHack = this.chkStatusAutoDisable.Checked;
        }
        #endregion

        #region "== GM Flag Handlers"
        /// <summary>
        /// Enables or disables the GM Flag hack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUseGMFlag_CheckedChanged(object sender, EventArgs e)
        {
            // Enable the GM Flag hack thread..
            if (Player.UseGMFlag == false && this.chkUseGMFlag.Checked)
            {
                Player.UseGMFlag = this.chkUseGMFlag.Checked;

                var flagThread = new Thread(Player.EnableFlagHack)
                    {
                        IsBackground = true
                    };
                flagThread.Start();
            }

            Player.UseGMFlag = this.chkUseGMFlag.Checked;
        }
        #endregion

        #region "== JAWait0 Hack Handlers"
        /// <summary>
        /// Enables or disables the JAWait0 hack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUseJAWait0_CheckedChanged(object sender, EventArgs e)
        {
            // Enable the GM Flag hack thread..
            if (Player.UseJAWait0 == false && this.chkUseJAWait0.Checked)
            {
                // Cancel the current thread..
                if (Player.JAWait0Thread != null)
                {
                    Player.UseJAWait0 = false;
                    Player.JAWait0Thread.Join();
                }

                Player.UseJAWait0 = this.chkUseJAWait0.Checked;

                Player.JAWait0Thread = new Thread(Player.EnableJAWait0Hack)
                    {
                        IsBackground = true
                    };
                Player.JAWait0Thread.Start();
            }

            Player.UseJAWait0 = this.chkUseJAWait0.Checked;
        }

        /// <summary>
        /// Enables or disables the auto-disable JAWait0 hack feature.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoDisableJAWait0_CheckedChanged(object sender, EventArgs e)
        {
            Player.AutoDisableJAWait0Hack = this.chkAutoDisableJAWait0.Checked;
        }
        #endregion

        #region "== Speed Hack Handlers"
        /// <summary>
        /// Enables or disables the speed hack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableSpeedHack_CheckedChanged(object sender, EventArgs e)
        {
            // Enable the speed hack thread..
            if (Player.UseSpeedHack == false && this.chkEnableSpeedHack.Checked)
            {
                Player.UseSpeedHack = this.chkEnableSpeedHack.Checked;

                var speedThread = new Thread(Player.EnableSpeedHack)
                    {
                        IsBackground = true
                    };
                speedThread.Start();
            }

            Player.UseSpeedHack = this.chkEnableSpeedHack.Checked;

            // Reset the speed trackbar..
            if (Player.UseSpeedHack == false)
            {
                this.tbPlayerSpeed.Value = 1;
                Player.SpeedAmount = 5.0f;
            }
        }

        /// <summary>
        /// Enables or disables the auto-disable feature for the speed hack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoDisableSpeedHack_CheckedChanged(object sender, EventArgs e)
        {
            Player.AutoDisableSpeedHack = this.chkAutoDisableSpeedHack.Checked;
        }

        /// <summary>
        /// Adjusts the players speed based on the track bars position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPlayerSpeed_Scroll(object sender, EventArgs e)
        {
            var speedMult = this.tbPlayerSpeed.Value * 0.1f;
            var speed = 5.0f + (speedMult - 0.1f);

            Player.SpeedAmount = speed;
        }

        /// <summary>
        /// Adjusts the players speed when disabled based on the track bars position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPlayerSpeedDisabled_Scroll(object sender, EventArgs e)
        {
            var speedMult = this.tbPlayerSpeedDisabled.Value * 0.1f;
            var speed = 5.0f + (speedMult - 0.1f);

            Player.SpeedAmountDisabled = speed;

            this.lblPlayerSpeedDisabled.Text = speed.ToString();
        }
        #endregion

        #region "== Position Adjustment Hacks"
        /// <summary>
        /// Enables or disables the Z Coord locking hack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLockZCoord_CheckedChanged(object sender, EventArgs e)
        {
            // Enable the Z Coord hack thread..
            if (Player.UseZCoordHack == false && this.chkLockZCoord.Checked)
            {
                Player.UseZCoordHack = this.chkLockZCoord.Checked;

                // Set the Z Coord to lock to..
                Player.LockedZCoord = Player.ZCoord;

                // Start Z Coord lock thread..
                var coordThread = new Thread(Player.EnableZCoordHack)
                    {
                        IsBackground = true
                    };
                coordThread.Start();
            }

            Player.UseZCoordHack = this.chkLockZCoord.Checked;
        }

        /// <summary>
        /// Enables or disables the auto-disable position adjustment hack feature.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoDisablePositionHacks_CheckedChanged(object sender, EventArgs e)
        {
            Player.AutoDisablePositionHacks = this.chkAutoDisablePositionHacks.Checked;
        }

        /// <summary>
        /// Adjusts a players position to the north.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosN_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.N, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position to the south.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosS_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.S, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position to the east.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosE_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.E, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position to the west.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosW_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.W, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosU_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.Up, 1.0f);
            if (this.chkLockZCoord.Checked)
                Player.LockedZCoord -= 1.0f;
        }

        /// <summary>
        /// Adjusts a players position down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosD_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.Down, 1.0f);
            if (this.chkLockZCoord.Checked)
                Player.LockedZCoord += 1.0f;
        }

        /// <summary>
        /// Adjusts a players position to the north-west.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosNW_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.NW, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position to the north-east.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosNE_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.NE, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position to the south-west.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosSW_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.SW, 1.0f);
        }

        /// <summary>
        /// Adjusts a players position to the south-east.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosSE_Click(object sender, EventArgs e)
        {
            Player.AdjustPosition(Player.PositionDirection.SE, 1.0f);
        }
        #endregion

        #region "== Exclusion Handler"
        /// <summary>
        /// Determines if Clipper should use exclusions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUseExclusions_CheckedChanged(object sender, EventArgs e)
        {
            Player.UseExclusions = this.chkUseExclusions.Checked;
        }
        #endregion

        #region "== File Menu Handlers"
        /// <summary>
        /// Launches the character selection form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ensure all hacks are disabled..
            Player.UseExclusions = false;
            Player.UseGMFlag = false;
            Player.UseJAWait0 = false;
            Player.UseSpeedHack = false;
            Player.UseStatusHack = false;
            Player.UseZCoordHack = false;

            // Ensure JAWait0 is disabled..
            if (Player.JAWait0Thread != null)
            {
                Player.UseJAWait0 = false;
                Player.JAWait0Thread.Join();
            }

            // Disable the form controls..
            this.DisableFormControls();

            // Show character selector..
            var charSelect = new frmSelectCharacter();
            if (charSelect.ShowDialog() == DialogResult.OK)
            {
                this.EnableFormControls();
            }
        }

        /// <summary>
        /// Termintes the current application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Displays the settings form for this application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new frmSettings();
            settings.ShowDialog();

            // Set always on top option..
            this.TopMost = Globals.Instance.Config.AlwaysOnTop;

            // Set minimize to tray option..
            this.niTrayIcon.Visible = Globals.Instance.Config.MinimizeToTray;
        }

        /// <summary>
        /// Displays the about form for this application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new frmAbout();
            aboutForm.ShowDialog();
        }
        #endregion

        #region "== Form Resizing / Tray Icon Handlers
        /// <summary>
        /// Resize event to use tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (!Globals.Instance.Config.MinimizeToTray)
                return;

            // Show icon when minimized..
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Tray icon click event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void niTrayIcon_Click(object sender, EventArgs e)
        {
            if (!Globals.Instance.Config.MinimizeToTray)
                return;

            if (this.WindowState == FormWindowState.Minimized && this.Visible == false)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }
        #endregion
    }
}