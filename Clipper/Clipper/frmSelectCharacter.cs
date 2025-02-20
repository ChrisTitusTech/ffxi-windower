
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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;

    public partial class frmSelectCharacter : Form
    {
        private List<ProcessEntry> m_ProcessList;
 
        /// <summary>
        /// Defualt Constructor
        /// </summary>
        public frmSelectCharacter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the current character list of found 
        /// PlayOnline instances.
        /// </summary>
        private void LoadCharacterList()
        {
            // Find all PlayOnline processes..
            var procs = (from p in Process.GetProcessesByName("pol")
                         select p);

            // Locate all valid processes..
            foreach (var p in procs)
            {
                // Ensure FFXiMain.dll is loaded..
                if (!(from ProcessModule m in p.Modules
                      where m.ModuleName.ToLower() == "ffximain.dll"
                      select m).Any())
                    continue;

                this.m_ProcessList.Add(new ProcessEntry(p));
            }

            // Set listbox data context..
            this.lstCharacters.DataSource = this.m_ProcessList;

            // Auto-attach for single instance..
            if (this.m_ProcessList.Count() == 1)
            {
                // Set selected process..
                Globals.Instance.CurrentProcess = this.m_ProcessList[0].Process;
                PointerFactory.Instance.UpdateFactory();

                // Close this window..
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            // Auto-close if no processes found..
            if (!this.m_ProcessList.Any())
            {
                // Close this window..
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// Form load event used to load the character list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSelectCharacter_Load(object sender, EventArgs e)
        {
            // Load current process list..
            this.m_ProcessList = new List<ProcessEntry>();
            this.LoadCharacterList();
        }

        /// <summary>
        /// Selects a character based on the selected index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectCharacter_Click(object sender, EventArgs e)
        {
            var selected = (ProcessEntry)this.lstCharacters.SelectedItem;
            if (selected != null)
            {
                // Set selected process..
                Globals.Instance.CurrentProcess = selected.Process;
                PointerFactory.Instance.UpdateFactory();

                // Close this window..
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        /// <summary>
        /// Handles double-clicks on the character select box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCharacters_DoubleClick(object sender, EventArgs e)
        {
            // Invoke button click event..
            this.btnSelectCharacter_Click(this, null);
        }
    }

    /// <summary>
    /// Process Entry Class
    /// 
    /// Used to override the .ToString() method to display the window
    /// title of a process instead of its type name.
    /// </summary>
    public class ProcessEntry
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="p"></param>
        public ProcessEntry(Process p)
        {
            this.Process = p;
        }

        /// <summary>
        /// ToString Override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Process.MainWindowTitle;
        }

        /// <summary>
        /// Gets or sets the process of this entry.
        /// </summary>
        public Process Process { get; set; }
    }
}
