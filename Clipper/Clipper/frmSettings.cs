
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
    using Clipper.Classes;
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public partial class frmSettings : Form
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public frmSettings()
        {
            InitializeComponent();

            // Attach controls to properties..
            this.chkAlwaysOnTop.DataBindings.Add("Checked", Globals.Instance.Config, "AlwaysOnTop");
            this.chkMinimizeToTray.DataBindings.Add("Checked", Globals.Instance.Config, "MinimizeToTray");
            this.txtZoneDelay.DataBindings.Add("Text", Globals.Instance.Config, "ZoneDelay");

            // Attach controls to data sources..
            this.lstExcludedPlayers.DataSource = Globals.Instance.Config.ExcludedPlayers;
            this.lstExcludedZones.DataSource = Globals.Instance.Config.ExcludedZones;
        }

        /// <summary>
        /// Deletes the selected player from the excluded players list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletePlayer_Click(object sender, System.EventArgs e)
        {
            // Obtain the selected player..
            var item = this.lstExcludedPlayers.SelectedItem;
            if (item == null) return;

            // Delete the player..
            AdjustExcludedPlayer((String)item, true);
        }

        /// <summary>
        /// Adds a player to the excluded player list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            // Ensure the user entered text to add..
            if (!String.IsNullOrWhiteSpace(this.txtPlayerName.Text))
            {
                // Add the player..
                AdjustExcludedPlayer(this.txtPlayerName.Text);
            }

            this.txtPlayerName.Text = string.Empty;
        }

        /// <summary>
        /// Deletes the selected zone from the excluded zones list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteZone_Click(object sender, EventArgs e)
        {
            // Obtain the selected zone..
            var item = this.lstExcludedZones.SelectedItem;
            if (item == null) return;

            // Delete the zone..
            AdjustExcludedZone((Int32)item, true);
        }

        /// <summary>
        /// Adds a zone to the excluded zone list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddZone_Click(object sender, EventArgs e)
        {
            // Ensure the user entered text to add..
            if (String.IsNullOrWhiteSpace(this.txtZoneID.Text))
                return;

            // Convert the string to an integer..
            var zoneId = 0;
            if (Int32.TryParse(this.txtZoneID.Text, out zoneId))
            {
                // Add the zone..
                AdjustExcludedZone(zoneId);
            }

            this.txtZoneID.Text = string.Empty;
        }

        /// <summary>
        /// Adds or removes players from the excluded player list. Used in order
        /// to rebind the data source to allow the listbox to properly refresh.
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="bRemove"></param>
        private void AdjustExcludedPlayer(String strName, bool bRemove = false)
        {
            // Attempt to remove the name if we just wish to remove it..
            if (bRemove)
                Globals.Instance.Config.ExcludedPlayers.Remove(strName);

            // Attempt to add the name if it is unique..
            else
            {
                // Determine if the name is unique to the list..
                var names = Globals.Instance.Config.ExcludedPlayers;
                var hasName = (from s in names
                               where s.ToLower() == this.txtPlayerName.Text.ToLower()
                               select s).Any();

                if (!hasName) Globals.Instance.Config.ExcludedPlayers.Add(strName);
            }

            // Rebind the data source to refresh the list..
            this.lstExcludedPlayers.DataSource = null;
            this.lstExcludedPlayers.DataSource = Globals.Instance.Config.ExcludedPlayers;
        }

        /// <summary>
        /// Adds or removes zones from the excluded zones list. Used in order
        /// to rebind the data source to allow the listbox to properly refresh.
        /// </summary>
        /// <param name="nZoneId"></param>
        /// <param name="bRemove"></param>
        private void AdjustExcludedZone(Int32 nZoneId, bool bRemove = false)
        {
            // Attempt to remove the zone if we just wish to remove it..
            if (bRemove)
                Globals.Instance.Config.ExcludedZones.Remove(nZoneId);

            // Attempt to add the zone if it is unique..
            else
            {
                if (!Globals.Instance.Config.ExcludedZones.Contains(nZoneId))
                    Globals.Instance.Config.ExcludedZones.Add(nZoneId);
            }

            // Rebind the data source to refresh the list..
            this.lstExcludedZones.DataSource = null;
            this.lstExcludedZones.DataSource = Globals.Instance.Config.ExcludedZones;
        }

        /// <summary>
        /// Saves the configuration changes made to the configuration file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Open configuration file for writing..
                using (var writer = new StreamWriter(Application.StartupPath + "\\Configuration.xml"))
                {
                    // Attempt to save the configuration file..
                    var serializer = new XmlSerializer(typeof(Configuration));
                    serializer.Serialize(writer, Globals.Instance.Config);
                }

                // Close this window..
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Failed to save.. announce the error..
                MessageBox.Show(ex.ToString(), "Failed to save configuration file..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}