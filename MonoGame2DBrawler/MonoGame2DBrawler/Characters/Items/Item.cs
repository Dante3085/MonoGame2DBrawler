using MonoGame2DBrawler.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoGame2DBrawler.Characters.Actions;
using Microsoft.Xna.Framework;

namespace MonoGame2DBrawler.Characters.Items
{
    public class Item : AAction
    {
        private int _hpModifier;
        private int _mpModifier;

        private Item(int hpModifier = 0, int mpModifier = 0, EAction action = EAction.None ) : base(action)
        {
            _hpModifier = hpModifier;
            _mpModifier = mpModifier;

            SetUpActions();
        }

        #region PredefinedItems
        /// <summary>
        /// Returns an Item with hpModifier = 100 (i.e. A HealthPotion that heals 100 healt/damage)
        /// </summary>
        /// <returns></returns>
        public static Item HealthPotion(EAction action)
        {
            return new Item(hpModifier: 100, action: action);
        }

        /// <summary>
        /// Returns a new Item with mpModifier = 100 (i.e. A ManaPotion that refills current mp by 100)
        /// </summary>
        /// <returns></returns>
        public static Item ManaPotion(EAction action)
        {
            return new Item(mpModifier: 100, action: action);
        }
        #endregion

        protected override void SetUpActions()
        {
            base.SetUpActions();
            _actions[EAction.Heal] = Heal;
            _actions[EAction.RefillMana] = RefillMana;
        }

        #region Actions
        /// <summary>
        /// Heals target Character by _hpModifier's amount.
        /// If target Character gets healed above max hp, current hp will be set to max hp.
        /// </summary>
        /// <param name="target"></param>
        private void Heal(Character target)
        {
            target._hp._first += _hpModifier;

            if (target._hp._first > target._hp._second)
                target._hp._first = target._hp._second;
        }

        /// <summary>
        /// Refills target Character's current mp by _mpModifier's amount.
        /// If target Character's current mp gets increased above max mp, current mp will be set to max mp.
        /// </summary>
        /// <param name="target"></param>
        private void RefillMana(Character target)
        {
            target._mp._first += _mpModifier;

            if (target._mp._first > target._mp._second)
                target._mp._first = target._mp._second;
        }
        #endregion
    }
}
