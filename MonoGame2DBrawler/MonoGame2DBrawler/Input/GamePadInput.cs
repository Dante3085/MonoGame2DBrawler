using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Input
{
    /// <summary>
    /// Maks very common Game inputs with their respective names (Left, Right, etc.).
    /// </summary>
    public class GamePadInput
    {
        public Buttons Left { get; set; }
        public Buttons Up { get; set; }
        public Buttons Right { get; set; }
        public Buttons Down { get; set; }
        public Buttons Attack { get; set; }
    }
}
