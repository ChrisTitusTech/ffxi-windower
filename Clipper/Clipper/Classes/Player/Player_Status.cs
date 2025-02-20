
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

namespace Clipper.Classes.Player
{
    using System;
    using System.Threading;

    /// <summary>
    /// Player (Partial Class)
    /// 
    /// Status Hacks
    /// Contains definitions and properties used for status
    /// hacks that can be set on the player.
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Thread delegate to handle player status hacks.
        /// </summary>
        public static void EnableStatusHack()
        {
            // Loop while the status hack is enabled..
            while (Player.UseStatusHack && !Globals.Instance.IsClosing)
            {
                // Adjust player status..
                if (Player.IsDetectingPlayer && Player.AutoDisableStatusHack)
                    Player.Status = 0;
                else
                    Player.Status = Player.StatusMode;
                
                // Sleep to prevent CPU raping..
                Thread.Sleep(10);
            }

            // Reset player status..
            Player.Status = 0;
            Player.StatusMode = 0;
        }

        /// <summary>
        /// Gets or sets the Status hack usage flag.
        /// </summary>
        public static bool UseStatusHack { get; set; }

        /// <summary>
        /// Gets or sets the Status auto-disable flag.
        /// </summary>
        public static bool AutoDisableStatusHack { get; set; }

        /// <summary>
        /// Gets or sets the player status mode.
        /// </summary>
        public static Int32 StatusMode { get; set; }
    }
}