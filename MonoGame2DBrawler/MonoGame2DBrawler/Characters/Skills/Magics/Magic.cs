using MonoGame2DBrawler.Characters.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Characters.Skills.Magics
{
    public class Magic : Skill
    {
        // public static Magic Fire = new Magic(300, 10, 1, "Fire");
        // public static Magic Water = new Magic(500, 3, 2);

        private Magic(int hpModifier, int mpModifier, int revengeModifier, string description = null, EAction action = EAction.None) : base(hpModifier, mpModifier, revengeModifier, description, action)
        {
            SetUpActions();
        }

        #region PredefinedMagics
        public static Magic Fire(EAction action)
        {
            return new Magic(300, 10, 1, "Fire", action: action);
        }

        public static Magic Water(EAction action)
        {
            return new Magic(500, 3, 2, action: action);
        }
        #endregion

        protected override void SetUpActions()
        {
            base.SetUpActions();
            _actions[EAction.Death] = Death;
            _actions[EAction.Heal] = Heal;
        }

        #region Actions
        /// <summary>
        /// Kills a Character by putting his current hp to int.MinValue.
        /// </summary>
        /// <param name="target"></param>
        public void Death(Character target)
        {
            target._hp._first = int.MinValue;
        }

        /// <summary>
        /// Heals target Character by _hpModifier's amount.
        /// If target Character gets healed above max hp, current hp is set to max hp.
        /// </summary>
        /// <param name="target"></param>
        public void Heal(Character target)
        {
            target._hp._first += _hpModifier;

            if (target._hp._first > target._hp._second)
                target._hp._first = target._hp._second;
        }
        #endregion
    }
}
