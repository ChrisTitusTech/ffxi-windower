
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Player (Partial Class)
    /// 
    /// Auto-Detect 
    /// Contains definitions and properties used for detecting nearby players
    /// to allow hacks to auto-disable themselves.
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Attempts to detect players to disable hacks when near others.
        /// </summary>
        public static void ScanForPlayers()
        {
            // Are we under a zone cooldown?
            if (Player.ZoneCooldown != 0)
            {
                System.Threading.Thread.Sleep(1000);
                Player.DetectedPlayerName = string.Format(">> Zoning Cooldown {0}", Player.ZoneCooldown);
                Player.ZoneCooldown--;
                return;
            }

            // Get the current zone id..
            var currZone = Player.GetZoneId();

            // Are we in an excluded zone..?
            if (Player.UseExclusions)
            {
                // Zone Id Exceptions..
                if (Globals.Instance.Config.ExcludedZones.Any(z => Convert.ToInt16(z) == currZone))
                    return;
            }

            // Have we set a zone yet..?
            if (Player.CurrentZone == 0)
                Player.CurrentZone = currZone;

            // Have we zoned?
            if ((currZone != Player.CurrentZone) && currZone != 0)
            {
                // Set as detected..
                Player.IsDetectingPlayer = true;
                Player.ZoneCooldown = Globals.Instance.Config.ZoneDelay;
                Player.DetectedPlayerName = string.Format(">> Zoning Cooldown {0}", Player.ZoneCooldown);

                // Set the zone for next loop..
                Player.CurrentZone = currZone;

                return;
            }

            var tempBuffer = new byte[4];

            // Read the current NPCMAP pointer..
            if (!Memory.Peek(Globals.Instance.CurrentProcess, PointerFactory.Instance["NPCMAP"], tempBuffer))
                return;
            var npcmap = BitConverter.ToInt32(tempBuffer, 0);

            // Loop the NPC map looking for players..
            for (var x = 0; x < 4000; x++)
            {
                // Read current pointer..
                Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)npcmap + (x * 4), tempBuffer);
                if (BitConverter.ToInt32(tempBuffer, 0) == 0)
                    continue;

                // Read the entity name..
                var nameBuffer = new byte[25];
                Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(tempBuffer, 0) + Globals.Instance.GetOffset("MOB_NAME"), nameBuffer);

                var entityName = Encoding.ASCII.GetString(nameBuffer);
                var nameIndex = Array.FindIndex(entityName.ToCharArray(), c => !char.IsLetter(c));
                if (nameIndex != -1)
                    entityName = entityName.Substring(0, nameIndex);

                // Check if name is valid..
                if (String.IsNullOrWhiteSpace(entityName) || entityName[0] == 0x00 || Player.Name.Equals(entityName) || entityName.Length < 3)
                    continue;

                // Ensure this entity is a player..
                var typeBuffer = new byte[1];
                Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(tempBuffer, 0) + Globals.Instance.GetOffset("MOB_TYPE"), typeBuffer);
                if (typeBuffer[0] != 0)
                {
                    var spawnBuffer = new byte[4];
                    Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(tempBuffer, 0) + Globals.Instance.GetOffset("MOB_SPAWNTYPE"), spawnBuffer);
                    var spawnType = BitConverter.ToInt32(spawnBuffer, 0);
                    if (spawnType != 0x0D && spawnType != 0x01)
                        continue;
                }

                if (Player.UseExclusions)
                {
                    // Check if name is excluded..
                    var foundExcludedPlayer = Globals.Instance.Config.ExcludedPlayers.Any(p => p.ToLower() == entityName.ToLower());
                    if (foundExcludedPlayer)
                        continue;
                }

                // Read the entity visible flag..
                var visibleBuffer = new byte[4];
                Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(tempBuffer, 0) + Globals.Instance.GetOffset("MOB_RENDER"), visibleBuffer);
                if ((BitConverter.ToInt32(visibleBuffer, 0) & 0x200) == 0)
                    continue;

                // Ignore players over 50 yalms away..
                var mobDistance = new byte[4];
                Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(tempBuffer, 0) + Globals.Instance.GetOffset("MOB_DISTANCE"), mobDistance);
                if (Math.Sqrt(BitConverter.ToSingle(mobDistance, 0)) > 50.0f)
                    continue;

                // Detected a player..
                Player.IsDetectingPlayer = true;
                Player.DetectedPlayerName = entityName;
                return;
            }

            Player.IsDetectingPlayer = false;
            Player.DetectedPlayerName = String.Empty;
        }

        /// <summary>
        /// Gets or sets if the auto-detection found a player.
        /// </summary>
        public static bool IsDetectingPlayer { get; set; }

        /// <summary>
        /// Gets or sets the name of the detected player.
        /// </summary>
        public static string DetectedPlayerName { get; set; }

        /// <summary>
        /// Gets or sets if exclusions should be used when scanning for players.
        /// </summary>
        public static bool UseExclusions { get; set; }

        /// <summary>
        /// Gets or sets the players current zone used to compare to the in-memory value.
        /// </summary>
        public static short CurrentZone { get; set; }

        /// <summary>
        /// Gets or sets the current cooldown increment if the player has zoned already.
        /// </summary>
        public static int ZoneCooldown { get; set; }
    }
}