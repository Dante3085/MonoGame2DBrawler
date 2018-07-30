using MonoGame2DBrawler.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int _currentHp;
        private int _maxHp;

        private int _currentMp;
        private int _maxMp;

        private int _strength;
        private int _defence;
        private int _wit;
        private int _agility;
        private int _speed;
        private int _revengeValue = 0;

        private bool _isAlive = true;

        private EAction _currentAction = EAction.None;

        private Dictionary<EAction, AAction> _actions = new Dictionary<EAction, AAction>();
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
        public string Name { get => _name; set => _name = value; }
        public int CurrentHp { get => _currentHp; set => _currentHp = value; }
        public int MaxHp { get => _maxHp; set => _maxHp = value; }
        public int CurrentMp { get => _currentMp; set => _currentMp = value; }
        public int MaxMp { get => _maxMp; set => _maxMp = value; }
        #endregion

        public Character(String name = "NoName", int maxHp = 0, int maxMp = 0, int strength = 0, int defence = 0, int wit = 0, int agility = 0, int speed = 0)
        {
            _name = name;

            _currentHp = maxHp;
            _maxHp = maxHp;

            _currentMp = maxMp;
            _maxMp = maxMp;

            _strength = strength;
            _defence = defence;
            _wit = wit;
            _agility = agility;
            _speed = speed;

            SetUp_Items();
            SetUp_PhysicalSkills();
            SetUp_RevengeSkills();
            SetUp_Magics();

            _currentAction = EAction.Cleave;
        }

        public void UseCurrentAction(Character target)
        {
            _actions[_currentAction].ExecuteAction(target);
            Game1.gameConsole.Log(this.Name + " used " + _currentAction + " on " + target.Name);
        }

        #region SetUpMethods
        private void SetUp_Items()
        {
            _actions[EAction.HealthPotion] = AAction.HealthPotion();
            _actions[EAction.ManaPotion] = AAction.ManaPotion();
        }

        private void SetUp_Magics()
        {
            _actions[EAction.Fire] = AAction.Fire();
        }

        private void SetUp_PhysicalSkills()
        {
            _actions[EAction.Cleave] = AAction.Cleave();
        }

        private void SetUp_RevengeSkills()
        {
            _actions[EAction.Counter] = AAction.Counter();
            _actions[EAction.Break] = AAction.Break();
        }
        #endregion
    }
}
