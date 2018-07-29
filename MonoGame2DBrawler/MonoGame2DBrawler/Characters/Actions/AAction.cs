using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Characters.Actions
{
    /// <summary>
    /// Describes something that can have an effect on a Character or something else.
    /// A AAction can be dealing damage, healing damage, increasing MaxHp (very broad term).
    /// Or something more complicated.
    /// </summary>
    public abstract class AAction
    {
        /// <summary>
        /// EAction that this Action was assigned.
        /// </summary>
        private EAction _action;
        protected Dictionary<EAction, Action<Character>> _actions = new Dictionary<EAction, Action<Character>>();

        public AAction(EAction action)
        {
            _action = action;
            SetUpActions();
        }

        public void ExecuteAction(Character target)
        {
            _actions[_action].Invoke(target);
        }

        /// <summary>
        /// Puts Methods from region "Actions" into Dictionary with corresponding EAction Key.
        /// </summary>
        protected virtual void SetUpActions()
        {
            _actions[EAction.DoubleHealth] = DoubleHealth;
            _actions[EAction.HalfHealth] = HalfHealth;
        }

        #region Actions
        private void HalfHealth(Character target)
        {
            target._hp._second = (int)(target._hp._second * 0.5);
        }

        private void DoubleHealth(Character target)
        {
            target._hp._second = target._hp._second * 2;
        }
        #endregion
    }
}
