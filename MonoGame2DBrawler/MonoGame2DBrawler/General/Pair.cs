using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.General
{
    /// <summary>
    /// Holds 2 Objects of the same type in a structure.
    /// Motivation: CurrentHP and MaxHP are the two pieces of information that
    /// describe the HP state of a Character. It makes sense to keep them close.
    /// It is also easy to think about other circumstances where 2 pieces of 
    /// information are always relevant for each other.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Pair<T>
    {
        public T _first;
        public T _second;

        public Pair(T first, T second)
        {
            _first = first;
            _second = second;
        }
    }
}
