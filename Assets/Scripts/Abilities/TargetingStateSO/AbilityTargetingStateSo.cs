using System;
using Abilities.AbilitySO;
using Abilities.TargetingSO;
using Managers;
using UnityEngine;


namespace Abilities.TargetingStateSO
{
    public abstract class AbilityTargetingStateSo : ScriptableObject
    {
        [SerializeField]private TargetingSo targetingSo;
        protected TargetingSo TargetingSoClone;
        protected AbilitySo AbilitySo;
        [HideInInspector] public SpellUIType spellUIType;

        public Action OnFirePress;
        public Action OnFireRelease;

        public virtual void Init(AbilitySo abilitySo)
        {
            AbilitySo = abilitySo;
            OnFirePress = OnFirePressEvent;
            OnFireRelease = OnFireReleaseEvent;

            TargetingSoClone = Instantiate(targetingSo);
            TargetingSoClone.Init(abilitySo, abilitySo.stats.maxRange);
        }

        public abstract void Refresh();

        public abstract void OnEnter();

        public abstract void OnExit();

        protected abstract void OnFirePressEvent();
        protected abstract void OnFireReleaseEvent();
    }
}