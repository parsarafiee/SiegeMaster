using Abilities.AbilitySO;
using Abilities.TargetingStateSO;
using UnityEngine;

namespace Abilities.AbilityStates
{
    public class AbilityTargetingState : AbilityStates.AbilityState
    {
        private readonly AbilityTargetingStateSo _targetingStateSo;

        public AbilityTargetingState(AbilitySo abilitySo, AbilityTargetingStateSo targetingStateSo) : base(abilitySo)
        {
            _targetingStateSo = targetingStateSo;
        }

        public override void Refresh()
        {
            _targetingStateSo.Refresh();
        }

        public override void OnEnter()
        {
            AbilitySo.TargetTransform = null;
           _targetingStateSo.OnEnter();
        }

        public override void OnExit()
        {
            _targetingStateSo.OnExit();
        }

        protected override void OnFirePressAction()
        {
            _targetingStateSo.OnFirePress?.Invoke();
        }

        protected override void OnFireReleaseAction()
        {
            _targetingStateSo.OnFireRelease?.Invoke();
        }
    }
}