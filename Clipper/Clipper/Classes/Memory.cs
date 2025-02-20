
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
    using System.Runtime.InteropServices;

    /// <summary>
    /// Memory Helper Class
    /// 
    /// Wraps ReadProcessMemory and WriteProcessMemory API for easier usage.
    /// </summary>
    public static class Memory
    {
        /// <summary>
        /// Reads the amount of bytes from the given location.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="lpAddress"></param>
        /// <param name="btBuffer"></param>
        /// <returns></returns>
        public static bool Peek(Process p, IntPtr lpAddress, byte[] btBuffer)
        {
            if (p == null || btBuffer == null || btBuffer.Length == 0)
                return false;

            var read = new IntPtr(0);
            return NativeMethods.ReadProcessMemory(p.Handle, lpAddress, btBuffer, (uint)btBuffer.Length, ref read);
        }

        /// <summary>
        /// Writes the given bytes to the given memory location.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="lpAddress"></param>
        /// <param name="btBuffer"></param>
        /// <returns></returns>
        public static bool Poke(Process p, IntPtr lpAddress, byte[] btBuffer)
        {
            if (p == null)
                return false;

            var written = new IntPtr(0);
            return NativeMethods.WriteProcessMemory(p.Handle, lpAddress, btBuffer, (uint)btBuffer.Length, ref written);
        }
    }

    /// <summary>
    /// Internal NativeMethods Import Definitions
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// kernel32.ReadProcessMemory Import
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpNumberOfBytesRead"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] lpBuffer,
            UInt32 nSize,
            ref IntPtr lpNumberOfBytesRead
            );

        /// <summary>
        /// kernel32.WriteProcessMemory Import
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] lpBuffer,
            UInt32 nSize,
            ref IntPtr lpNumberOfBytesWritten
            );
    }
}