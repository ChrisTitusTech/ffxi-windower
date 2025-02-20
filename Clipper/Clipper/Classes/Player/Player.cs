
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
    using System.Text;

    /// <summary>
    /// Player (Partial Class)
    /// 
    /// Main player class definition that contains helpful functions and properties
    /// specific to the player.
    /// </summary>
    public static partial class Player
    {
        /// <summary>
        /// Position Direction Enumeration
        /// </summary>
        public enum PositionDirection
        {
            N = 0,
            S,
            E,
            W,
            NE,
            SE,
            NW,
            SW,
            Up,
            Down,
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        static Player()
        {
            // Auto-detection initialization..
            Player.IsDetectingPlayer = false;
            Player.DetectedPlayerName = String.Empty;

            // GM Flag hack settings..
            Player.UseGMFlag = false;

            // JAWait0 hack settings..
            Player.UseJAWait0 = false;
            Player.AutoDisableJAWait0Hack = false;

            // Speed hack settings..
            Player.UseSpeedHack = false;
            Player.SpeedAmount = 5.0f;
            Player.SpeedAmountDisabled = 5.0f;
            Player.AutoDisableSpeedHack = false;

            // Status hack settings..
            Player.UseStatusHack = false;
            Player.StatusMode = 0;
            Player.AutoDisableStatusHack = false;

            // Z Coord hack settings.
            Player.UseZCoordHack = false;
            Player.LockedZCoord = 0.0f;
        }
        
        /// <summary>
        /// Obtains the players current mob array pointer.
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetPointer()
        {
            var tempBuffer = new byte[4];

            try
            {
                // Read the current NPCMAP pointer..
                if (!Memory.Peek(Globals.Instance.CurrentProcess, PointerFactory.Instance["NPCMAP"], tempBuffer))
                    throw new Exception("Failed to read NPCMAP pointer.");
                var npcmap = BitConverter.ToInt32(tempBuffer, 0);

                // Read the current OWNPOS pointer..
                if (!Memory.Peek(Globals.Instance.CurrentProcess, PointerFactory.Instance["OWNPOS"], tempBuffer))
                    throw new Exception("Failed to read OWNPOS pointer.");
                var ownpos = BitConverter.ToInt32(tempBuffer, 0);

                // Read the players current NPCMAP index..
                if (!Memory.Peek(Globals.Instance.CurrentProcess, new IntPtr(ownpos + Globals.Instance.GetOffset("PTR_OWNPOS")), tempBuffer))
                    throw new Exception("Failed to read OWNPOS pointer.");
                ownpos = BitConverter.ToInt16(tempBuffer, 0);

                // Read the players current mob array pointer..
                if (!Memory.Peek(Globals.Instance.CurrentProcess, new IntPtr(npcmap + (ownpos * 4)), tempBuffer))
                    throw new Exception("Failed to read player mob pointer.");
                return (IntPtr)BitConverter.ToInt32(tempBuffer, 0);
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// Obtains the players current zone id.
        /// </summary>
        /// <returns></returns>
        public static Int16 GetZoneId()
        {
            // Read main map data pointer..
            var ptrBuffer = new byte[4];
            Memory.Peek(Globals.Instance.CurrentProcess, PointerFactory.Instance["ZONEID"], ptrBuffer);
            Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(ptrBuffer, 0), ptrBuffer);

            // Read map id offset..
            var offBuffer = new byte[4];
            Memory.Peek(Globals.Instance.CurrentProcess, PointerFactory.Instance["ZONEID"] + 7, offBuffer);

            // Read map id..
            var zoneBuffer = new byte[2];
            Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(ptrBuffer, 0) + BitConverter.ToInt32(offBuffer, 0), zoneBuffer);
            return BitConverter.ToInt16(zoneBuffer, 0);
        }

        /// <summary>
        /// Adjusts a players position with the given direction and amount.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fAmount"></param>
        public static void AdjustPosition(PositionDirection dir, float fAmount)
        {
            // Cancel adjustment if we are detected and auto-disabled..
            if (Player.IsDetectingPlayer && Player.AutoDisablePositionHacks)
                return;

            // Get the player pointer..
            var mobPtr = Player.GetPointer();
            if (mobPtr == IntPtr.Zero)
                return;

            // Read the warp struct pointer..
            var warpPtr = new byte[4];
            if (!Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_WARP"), warpPtr))
                return;

            // Read the players current position..
            var tempPosition = new byte[4];
            if (!Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), tempPosition))
                return;
            var fPositionX = BitConverter.ToSingle(tempPosition, 0);

            if (!Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), tempPosition))
                return;
            var fPositionY = BitConverter.ToSingle(tempPosition, 0);

            if (!Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSZ_1"), tempPosition))
                return;
            var fPositionZ = BitConverter.ToSingle(tempPosition, 0);

            switch (dir)
            {
                case PositionDirection.N:
                    {
                        var adjustment = BitConverter.GetBytes(fPositionY + fAmount);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), adjustment);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_2"), adjustment);
                        break;
                    }
                case PositionDirection.S:
                    {
                        var adjustment = BitConverter.GetBytes(fPositionY - fAmount);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), adjustment);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_2"), adjustment);
                        break;
                    }
                case PositionDirection.E:
                    {
                        var adjustment = BitConverter.GetBytes(fPositionX + fAmount);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), adjustment);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_2"), adjustment);
                        break;
                    }
                case PositionDirection.W:
                    {
                        var adjustment = BitConverter.GetBytes(fPositionX - fAmount);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), adjustment);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_2"), adjustment);
                        break;
                    }

                case PositionDirection.NW:
                    {
                        var fDistance = ((int)Math.Floor(fAmount % 2) == 0) ? fAmount / 2 : fAmount / 2 + 1;

                        var adjustmentX = BitConverter.GetBytes(fPositionX - fDistance);
                        var adjustmentY = BitConverter.GetBytes(fPositionY + fDistance);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_2"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), adjustmentY);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_2"), adjustmentY);
                        break;
                    }
                case PositionDirection.NE:
                    {
                        var fDistance = ((int)Math.Floor(fAmount % 2) == 0) ? fAmount / 2 : fAmount / 2 + 1;

                        var adjustmentX = BitConverter.GetBytes(fPositionX + fDistance);
                        var adjustmentY = BitConverter.GetBytes(fPositionY + fDistance);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_2"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), adjustmentY);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_2"), adjustmentY);
                        break;
                    }
                case PositionDirection.SW:
                    {
                        var fDistance = ((int)Math.Floor(fAmount % 2) == 0) ? fAmount / 2 : fAmount / 2 + 1;

                        var adjustmentX = BitConverter.GetBytes(fPositionX - fDistance);
                        var adjustmentY = BitConverter.GetBytes(fPositionY - fDistance);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_2"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), adjustmentY);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_2"), adjustmentY);
                        break;
                    }
                case PositionDirection.SE:
                    {
                        var fDistance = ((int)Math.Floor(fAmount % 2) == 0) ? fAmount / 2 : fAmount / 2 + 1;

                        var adjustmentX = BitConverter.GetBytes(fPositionX + fDistance);
                        var adjustmentY = BitConverter.GetBytes(fPositionY - fDistance);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_1"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSX_2"), adjustmentX);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_1"), adjustmentY);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSY_2"), adjustmentY);
                        break;
                    }

                case PositionDirection.Up:
                    {
                        var adjustment = BitConverter.GetBytes(fPositionZ - fAmount);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSZ_1"), adjustment);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSZ_2"), adjustment);
                        break;
                    }
                case PositionDirection.Down:
                    {
                        var adjustment = BitConverter.GetBytes(fPositionZ + fAmount);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSZ_1"), adjustment);
                        Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpPtr, 0) + Globals.Instance.GetOffset("WARP_POSZ_2"), adjustment);
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets the players name.
        /// </summary>
        public static String Name
        {
            get
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr != IntPtr.Zero)
                {
                    var nameBuffer = new byte[25];
                    Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_NAME"), nameBuffer);

                    var entityName = Encoding.ASCII.GetString(nameBuffer);
                    var nameIndex = Array.FindIndex(entityName.ToCharArray(), c => !char.IsLetter(c));
                    if (nameIndex != -1)
                        entityName = entityName.Substring(0, nameIndex);

                    return entityName;
                }

                return "INVALID NAME";
            }
        }

        /// <summary>
        /// Gets or sets the players GM flag.
        /// </summary>
        public static Int32 GMFlag
        {
            get
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr != IntPtr.Zero)
                {
                    var flagBuffer = new byte[4];
                    Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_FLAG"), flagBuffer);
                    return BitConverter.ToInt32(flagBuffer, 0);
                }

                return -1;
            }
            set
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr == IntPtr.Zero)
                    return;

                var flag = Player.GMFlag;
                if (value == 0)
                {
                    if ((flag & 0x3800) != 0)
                        flag -= 0x3800;
                    Memory.Poke(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_FLAG"), BitConverter.GetBytes(flag));
                }
                else
                {
                    flag |= 0x3800;
                    Memory.Poke(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_FLAG"), BitConverter.GetBytes(flag));
                }
            }
        }

        /// <summary>
        /// Gets or sets the players current speed.
        /// </summary>
        public static float Speed
        {
            get
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr != IntPtr.Zero)
                {
                    var speedBuffer = new byte[4];
                    Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_SPEED"), speedBuffer);
                    return BitConverter.ToSingle(speedBuffer, 0);
                }

                return 0.0f;
            }
            set
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr == IntPtr.Zero)
                    return;

                var speedBuffer = BitConverter.GetBytes(value);
                Memory.Poke(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_SPEED"), speedBuffer);
            }
        }

        /// <summary>
        /// Gets or sets the players current status.
        /// </summary>
        public static Int32 Status
        {
            get
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr != IntPtr.Zero)
                {
                    var statusBuffer = new byte[4];
                    Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_STATUS"), statusBuffer);
                    return BitConverter.ToInt32(statusBuffer, 0);
                }

                return 0;
            }
            set
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr == IntPtr.Zero)
                    return;

                var statusBuffer = BitConverter.GetBytes(value);
                Memory.Poke(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_STATUS"), statusBuffer);
            }
        }

        /// <summary>
        /// Gets or sets the players current Z coord.
        /// </summary>
        public static float ZCoord
        {
            get
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr == IntPtr.Zero)
                    return 0.0f;

                var warpBuffer = new byte[4];
                if (!Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_WARP"), warpBuffer))
                    return 0.0f;

                var tempPosition = new byte[4];
                if (!Memory.Peek(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpBuffer, 0) + Globals.Instance.GetOffset("WARP_POSZ_1"), tempPosition))
                    return 0.0f;

                return BitConverter.ToSingle(tempPosition, 0);
            }
            set
            {
                var mobPtr = Player.GetPointer();
                if (mobPtr == IntPtr.Zero)
                    return;

                var warpBuffer = new byte[4];
                if (!Memory.Peek(Globals.Instance.CurrentProcess, mobPtr + Globals.Instance.GetOffset("MOB_WARP"), warpBuffer))
                    return;

                var posBuffer = BitConverter.GetBytes(value);
                Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpBuffer, 0) + Globals.Instance.GetOffset("WARP_POSZ_1"), posBuffer);
                Memory.Poke(Globals.Instance.CurrentProcess, (IntPtr)BitConverter.ToInt32(warpBuffer, 0) + Globals.Instance.GetOffset("WARP_POSZ_2"), posBuffer);
            }
        }
    }
}