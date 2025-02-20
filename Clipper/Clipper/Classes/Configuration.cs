
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Signature Object Class
    /// 
    /// Holds definition information for a single signature.
    /// </summary>
    public class Signature
    {
        /// <summary>
        /// The name of this signature.
        /// </summary>
        [XmlAttribute("name")]
        public String Name;

        /// <summary>
        /// The pattern of this signature.
        /// </summary>
        [XmlAttribute("pattern")]
        public String Pattern;

        /// <summary>
        /// The mask of this signature.
        /// </summary>
        [XmlAttribute("mask")]
        public String Mask;

        /// <summary>
        /// The offset of this signature.
        /// </summary>
        [XmlAttribute("offset")]
        public Int32 Offset;
    }

    /// <summary>
    /// Offset Object Class
    /// 
    /// Holds definition information for a single offset.
    /// </summary>
    public class Offset
    {
        /// <summary>
        /// The name of this offset.
        /// </summary>
        [XmlAttribute("name")]
        public String Name;

        /// <summary>
        /// The value of this offset.
        /// </summary>
        [XmlAttribute("value")]
        public Int32 Value;
    }

    /// <summary>
    /// Patch Object Class
    /// 
    /// Holds definition information for a single patch.
    /// </summary>
    public class Patch
    {
        /// <summary>
        /// The name of this patch.
        /// </summary>
        [XmlAttribute("name")]
        public String Name;

        /// <summary>
        /// The enabled patch code for this patch.
        /// </summary>
        [XmlAttribute("enabled")]
        public String Enabled;

        /// <summary>
        /// The disabled patch code for this patch.
        /// </summary>
        [XmlAttribute("disabled")]
        public String Disabled;
    }

    /// <summary>
    /// Clipper Configuration Class
    /// 
    /// Main configuration class which is deserialized from XML.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Configuration()
        {
            this.Signatures = new List<Signature>();
            this.Offsets = new List<Offset>();
            this.Patches = new List<Patch>();
            this.ExcludedPlayers = new List<String>();
            this.ExcludedZones = new List<Int32>();
        }

        /// <summary>
        /// Always on top configuration setting.
        /// </summary>
        public bool AlwaysOnTop { get; set; }

        /// <summary>
        /// Minimize to tray configuration setting.
        /// </summary>
        public bool MinimizeToTray { get; set; }

        /// <summary>
        /// Gets or sets the delay between zoning and reactiving tools.
        /// </summary>
        public int ZoneDelay { get; set; }

        /// <summary>
        /// List of signatures from the configuration file.
        /// </summary>
        public List<Signature> Signatures { get; set; }

        /// <summary>
        /// List of offsets from the configuration file.
        /// </summary>
        public List<Offset> Offsets { get; set; }

        /// <summary>
        /// List of patches from the configuration file.
        /// </summary>
        public List<Patch> Patches { get; set; }

        /// <summary>
        /// List of excluded players when checking for detected players.
        /// </summary>
        [XmlArray]
        [XmlArrayItem(ElementName = "Exclude")]
        public List<String> ExcludedPlayers { get; set; }

        /// <summary>
        /// List of excluded zones when checking for detected players.
        /// </summary>
        [XmlArray]
        [XmlArrayItem(ElementName = "Exclude")]
        public List<Int32> ExcludedZones { get; set; }

        /// <summary>
        /// Obtains a signature from the signature list by its name.
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public Signature GetSignature(String strName)
        {
            return (from s in this.Signatures
                    where s.Name.ToLower() == strName.ToLower()
                    select s).SingleOrDefault();
        }

        /// <summary>
        /// Obtains an offset from the offset list by its name.
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public Offset GetOffset(String strName)
        {
            return (from s in this.Offsets
                    where s.Name.ToLower() == strName.ToLower()
                    select s).SingleOrDefault();
        }

        /// <summary>
        /// Obtains a patch from the offset list by its name.
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public Patch GetPatch(String strName)
        {
            return (from s in this.Patches
                    where s.Name.ToLower() == strName.ToLower()
                    select s).SingleOrDefault();
        }
    }
}