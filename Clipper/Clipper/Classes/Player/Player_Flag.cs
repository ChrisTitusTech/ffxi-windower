
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
    using System.Threading;

    /// <summary>
    /// Player (Partial Class)
    /// 
    /// GM Flag Hacks
    /// Contains definitions and properties used for GM Flag mode
    /// hacks that can be set on the player.
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Thread delegate to handle player GM Flag hacks.
        /// </summary>
        public static void EnableFlagHack()
        {
            // Loop while the GM Flag hack is enabled..
            while (Player.UseGMFlag && !Globals.Instance.IsClosing)
            {
                // Set player GM Flag..
                Player.GMFlag = 1;

                // Sleep to prevent CPU raping..
                Thread.Sleep(10);
            }

            // Reset player GM Flag..
            Player.GMFlag = 0;
        }

        /// <summary>
        /// Gets or sets the GM flag usage flag.
        /// </summary>
        public static bool UseGMFlag { get; set; }
    }
}