using Managers;
using UnityEngine;

namespace Abilities.TargetingStateSO
{
    [CreateAssetMenu(fileName = "LocationTargeting",
        menuName = "ScriptableObjects/Ability State/Targeting/Location Targeting")]
    public class LocationTargetingState : AbilityTargetingStateSo
    {
        private SpellUI _spellUI;

        public override void Refresh()
        {
            
            TargetingSoClone.Refresh();
            if (_spellUI)
                _spellUI.transform.position = TargetingSoClone.TargetTransform.position;

        }

        public override void OnEnter()
        {
           // Debug.Log("OnEnter: " + AbilitySo.TargetPosition);
            TargetingSoClone.TargetTransform.position = AbilitySo.TargetPosition;

            if (spellUIType != SpellUIType.None)
                _spellUI = SpellUIManager.Instance.Create(spellUIType, new SpellUI.Args(AbilitySo.TargetPosition));
        }

        public override void OnExit()
        {
            if (spellUIType != SpellUIType.None)
                SpellUIManager.Instance.Pool(spellUIType, _spellUI);
        }

        protected override void OnFirePressEvent()
        {
            AbilitySo.TargetTransform = TargetingSoClone.TargetTransform;
        }

        protected override void OnFireReleaseEvent()
        {
        }
    }
}