
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
    using System.Linq;

    public static class Helpers
    {
        /// <summary>
        /// Converts a hex string to a byte array.
        /// </summary>
        /// <param name="strPattern"></param>
        /// <returns></returns>
        public static byte[] HexStringToArray(String strPattern)
        {
            return Enumerable.Range(0, strPattern.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(strPattern.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
