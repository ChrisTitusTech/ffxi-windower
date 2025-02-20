
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
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    /// <summary>
    /// Main Program Class
    /// 
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Unhandled exception handler..
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    // Display the exception as a critical error..
                    var e = (Exception)args.ExceptionObject;
                    Program.CriticalError(e.ToString());
                };

            try
            {
                // Attempt to deserialize configuration file..
                using (var reader = new StreamReader(Application.StartupPath + "\\Configuration.xml"))
                {
                    var serializer = new XmlSerializer(typeof(Configuration));
                    Globals.Instance.Config = (Configuration)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                CriticalError("Configuration file is missing or invalid! Cannot start Clipper! Error was:\r\n\r\n" + ex.ToString());
                return;
            }

            // Create and start main form..
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        /// <summary>
        /// Displays a critical error and informs the application to close.
        /// </summary>
        /// <param name="strErrorMsg"></param>
        public static void CriticalError(String strErrorMsg)
        {
            try
            {
                // Delete the previous error log..
                if (File.Exists(Application.StartupPath + "\\Error.txt"))
                    File.Delete(Application.StartupPath + "\\Error.txt");

                // Attempt to write the error to the log file..
                using (var writer = new StreamWriter(Application.StartupPath + "\\Error.txt"))
                {
                    writer.Write(strErrorMsg);
                    writer.Flush();
                }
            }
            catch
            {
            }

            // Display the error..
            MessageBox.Show(strErrorMsg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}