using MonoGame2DBrawler.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoGame2DBrawler.Characters.Items;
using MonoGame2DBrawler.Characters.Skills.Magics;
using MonoGame2DBrawler.Characters.Skills.PhysicalSkills;
using MonoGame2DBrawler.Characters.Skills.RevengeSkills;
using MonoGame2DBrawler.Sprites;
using MonoGame2DBrawler.Characters.Actions;

namespace MonoGame2DBrawler.Characters
{
    /// <summary>
    /// The Character class represents playable Characters in the Textadventure.
    /// A Character has certain Attributes that describe his role in Combat or other aspects of the game.
    /// A Character is able to perform 6 different types of actions in Combat.
    /// - Use a skill
    /// - Just attack
    /// - Defend
    /// - Use Magic
    /// - Use Item
    /// - End Turn
    /// </summary>
    public class Character : GameObject
    {
        #region MemberVariables
        private AnimatedSprite _animatedSprite;

        private string _name;

        /// <summary>
        /// _first is currentHp
        /// _second is maxHp
        /// </summary>
        public Pair<int> _hp;

        /// <summary>
        /// _first is currentMp
        /// _second is maxMp
        /// </summary>
        public Pair<int> _mp;

        private int _strength;
        private int _defence;
        private int _wit;
        private int _agility;
        private int _speed;
        private int _revengeValue = 0;

        private bool _isAlive = true;

        // Dictionaries are used because they grant O(1) lookup.
        // Lookup a skill or an item by providing the corresponding enum key.
        private Dictionary<EItem, Item> _items = new Dictionary<EItem, Item>();
        private Dictionary<EMagic, Magic> _magics = new Dictionary<EMagic, Magic>();
        private Dictionary<EPhysicalSkill, PhysicalSkill> _physicalSkills = new Dictionary<EPhysicalSkill, PhysicalSkill>();
        private Dictionary<ERevengeSkill, RevengeSkill> _revengeSkills = new Dictionary<ERevengeSkill, RevengeSkill>();
        #endregion

        #region Properties
        public AnimatedSprite AnimatedSprite
        {
            get { return _animatedSprite; }
            set { _animatedSprite = value; }
        }

        public int Strength { get => _strength; set => _strength = value; }
        public int Defence { get => _defence; set => _defence = value; }
        public int Wit { get => _wit; set => _wit = value; }
        public int Agility { get => _agility; set => _agility = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int RevengeValue { get => _revengeValue; set => _revengeValue = value; }
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
        #endregion

        public Character(String name = "NoName", int maxHp = 0, int maxMp = 0, int strength = 0, int defence = 0, int wit = 0, int agility = 0, int speed = 0)
        {
            _name = name;
            _hp = new Pair<int>(maxHp, maxHp);
            _mp = new Pair<int>(maxMp, maxMp);
            _strength = strength;
            _defence = defence;
            _wit = wit;
            _agility = agility;
            _speed = speed;

            SetUp_Items();
            SetUp_PhysicalSkills();
            SetUp_RevengeSkills();
            SetUp_Magics();
        }

        public void UsePhysicalSkill(Character target)
        {
            _physicalSkills[EPhysicalSkill.Cleave].ExecuteAction(target);
        }

        #region SetUpMethods
        private void SetUp_Items()
        {
            _items[EItem.HealthPotion] = Item.HealthPotion(EAction.Heal);
            _items[EItem.ManaPotion] = Item.ManaPotion(EAction.RefillMana);
        }

        private void SetUp_Magics()
        {
            _magics[EMagic.Fire] = Magic.Fire(EAction.DealDamage);
        }

        private void SetUp_PhysicalSkills()
        {
            _physicalSkills[EPhysicalSkill.Cleave] = PhysicalSkill.Cleave(EAction.DealDamage);
        }

        private void SetUp_RevengeSkills()
        {
            _revengeSkills[ERevengeSkill.Counter] = RevengeSkill.Counter(EAction.DealDamage);
            _revengeSkills[ERevengeSkill.Break] = RevengeSkill.Break(EAction.DealDamage);
        }
        #endregion
    }
}
