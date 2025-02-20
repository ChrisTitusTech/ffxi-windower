
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
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows.Forms;

    public partial class frmAbout : Form
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public frmAbout()
        {
            InitializeComponent();

            // Update the file version label..
            this.lblVersion.Text = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Link click event to open the latest version location for Clipper.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkClipperDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.lnkClipperDownload.Text);
        }
    }
}
