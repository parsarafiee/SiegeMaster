using Abilities.AbilitySO;
using UnityEngine;

namespace Abilities.AbilityStates
{
    public class AbilityCooldownState : AbilityState
    {
        public float CooldownTimeLeft { get; private set; }

        public bool CooldownIsOver => CooldownTimeLeft <= 0;

        public AbilityCooldownState(AbilitySo ability) : base(ability)
        {
        }

        public override void Refresh()
        {
            CooldownTimeLeft -= Time.deltaTime;
        }

        public override void OnEnter()
        {
            CooldownTimeLeft = AbilitySo.stats.baseCooldown;
        }

        public override void OnExit()
        {
        }

        protected override void OnFirePressAction()
        {
        }

        protected override void OnFireReleaseAction()
        {
        }
    }
}