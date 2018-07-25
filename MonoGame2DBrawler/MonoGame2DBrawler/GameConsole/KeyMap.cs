#region File Description
//-----------------------------------------------------------------------------
// KeyMap.cs
//
// Game Console
// Copyright (c) 2009 VosSoft
//-----------------------------------------------------------------------------
#endregion
#region License
//-----------------------------------------------------------------------------
// The MIT License (MIT)
//
// Copyright (c) 2009 VosSoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Input;
#endregion

namespace VosSoft.Xna.GameConsole
{
    /// <summary>
    /// Defines a key map to be used with the game console.
    /// </summary>
    public class KeyMap
    {
        #region Static Fields

        static Dictionary<string, KeyMap> keyMaps;

        #endregion

        #region Static Properties

        /// <summary>
        /// Gets the letter key map (A-Z, a-z).
        /// </summary>
        public static KeyMap LetterKeyMap
        {
            get
            {
                KeyMap keyMap = new KeyMap("letters");

                for (Keys key = Keys.A; key <= Keys.Z; key++)
                {
                    keyMap.AddChar(key, KeyModifier.Shift, (char)key);
                    keyMap.AddChar(key, KeyModifier.None, (char)(key + 32));
                }

                return keyMap;
            }
        }

        /// <summary>
        /// Gets the number key map (0-9).
        /// </summary>
        public static KeyMap NumberKeyMap
        {
            get
            {
                KeyMap keyMap = new KeyMap("numbers");

                for (Keys key = Keys.D0; key <= Keys.D9; key++)
                {
                    keyMap.AddChar(key, KeyModifier.None, (char)key);
                }

                return keyMap;
            }
        }

