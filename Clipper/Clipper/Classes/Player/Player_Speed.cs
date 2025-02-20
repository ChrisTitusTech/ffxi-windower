
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
    /// Speed Hacks
    /// Contains definitions and properties used for speed hacks.
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Thread delegate to handle player speed hacks.
        /// </summary>
        public static void EnableSpeedHack()
        {
            // Loop while the speed hack is enabled..
            while (Player.UseSpeedHack && !Globals.Instance.IsClosing)
            {
                if (Player.IsDetectingPlayer && Player.AutoDisableSpeedHack)
                    Player.Speed = Player.SpeedAmountDisabled;
                else
                    Player.Speed = Player.SpeedAmount;

                // Sleep to prevent CPU raping..
                Thread.Sleep(10);
            }

            // Reset speed variables..
            Player.Speed = Player.SpeedAmountDisabled;
            Player.SpeedAmount = 5.0f;
        }

        /// <summary>
        /// Gets or sets the speed hack usage flag.
        /// </summary>
        public static bool UseSpeedHack { get; set; }

        /// <summary>
        /// Gets or sets the player speed hack amount.
        /// </summary>
        public static float SpeedAmount { get; set; }

        /// <summary>
        /// Gets or sets the player speed hack amount when disabled.
        /// </summary>
        public static float SpeedAmountDisabled { get; set; }

        /// <summary>
        /// Gets or sets the Status auto-disable flag.
        /// </summary>
        public static bool AutoDisableSpeedHack { get; set; }
    }
}