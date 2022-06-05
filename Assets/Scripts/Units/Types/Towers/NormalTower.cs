using UnityEngine;
using UnityEngine.AI;

namespace Units.Types.Towers
{
    public class NormalTower : Tower
    {        // If you want want to use this predict bool you have to give the prediction projectile 
             // have to set a projectile that has PredictionMovement_SO, 
        #region Variables
        public float projectileSpeed;
        public bool predict;
        private Vector3 _projectileVelocity = Vector3.zero;

        #endregion
        #region Functions
        public override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            if (projectileType != ProjectileType.Proj_PredictionArrow)
            {
                Debug.Log("If you want to predict , you should use the prediction Arrow");
            }

#endif
        }

        public override void Fire(Transform targetTransform)
        {
            if (predict)
            {
                var targetNavemesh= target.GetComponent<NavMeshAgent>();
                //this is the math for predict the intercept with between two object wqith different speed 
                var targetMovementDirection = targetNavemesh.velocity;
                var targetMovementVelocity = targetNavemesh.speed;
                var angleTargetToPlayer = Vector3.Angle(targetMovementDirection.normalized,(head.position - target.position).normalized);
                var towerAngleFinalRotation =Mathf.Asin((Mathf.Sin(angleTargetToPlayer * Mathf.Deg2Rad) * targetMovementVelocity) /projectileSpeed) * Mathf.Rad2Deg;
                var dir = (targetTransform.position - head.position).normalized;
                var left = Vector3.Cross(dir, targetMovementDirection.normalized);
                head.LookAt(targetTransform.position, left);
                if (!float.IsNaN(towerAngleFinalRotation))
                {
                    head.RotateAround(head.transform.position, head.transform.up, towerAngleFinalRotation);
                }
                _projectileVelocity = head.forward * projectileSpeed;
                //calculate the prediction
            }

              ProjectileManager.Instance.Create(projectileType,new Projectile.Args(head.position, projectileType, targetTransform, projectileSpeed, projectileDamage,_projectileVelocity,true));
              base.Fire(targetTransform);
        }


        public override void ExtraBehaviorBeforeFire()
        {
            if (target)
            {
                head.forward = (target.position - head.position).normalized;
            }
        }
        #endregion
    }
}