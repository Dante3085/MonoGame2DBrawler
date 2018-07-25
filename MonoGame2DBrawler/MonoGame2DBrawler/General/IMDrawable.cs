using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.General
{
    /// <summary>
    /// IMDrawable Objects can be drawn on the screen with a SpriteBatch.
    /// This interface's name has prefix "M", because XNA Framework already has Interface "IDrawable".
    /// </summary>
    interface IMDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
