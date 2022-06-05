
using BatteObjects;
using Managers;
using UnityEngine;

namespace Abilities.AbilitySO
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/SpellAbility")]
    public class SpellAbility : AbilitySo
    {
        public SpellType type;
        public float sphereCastRadius;
        public float explosionDamage;

        protected override void ReadyStateRefresh()
        {
        }

        protected override void OnCast()
        {
            SpellManager.Instance.Create(type,new Spell.Args(TargetTransform.position, sphereCastRadius, explosionDamage));
        }

        protected override void OnActiveCast()
        {

        }

        protected override void ActiveStateRefresh()
        {

        }
    }
}