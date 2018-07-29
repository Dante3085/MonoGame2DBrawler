using MonoGame2DBrawler.Characters.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Characters.Skills.PhysicalSkills
{
    public class PhysicalSkill : Skill
    {
        private PhysicalSkill(int hpModifier, int mpModifier, int revengeModifier, string description = null, EAction action = EAction.None) : base (hpModifier, mpModifier, revengeModifier, description, action)
        {
            SetUpActions();
        }

        #region PredefinedPhysicalSkills
        public static PhysicalSkill Cleave(EAction action)
        {
            return new PhysicalSkill(100, 0, 10, "Cleave", action: action);
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
