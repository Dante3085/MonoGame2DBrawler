using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.General
{
    public interface IInputable
    {
        void HandleKeyboardInput(KeyboardState keyboardState);
        void HandleGamePadInput(GamePadState gamePadState);
    }
}