        /// <summary>
        /// Gets the default key map (A-Z, a-z, 0-9 and space).
        /// </summary>
        public static KeyMap DefaultKeyMap
        {
            get
            {
                KeyMap keyMap = new KeyMap("default");

                for (Keys key = Keys.A; key <= Keys.Z; key++)
                {
                    keyMap.AddChar(key, KeyModifier.Shift, (char)key);
                    keyMap.AddChar(key, KeyModifier.None, (char)(key + 32));
                }

                for (Keys key = Keys.D0; key <= Keys.D9; key++)
                {
                    keyMap.AddChar(key, KeyModifier.None, (char)key);
                }

                keyMap.AddChar(Keys.Space, KeyModifier.None, ' ');

                return keyMap;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Initializes the keyMap dictionary with the default key map
        /// and loads all key map files from the key maps directory.
        /// </summary>
        internal static void LoadKeyMaps(string keyMapsDirectory)
        {
            keyMaps = new Dictionary<string, KeyMap>();

            try
            {
                if (Directory.Exists(keyMapsDirectory))
                {
                    string[] files = Directory.GetFiles(keyMapsDirectory, "*.xml");
                    foreach (string file in files)
                    {
                        XmlDocument xmlFile = new XmlDocument();
                        xmlFile.Load(file);

                        XmlNode keymapNode = xmlFile.GetElementsByTagName("keymap")[0];
                        if (keymapNode != null)
                        {
                            KeyMap keyMap = new KeyMap(keymapNode.Attributes["description"].Value);
                            if (!keyMaps.ContainsKey(keyMap.Description))
                            {
                                foreach (XmlNode node in keymapNode.SelectNodes("key"))
                                {
                                    XmlAttribute modifierNode = node.Attributes["modifier"];

                                    Keys key = (Keys)Enum.Parse(typeof(Keys), node.Attributes["keys"].Value, true);
                                    KeyModifier modifier = modifierNode == null ? KeyModifier.None :
                                        (KeyModifier)Enum.Parse(typeof(KeyModifier), modifierNode.Value, true);
                                    char ch = node.InnerText.Length == 1 ? node.InnerText.ToCharArray(0, 1)[0] : ' ';

                                    keyMap.AddChar(key, modifier, ch);
                                }
                                keyMaps.Add(keyMap.Description, keyMap);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading key maps: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the key map with the specified description, if there is no
        /// key map with that description in the dictionary null is returned.
        /// </summary>
        /// <param name="description">The description in the xml key map file.</param>
        /// <returns>The key map or null.</returns>
        public static KeyMap GetKeyMap(string description)
        {
            if (keyMaps.ContainsKey(description))
            {
                return keyMaps[description];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a key map to the key map dictionary.
        /// </summary>
        /// <param name="keyMap">The new key map.</param>
        /// <returns>Returns true if the key map was successfully added, otherwise false
        /// (means there is already a key map with the same description).</returns>
        public static bool AddKeyMap(KeyMap keyMap)
        {
            if (keyMaps.ContainsKey(keyMap.Description))
            {
                return false;
            }
            else
            {
                keyMaps.Add(keyMap.Description, keyMap);
                return true;
            }
        }

        /// <summary>
        /// Removes a key map from the key map dictionary.
        /// </summary>
        /// <param name="description">The description of the key map that should be removed.</param>
        public static void RemoveKeyMap(string description)
        {
            if (keyMaps.ContainsKey(description))
            {
                keyMaps.Remove(description);
            }
        }

        /// <summary>
        /// Gets an array of all key map descriptions in the dictionary.
        /// </summary>
        /// <returns>Returns an array of all key map descriptions in the dictionary.</returns>
        public static string[] GetKeyMaps()
        {
            string[] keyMapDescriptions = new string[keyMaps.Keys.Count];

            int i = 0;
            foreach (string description in keyMaps.Keys)
            {
                keyMapDescriptions[i++] = description;
            }

            return keyMapDescriptions;
        }

        #endregion

        #region Fields

        Dictionary<Keys, Dictionary<KeyModifier, char>> map;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the description of the key map.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the whole dictionary for the pressed key, including
        /// all stored modifier keys and char combinations.
        /// </summary>
        /// <param name="key">The pressed key.</param>
        /// <returns><para>The dictionary that represents all stored modifier
        /// keys and char combinations for the pressed key.</para>
        /// <para>If the the pressed key is not in the key map, null is returned.</para></returns>
        public Dictionary<KeyModifier, char> this[Keys key]
        {
            get
            {
                return map.ContainsKey(key) ? map[key] : null;
            }
        }

        /// <summary>
        /// Gets the char for the key and and modifier key combination.
        /// </summary>
        /// <param name="key">The pressed key.</param>
        /// <param name="mod">The pressed modifier key.</param>
        /// <returns>The char in the key map or the 0 character, if there is
        /// no combination for the pressed key and modifier key.</returns>
        public char this[Keys key, KeyModifier mod]
        {
            get
            {
                return this[key] == null ? '\0' : (this[key].ContainsKey(mod) ? map[key][mod] : '\0');
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new key map with an empty description.
        /// </summary>
        public KeyMap()
            : this("")
        {
        }

        /// <summary>
        /// Creates a new key map.
        /// </summary>
        /// <param name="description"><para>The description for the new key map.</para>
        /// <para>This will be the unique identifier for the key map.</para></param>
        public KeyMap(string description)
        {
            Description = description;
            map = new Dictionary<Keys, Dictionary<KeyModifier, char>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a character to the key map.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mod">The modifier key.</param>
        /// <param name="ch">The new character.</param>
        public void AddChar(Keys key, KeyModifier mod, char ch)
        {
            if (!map.ContainsKey(key))
            {
                map.Add(key, new Dictionary<KeyModifier, char>());
            }
            map[key][mod] = ch;
        }

        /// <summary>
        /// Removes a character from the key map.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mod">The modifier key.</param>
        public void RemoveChar(Keys key, KeyModifier mod)
        {
            if (map.ContainsKey(key))
            {
                map[key].Remove(mod);
                if (map[key].Count == 0)
                {
                    map.Remove(key);
                }
            }
        }

        #endregion

    }

    #region Enum KeyModifier

    /// <summary>
    /// Defines the modifier keys.
    /// </summary>
    [Flags]
    public enum KeyModifier
    {
        /// <summary>
        /// No modifier key pressed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Shift modifier key pressed.
        /// </summary>
        Shift = 0x1,
        /// <summary>
        /// Control modifier key pressed.
        /// </summary>
        Control = 0x2,
        /// <summary>
        /// Alt modifier key pressed.
        /// </summary>
        Alt = 0x4,

        /// <summary>
        /// Control and alt modifier keys pressed.
        /// </summary>
        ControlAlt = Control | Alt,
        /// <summary>
        /// Control and shift modifier keys pressed.
        /// </summary>
        ControlShift = Control | Shift,
        /// <summary>
        /// Shift and alt modifier keys pressed.
        /// </summary>
        ShiftAlt = Shift | Alt,
        /// <summary>
        /// Control, alt and shift modifier keys pressed.
        /// </summary>
        ControlShiftAlt = Control | Shift | Alt,
    }

    #endregion
}
