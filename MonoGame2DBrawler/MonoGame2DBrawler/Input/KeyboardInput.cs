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
    public class KeyboardInput
    {

        public Keys Left { get; set; }
        public Keys Up { get; set; }
        public Keys Right { get; set; }
        public Keys Down { get; set; }
        public Keys Attack { get; set; }

        public static bool OnKeyPress(Keys key, KeyboardState currentState, KeyboardState previousState)
        {
            return (currentState.IsKeyDown(key) && previousState.IsKeyUp(Keys.Space));
        }
    }
}
