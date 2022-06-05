using System;
using Abilities.AbilitySO;
using Units.Types;
using UnityEngine;

namespace UI
{
    public class UIPlayerBar : UIElement
    {
        #region Singleton

        private static UIPlayerBar _instance;

        public static UIPlayerBar Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<UIPlayerBar>();
                return _instance;
            }
        }

        #endregion

        #region Properties and Variables

        private PlayerUnit _playerUnit;

        #region Ability Icon

        private const int MaxAbility = 4;

        public AbilityIcon ability1;
        public AbilityIcon ability2;
        public AbilityIcon ability3;
        public AbilityIcon ability4;

        private readonly AbilitySo[] _current = new AbilitySo[MaxAbility];
        private readonly AbilityIcon[] _abilityIcons = new AbilityIcon[MaxAbility];

        public Action<AbilitySo[]> SetAbility;

        #endregion

        #region Fillable bar

        [SerializeField] private FillableBar healthBar;
        [SerializeField] private FillableBar manaBar;

        #endregion

        #endregion

        public override void Init()
        {
            _instance = this;
            _playerUnit = FindObjectOfType<PlayerUnit>();

            #region Ability Icon

            _abilityIcons[0] = ability1;
            _abilityIcons[1] = ability2;
            _abilityIcons[2] = ability3;
            _abilityIcons[3] = ability4;

            SetAbility = SetAbilityEvent;

            #endregion
        }

        public override void Refresh()
        {
            base.Refresh();
            
            healthBar.Refresh(_playerUnit.stats.health.Current / _playerUnit.stats.health.Maximum,
                _playerUnit.stats.health.Current + " / " + _playerUnit.stats.health.Maximum);
            
            manaBar.Refresh(_playerUnit.stats.mana.Current / _playerUnit.stats.mana.Maximum,
                _playerUnit.stats.mana.Current + " / " + _playerUnit.stats.mana.Maximum);

            for (var i = 0; i < _current.Length; i++)
            {
                _abilityIcons[i].Refresh(_current[i]);
            }
        }

        private void SetAbilityEvent(AbilitySo[] abilities)
        {
            for (var i = 0; i < abilities.Length; i++)
            {
                _current[i] = abilities[i];
                _abilityIcons[i].SetArt(_current[i]);
            }
        }
    }
}