using General;
using UnityEngine;

namespace Units.Types.Towers
{
    public class Tower : Unit,ICreatable<Tower.Args>
    {

        #region variables
        [HideInInspector]public Transform target;
        public ProjectileType projectileType;
        public ParticleType towerParticleType;
        public SO.TowerSo.Targeting.TargetingSo targetingSo;
        public float projectileDamage;
        public float towerAttackRange;
        public float attackSpeed;
        public Transform head;
        public Transform barrel;
        public Transform particlePosition;

        protected override Vector3 AimedPosition => target.position;

        // Must have fire animation and one trigger parameter , the name of trigger must be  "Fire"
        private static readonly int Fire1 = Animator.StringToHash("Fire");
        private float _timer = 0;

        #endregion
        #region Functions
        public override void Init()
        {
            //targetingSo = Instantiate(targetingSo);
            base.Init();
        }
        public override void PostInit()
        {
            targetingSo.Init(this.gameObject, towerAttackRange);
        }
        public override void Refresh()
        {
            CoolDown(attackSpeed);
            
        }
        public override void GotShot(float damage)
        {
            
        }

        protected virtual void GetTarget()
        {
            target = targetingSo.GetTheTarget();
        }

        private void CoolDown(float attackSpeed)
        {
            
            _timer += Time.deltaTime;
            if (_timer > attackSpeed)
            {
                GetTarget();
                ExtraBehaviorBeforeFire();
                if (target)
                {
                    Fire(target);
                }
                
                _timer = 0;

            }
         
        }
        public virtual void ExtraBehaviorBeforeFire()
        {

        }
        public  virtual void Fire(Transform targetTransform)
        {

            Animator.SetTrigger(Fire1);
         
            
            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(particlePosition.position));

        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            targetingSo.Init(this.gameObject, towerAttackRange);
        }
        #endregion
        #region Args
        public class Args : ConstructionArgs
        {

            public Args(Vector3 spawningPosition) : base(spawningPosition)
            {

            }

        }
        #endregion
    }
}
