using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.General
{
    /// <summary>
    /// Base class for every kind of object in the game.
    /// </summary>
    public class GameObject
    {
        #region MemberVariables
        // Tracks number of existing GameObject instances.
        private static int _numGameObjects;
        #endregion

        #region Properties
        public static int NumGameObjects { get { return _numGameObjects; } }
        #endregion

        /// <summary>
        /// Constructor. Increments _numGameObjects.
        /// </summary>
        public GameObject()
        {
            _numGameObjects++;
        }

        /// <summary>
        /// Destructor / Finalizer. Decrements _numGameObjects.
        /// </summary>
        ~GameObject()
        {
            _numGameObjects--;
        }
    }
}
