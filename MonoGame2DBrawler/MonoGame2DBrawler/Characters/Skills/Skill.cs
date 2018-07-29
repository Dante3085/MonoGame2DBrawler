using MonoGame2DBrawler.Characters.Actions;
using MonoGame2DBrawler.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Characters.Skills
{
    /// <summary>
    /// Describes a Skill that a Character can use.
    /// </summary>
    public class Skill : AAction
    {
        protected int _hpModifier;
        protected int _mpModifier;
        protected int _revengeModifier;
        protected string _description;

        protected Skill(int hpModifier = 0, int mpModifer = 0, int revengeModifier = 0, string description = "NoDescription", EAction action = EAction.None) : base(action)
        {
            _hpModifier = hpModifier;
            _mpModifier = mpModifer;
            _revengeModifier = revengeModifier;
            _description = description;

            SetUpActions();
        }

        protected override void SetUpActions()
        {
            base.SetUpActions();
            _actions[EAction.DealDamage] = DealDamage;
        }

        #region Actions
        private void DealDamage(Character target)
        {
            target._hp._first -= (_hpModifier - target.Defence);
            if (target._hp._first <= 0)
            {
                target._hp._first = 0;
                target.IsAlive = false;
            }
        }
        #endregion
    }
}
