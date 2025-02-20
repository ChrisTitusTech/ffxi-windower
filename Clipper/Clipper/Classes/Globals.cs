
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
    using System.Diagnostics;

    public class Globals
    {
        /// <summary>
        /// Internal singleton instance of this class.
        /// </summary>
        private static Globals m_Instance;

        /// <summary>
        /// Private Constructor
        /// </summary>
        private Globals()
        {
        }

        /// <summary>
        /// Gets the instance of this class.
        /// </summary>
        public static Globals Instance
        {
            get { return m_Instance ?? (m_Instance = new Globals()); }
        }

        /// <summary>
        /// Returns an offset from the loaded configuration.
        /// </summary>
        /// <param name="strOffsetName"></param>
        /// <returns></returns>
        public Int32 GetOffset(String strOffsetName)
        {
            var offset = this.Config.GetOffset(strOffsetName);
            return (offset != null) ? offset.Value : 0;
        }

        /// <summary>
        /// Returns a patch from the loaded configuration.
        /// </summary>
        /// <param name="strPatchName"></param>
        /// <returns></returns>
        public Patch GetPatch(String strPatchName)
        {
            var patch = this.Config.GetPatch(strPatchName);
            return patch ?? new Patch { Name = "Invalid" };
        }

        /// <summary>
        /// Gets or sets the current configuration.
        /// </summary>
        public Configuration Config { get; set; }

        /// <summary>
        /// Gets or sets the current target process.
        /// </summary>
        public Process CurrentProcess { get; set; }

        /// <summary>
        /// Gets or sets if this application is closing.
        /// </summary>
        public bool IsClosing { get; set; }
    }
}