using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Characters.Actions
{
    public enum EAction
    {
        None,

        #region Items
        HealthPotion,
        ManaPotion,
        #endregion

        #region Magics
        Fire,
        Water,
        #endregion

        #region PhysicalAttacks
        Cleave,
        #endregion

        #region RevengeSkills
        Counter,
        Break,
        #endregion
    }
}
