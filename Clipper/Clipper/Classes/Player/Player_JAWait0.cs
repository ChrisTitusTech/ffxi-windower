
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
    /// JAWait0
    /// Contains definitions and properties used for JAWait0
    /// hacks that can be set on the player.
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Thread delegate to handle player GM Flag hacks.
        /// </summary>
        public static void EnableJAWait0Hack()
        {
            // Enable JAWait0..
            var isJAWaitEnabled = true;
            Player.EnableJAWait0();

            // Loop while the JAWait0 hack is enabled..
            while (Player.UseJAWait0 && !Globals.Instance.IsClosing)
            {
                // Disable the hack..
                if (Player.IsDetectingPlayer && Player.AutoDisableJAWait0Hack && isJAWaitEnabled)
                {
                    isJAWaitEnabled = false;
                    Player.DisableJAWait0();
                }

                // Enablet the hack..
                if (!Player.IsDetectingPlayer && !isJAWaitEnabled)
                {
                    isJAWaitEnabled = true;
                    Player.EnableJAWait0();
                }

                // Sleep to prevent CPU raping..
                Thread.Sleep(10);
            }

            // Ensure we are disabled..
            if (isJAWaitEnabled)
                Player.DisableJAWait0();
        }

        /// <summary>
        /// Enables the JAWait0 hack.
        /// </summary>
        public static void EnableJAWait0()
        {
            var patch1 = Helpers.HexStringToArray(Globals.Instance.GetPatch("JAWAIT0_1").Enabled);
            var patch2 = Helpers.HexStringToArray(Globals.Instance.GetPatch("JAWAIT0_2").Enabled);

            Memory.Poke(Globals.Instance.CurrentProcess, PointerFactory.Instance["JAWAIT0_1"], patch1);
            Memory.Poke(Globals.Instance.CurrentProcess, PointerFactory.Instance["JAWAIT0_2"], patch2);
        }

        /// <summary>
        /// Disables the JAWait0 hack.
        /// </summary>
        public static void DisableJAWait0()
        {
            var patch1 = Helpers.HexStringToArray(Globals.Instance.GetPatch("JAWAIT0_1").Disabled);
            var patch2 = Helpers.HexStringToArray(Globals.Instance.GetPatch("JAWAIT0_2").Disabled);

            Memory.Poke(Globals.Instance.CurrentProcess, PointerFactory.Instance["JAWAIT0_1"], patch1);
            Memory.Poke(Globals.Instance.CurrentProcess, PointerFactory.Instance["JAWAIT0_2"], patch2);
        }

        /// <summary>
        /// Gets or sets the JAWait0 usage flag.
        /// </summary>
        public static bool UseJAWait0 { get; set; }

        /// <summary>
        /// Gets or sets the JAWait0 auto-disable flag.
        /// </summary>
        public static bool AutoDisableJAWait0Hack { get; set; }

        /// <summary>
        /// Internal thread object for the JAWait0 hack.
        /// </summary>
        internal static Thread JAWait0Thread { get; set; }
    }
}