using General;
using Units.Interfaces;
using UnityEngine;

namespace BatteObjects
{
    public class Spell : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Spell.Args>
    {
        public void PostInit()
        {
        }

        public void Init()
        {
        }

        public void FixedRefresh()
        {
        }

        public void LateRefresh()
        {
        }

        public void Refresh()
        {
        }

        public void Pool()
        {
            gameObject.SetActive(false);
        }


        public void Depool()
        {
            gameObject.SetActive(true);
        }
        void ExplosionDamage(Transform center, float radius, float damage)
        {
            RaycastHit[] ray = Physics.SphereCastAll(center.position, radius, center.forward);

            foreach (var hitCollider in ray)
            {
                if ((hitCollider.collider.gameObject.tag == "Target" || hitCollider.collider.gameObject.tag == "BossEnemy") && hitCollider.collider.gameObject.TryGetComponent(out IHittable hittable))
                {

                    hittable.GotShot(damage);
                }

            }
        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            ExplosionDamage(transform, constructionArgs.radius, constructionArgs.explosionDamage);
        }


        public class Args : ConstructionArgs
        {
            public float radius;
            public float explosionDamage;
            public Args(Vector3 _spawningPosition, float _radius, float _explosionDamage) : base(_spawningPosition)
            {
                radius = _radius;
                explosionDamage = _explosionDamage;
            }
        }
    }
}