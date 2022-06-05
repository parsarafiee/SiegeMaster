using System;
using Abilities.AbilityStates;
using Abilities.TargetingStateSO;
using General;
using Managers;
using Units.Interfaces;
using Units.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities.AbilitySO
{
    public enum CastMethod
    {
        InstantCast,
        Indicator,
        PressAndRelease,
    }

    public abstract class AbilitySo : ScriptableObject, ITargetAcquirer
    {
        #region Properties and Variables

        [Header("Stats")] public AbilityStats stats;
        public float CooldownTimeLeft => _stateMachine.CooldownTimeLeft;

        #region Cast Method (NOT IMPLEMENTED)

/*
        [Header("Cast Method")] public CastMethod castMethod;

        [InspectorName("Lock Cast Method")]
        [Tooltip("Locking the cast method will force the ability to " +
                 "be cast a certain way, overriding player cast preference.")]
        [SerializeField]
        protected bool castMethodIsLock;

        public bool IsPressAndRelease => castMethod == CastMethod.PressAndRelease;
*/

        #endregion

        public Unit Owner { get; private set; }
        public SpellUIType spellUIType;

        #region State Specifics and StateMachine

/*
        [Header("State Machine Parameter")]
        [SerializeField]
        [Tooltip("If ability is a toggle or has a recast mechanic, put this true")]
*/
        //State Machine
        private AbilityStateMachine _stateMachine;

        //Ability State (Also written simplified bool to be use in ability handler)
        public bool IsReadyToCast => _stateMachine.IsReady;
        public bool IsChanneling => _stateMachine.IsChanneling;
        public bool IsTargeting => _stateMachine.IsTargeting;
        public bool IsActive => _stateMachine.IsActive;
        public bool IsOnCooldown => _stateMachine.IsOnCooldown;

        #endregion

/*
        [Header("Ready State")] public Action OnReadyEnter;


        //[Header("Targeting State")]
        [Header("Channeling State")] public Action OnChannelingEnter;
        */
        [Header("Active State")] public float activeTime;
        public int recastCharges;

        //Public events related to "Input" Handling
        private bool IsSelected => Owner.AbilityHandler.SelectedAbility == this;

        public Action OnFirePress;
        public Action OnFireRelease;

        #region Targeting Stuff

        //Targeting
        public AbilityTargetingStateSo targetingStateSo;
        private AbilityTargetingStateSo _targetingStateSoClone;
        public Transform ShootingPosition => Owner.ShootingPosition;
        public Vector3 AimingDirection => Owner.AimingDirection;
        public Vector3 TargetPosition => Owner.TargetPosition;
        public Transform TargetTransform { get; set; }

        #endregion

        #endregion

        #region Public Methods

        public virtual void Init(Unit owner)
        {
            //Init Properties and Variables
            Owner = owner;

            _targetingStateSoClone = Instantiate(targetingStateSo);
            _targetingStateSoClone.Init(this);
            _targetingStateSoClone.spellUIType = spellUIType;

            _stateMachine =
                new AbilityStateMachine(this, OnCast, OnActiveCast, ActiveStateRefresh, _targetingStateSoClone);

            //define Events
            OnFirePress = () =>
            {
                if (IsSelected && _stateMachine.CurrentState as AbilityState is var state && state != null)
                {
                    state.OnFirePress?.Invoke();
                }
            };

            OnFireRelease = () =>
            {
                if (IsSelected && _stateMachine.CurrentState as AbilityState is var state && state != null)
                {
                    state.OnFireRelease?.Invoke();
                }
            };
        }

        public void Refresh()
        {
            _stateMachine.Refresh();
        }

        private bool OwnerHasResources()
        {
            return Owner.Gold >= stats.goldCost && Owner.stats.mana.Current >= stats.manaCost;
        }

        #endregion

        #region Abstract Methods

        protected abstract void ReadyStateRefresh();
        protected abstract void OnCast();
        protected abstract void OnActiveCast();
        protected abstract void ActiveStateRefresh();

        #endregion

        private class AbilityStateMachine : StateMachine
        {
            #region Properties and Variables
            
            //Values needed elsewhere
            public float CooldownTimeLeft => _cooldownState.CooldownTimeLeft;

            //States
            private readonly AbilityReadyState _readyState;
            private readonly AbilityTargetingState _targetingState;
            private readonly AbilityChannelingState _channelingState;
            private readonly AbilityCooldownState _cooldownState;
            private readonly AbilityActiveState _activeState;

            //Simple bool for State Check
            public bool IsReady => CurrentState == _readyState;
            public bool IsTargeting => CurrentState == _targetingState;
            public bool IsChanneling => CurrentState == _channelingState;
            public bool IsOnCooldown => CurrentState == _cooldownState;
            public bool IsActive => CurrentState == _activeState;

            #endregion

            public AbilityStateMachine(AbilitySo abilitySo, Action onCast, Action onActiveCast,
                Action activeStateRefresh, AbilityTargetingStateSo targetingStateSo)
            {
                #region State Creation (Bunch of "state = new state()")

                _readyState = new AbilityReadyState(abilitySo);
                _targetingState = new AbilityTargetingState(abilitySo, targetingStateSo);
                _channelingState = new AbilityChannelingState(abilitySo);
                _cooldownState = new AbilityCooldownState(abilitySo);
                _activeState = new AbilityActiveState(abilitySo, onCast, onActiveCast, activeStateRefresh);

                #endregion

                #region Transitions (from, to, Condition)

                //Ready 
                AddTransition(_readyState, _targetingState, WasTrigger());
                //Targeting
                AddTransition(_targetingState, _channelingState, HasTargetAndResources());
                AddTransition(_targetingState, _readyState, AbilityIsNotSelected());
                //Channeling
                AddTransition(_channelingState, _activeState, ChannelComplete());
                AddTransition(_channelingState, _cooldownState, ChannelWasInterrupted());
                //Active
                AddTransition(_activeState, _cooldownState, ActiveStateIsOver());
                //Cooldown
                AddTransition(_cooldownState, _readyState, CooldownIsOver());

                #endregion

                #region Conditions

                Func<bool> WasTrigger() => () => abilitySo.IsSelected;

                Func<bool> HasTargetAndResources() =>
                    () => abilitySo.TargetTransform != null && abilitySo.OwnerHasResources();

                Func<bool> AbilityIsNotSelected() => () => !abilitySo.IsSelected;

                Func<bool> ChannelComplete() => () => _channelingState.HasCompleted;

                Func<bool> ChannelWasInterrupted() => () => _channelingState.HasBeenInterrupt;

                Func<bool> CooldownIsOver() => () => _cooldownState.CooldownIsOver;

                Func<bool> ActiveStateIsOver() => () =>
                    _activeState.HasNoRecastChargesLeft ||
                    _activeState.HasActiveTimer && _activeState.ActiveTimeRemainingIsOver;

                #endregion

                SetState(_readyState);
            }
        }

        [Serializable]
        public class AbilityStats
        {
            public int manaCost;
            public int goldCost;
            public int maxRange;

            public float baseChannelTime;
            public float baseCooldown;

            //UI
            public Sprite art;
        }

        //Not used yet, but could store specific animation and audio to play for different part of the ability
        [Serializable]
        public class Event : UnityEvent
        {
            public AnimationClip animationClipToPlay;
            //TODO : Wwise Event Field 
            //TODO : Particle System 

            private Animator _animator;

            /*public void Init(AbilitySo abilitySo)
            {
                _animator = abilitySo.Owner.GetComponent<Animator>();
                if(animationClipToPlay) AddListener(()=> _animator.;
            }*/
        }
    }
}