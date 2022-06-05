using UnityEngine;

namespace Abilities.AbilitySO
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/ProjectileAbility")]
    public class ProjectileAbility : AbilitySo
    {
        public ProjectileType type;
        public float projectileSpeed;
        public float projectileDamage;
        
        protected override void ReadyStateRefresh()
        {
            
        }

        protected override void OnCast()
        {
            ProjectileManager.Instance.Create(type,
                new Projectile.Args(ShootingPosition.position, type, TargetTransform, projectileSpeed, projectileDamage, Vector3.zero, true));
        }

        protected override void OnActiveCast()
        {
            
        }

        protected override void ActiveStateRefresh()
        {
           
        }
    }
}
