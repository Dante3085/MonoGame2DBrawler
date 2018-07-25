using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.General
{
    public interface ICollidable
    {
        string Name { get; }
        Rectangle BoundingBox { get; }
        bool CollidesWith(ICollidable partner);
    }
}
