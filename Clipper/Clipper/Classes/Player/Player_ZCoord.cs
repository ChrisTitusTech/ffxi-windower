
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
    /// ZCoord Hack
    /// Contains definitions and properties used for hacking
    /// the players Z Coord. 
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Thread delegate to handle player GM Flag hacks.
        /// </summary>
        public static void EnableZCoordHack()
        {
            // Loop while the Z Coord hack is enabled..
            while (Player.UseZCoordHack && !Globals.Instance.IsClosing)
            {
                // Sleep to prevent CPU raping..
                Thread.Sleep(1);

                // Skip if we are detected and auto-disabled..
                if (Player.IsDetectingPlayer && Player.AutoDisablePositionHacks)
                    continue;

                // Set the player Z Coord..
                Player.ZCoord = Player.LockedZCoord;
            }

            // Reset the locked coord..
            Player.LockedZCoord = 0.0f;
        }

        /// <summary>
        /// Gets or sets the Z Coord hack usage flag.
        /// </summary>
        public static bool UseZCoordHack { get; set; }

        /// <summary>
        /// Gets or sets the Z Coord to lock the player to.
        /// </summary>
        public static float LockedZCoord { get; set; }

        /// <summary>
        /// Gets or sets the position hack auto-disable flag.
        /// </summary>
        public static bool AutoDisablePositionHacks { get; set; }
    }
}