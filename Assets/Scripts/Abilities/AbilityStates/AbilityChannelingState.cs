using Abilities.AbilitySO;
using UnityEngine;

namespace Abilities.AbilityStates
{
    public class AbilityChannelingState : AbilityStates.AbilityState
    {
        private float _channelTime;
        public bool HasCompleted => _channelTime >= AbilitySo.stats.baseChannelTime;
        public bool HasBeenInterrupt { get; set; }

        public AbilityChannelingState(AbilitySo abilitySo) : base(abilitySo)
        {
        }
        public override void Refresh()
        {
            _channelTime += Time.deltaTime;
        }

        public override void OnEnter()
        {
            _channelTime = 0;
            HasBeenInterrupt = false;
            AbilitySo.Owner.RemoveGold(AbilitySo.stats.goldCost);
            AbilitySo.Owner.stats.mana.Current -= AbilitySo.stats.manaCost;
        }

        public override void OnExit()
        {
            //TODO : Do something on cast fail
        }

        protected override void OnFirePressAction()
        {
        }

        protected override void OnFireReleaseAction()
        {
        }
    }
}