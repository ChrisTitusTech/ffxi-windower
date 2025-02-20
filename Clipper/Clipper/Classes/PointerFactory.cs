
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

namespace Clipper.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class PointerFactory
    {
        /// <summary>
        /// Internal singleton instance of this class.
        /// </summary>
        private static PointerFactory m_Instance;

        /// <summary>
        /// Internal pointer table.
        /// </summary>
        private Dictionary<String, IntPtr> m_PointerTable; 

        /// <summary>
        /// Private Constructor
        /// </summary>
        private PointerFactory()
        {
        }

        /// <summary>
        /// Gets the instance of this class.
        /// </summary>
        public static PointerFactory Instance
        {
            get { return m_Instance ?? (m_Instance = new PointerFactory()); }
        }

        /// <summary>
        /// Updates the internal pointer list.
        /// </summary>
        /// <returns></returns>
        public bool UpdateFactory()
        {
            // Reset the pointer table..
            this.m_PointerTable = new Dictionary<String, IntPtr>();

            // Validate current process..
            if (Globals.Instance.CurrentProcess == null || Globals.Instance.CurrentProcess.HasExited)
                return false;

            // Locate FFXiMain.dll..
            var ffxiMain = (from ProcessModule m in Globals.Instance.CurrentProcess.Modules
                            where m.ModuleName.ToLower() == "ffximain.dll"
                            select m).SingleOrDefault();

            if (ffxiMain == null)
                return false;

            // Prepare signature scanner..
            var sigScan = new SigScan(
                Globals.Instance.CurrentProcess, ffxiMain.BaseAddress, ffxiMain.ModuleMemorySize
                );

            // Loop each signature and rescan..
            foreach (var p in Globals.Instance.Config.Signatures)
            {
                // Attempt to locate signature..
                var pointer = sigScan.FindPattern(Helpers.HexStringToArray(p.Pattern), p.Mask, p.Offset);
                if (pointer == IntPtr.Zero)
                {
                    Program.CriticalError("Failed to locate critical signature: " + p.Name);
                    return false;
                }

                // Add pointer to pointer table..
                this.m_PointerTable.Add(p.Name, pointer);
            }

            return true;
        }

        /// <summary>
        /// Returns a pointer from the pointer factory with the given name.
        /// </summary>
        /// <param name="strPointerName"></param>
        /// <returns></returns>
        public IntPtr this[String strPointerName]
        {
            get
            {
                return this.m_PointerTable.ContainsKey(strPointerName) ? this.m_PointerTable[strPointerName] : IntPtr.Zero;
            }
        }
    }
}
