using System;
using Abilities.AbilitySO;
using General;
using UI;
using Units.Types;
using UnityEngine;

namespace Abilities
{
    public class AbilityHandler : MonoBehaviour, IUpdatable
    {
        #region Property and Variables

        private Unit _owner;
        private const int NumberOfAbility = 4;

        [Header("Attack and Abilities")] [SerializeField]
        private AbilitySo basicAttack;

        private AbilitySo _basicAttackClone;

        [SerializeField] private AbilitySo[] abilities = new AbilitySo[NumberOfAbility];
        private readonly AbilitySo[] _abilitiesClone = new AbilitySo[NumberOfAbility];

        [SerializeField] private AbilitySo[] towers = new AbilitySo[NumberOfAbility];
        private readonly AbilitySo[] _towersClone = new AbilitySo[NumberOfAbility];

        private bool _inBuildingMode;

        public AbilitySo SelectedAbility { get; private set; }

        //Events
        public Action<int> OnAbilityPress;
        public Action<int> OnAbilityRelease;
        public Action OnFirePress;
        public Action OnFireRelease;
        public Action ToggleBuildMode;

        #endregion

        #region Public Method

        #region Init

        public void Init()
        {
            //Cache Components
            _owner = GetComponent<Unit>();

            //Init Events
            OnAbilityPress = OnAbilityPressEvent;
            OnAbilityRelease = OnAbilityReleaseEvent;
            OnFirePress = OnFirePressEvent;
            OnFireRelease = OnFireReleaseEvent;
            ToggleBuildMode = ToggleBuildingMode;

            InitAbilities();
            InitBasicAttack();
            InitTowers();
        }

        public void PostInit()
        {
            if (_owner.IsPlayer)
                UIPlayerBar.Instance.SetAbility(_abilitiesClone);
        }

        #endregion

        #region Refresh

        public void Refresh()
        {
            CheckBasicAttackAutoSelection();

            RefreshBasicAttack();

            RefreshAbilities();

            RefreshTowersAbilities();
        }

        #region Not Use

        public void FixedRefresh()
        {
        }

        public void LateRefresh()
        {
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region Init

        private void InitAbilities()
        {
            for (var i = 0; i < NumberOfAbility; i++)
            {
                if (!abilities[i]) continue;
                _abilitiesClone[i] = Instantiate(abilities[i]);
                _abilitiesClone[i].Init(_owner);
            }
        }

        private void InitBasicAttack()
        {
            if (!basicAttack) return;
            _basicAttackClone = Instantiate(basicAttack);
            _basicAttackClone.Init(_owner);
            SelectedAbility = _basicAttackClone;
        }

        private void InitTowers()
        {
            for (var i = 0; i < NumberOfAbility; i++)
            {
                if (!towers[i]) continue;
                _towersClone[i] = Instantiate(towers[i]);
                _towersClone[i].Init(_owner);
            }
        }

        #endregion

        #region Refresh

        private void RefreshTowersAbilities()
        {
            foreach (var towerAbility in _towersClone)
            {
                if (towerAbility)
                    towerAbility.Refresh();
            }
        }

        private void RefreshAbilities()
        {
            foreach (var ability in _abilitiesClone)
                if (ability)
                    ability.Refresh();
        }

        private void RefreshBasicAttack()
        {
            if (basicAttack)
                _basicAttackClone.Refresh();
        }

        private void CheckBasicAttackAutoSelection()
        {
            //Put basic Attack as Selected if selected spell isn't basic attack and is on cooldown
            if (SelectedAbility && SelectedAbility.IsOnCooldown && SelectedAbility != _basicAttackClone &&
                basicAttack != null)
                SelectedAbility = _basicAttackClone;
        }

        #endregion

        #region Events

        private void ToggleBuildingMode()
        {
            _inBuildingMode = !_inBuildingMode;
            UIPlayerBar.Instance.SetAbility(_inBuildingMode ? _towersClone : _abilitiesClone);
        }

        private void OnFirePressEvent()
        {
            if (SelectedAbility)
                SelectedAbility.OnFirePress?.Invoke();
        }

        private void OnFireReleaseEvent()
        {
            if (SelectedAbility)
                SelectedAbility.OnFireRelease?.Invoke();
        }

        private void OnAbilityReleaseEvent(int i)
        {
        }

        private void OnAbilityPressEvent(int i)
        {
            if (_abilitiesClone[i] is null || SelectedAbility && SelectedAbility.IsChanneling) return;

            //Cancel Spell
            if (_abilitiesClone[i] == SelectedAbility || _towersClone[i] == SelectedAbility)
            {
                SelectedAbility = _basicAttackClone;
                return;
            }

            if (!_inBuildingMode)
            {
                if (!_abilitiesClone[i].IsReadyToCast) return;
                SelectedAbility = _abilitiesClone[i];
                SelectedAbility.OnFirePress?.Invoke();
            }
            else
            {
                if (!_towersClone[i].IsReadyToCast) return;
                SelectedAbility = _towersClone[i];
                SelectedAbility.OnFirePress?.Invoke();
            }
        }

        #endregion

        #endregion
    }
}