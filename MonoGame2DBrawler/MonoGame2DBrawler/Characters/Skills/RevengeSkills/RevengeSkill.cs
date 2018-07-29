using MonoGame2DBrawler.Characters.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Characters.Skills.RevengeSkills
{
    public class RevengeSkill : Skill
    {
        private RevengeSkill(int hpModifier, int mpModifier, string description = null, EAction action = EAction.None) : base(hpModifier, mpModifier, description:description, action:action)
        {
            SetUpActions();
        }

        #region PredefinedRevengeSkills
        public static RevengeSkill Counter(EAction action)
        {
            return new RevengeSkill(50, 0, "Counter");
        }

        public static RevengeSkill Break(EAction action)
        {
            return new RevengeSkill(20, 0);
        }
        #endregion

        protected override void SetUpActions()
        {
            base.SetUpActions();
        }

        #region Actions
        
        #endregion
    }
}
