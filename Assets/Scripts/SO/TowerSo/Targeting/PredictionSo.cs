using UnityEngine;

namespace SO.TowerSo.Targeting
{
    public class PredictionSo : TargetingSo
    {
        private Rigidbody _rb;
        public override void Init(GameObject owner,float maxRange)
        {
            base.Init(owner, maxRange);
            _rb = owner.GetComponent<Rigidbody>();
        }
        public Vector3 GetBulletVelocityDirection(Transform target, Vector3 targetVelocity, float bulletSpeed, Transform barrel, Transform head)
        {
            //transform temp   

            var position = target.position;
            var a = Vector3.Angle(targetVelocity.normalized, (barrel.position - position).normalized);
            //Debug.Log(Vector3.Magnitude(targetVelocity));
            var b = Mathf.Asin(Mathf.Sin(a * Mathf.Deg2Rad) / (bulletSpeed / (Vector3.Magnitude(targetVelocity)))) * Mathf.Rad2Deg;

            head.forward = (position - head.position).normalized;
            var angle = head.eulerAngles;
            //Debug.Log(b);
            var velocityDirection = new Vector3(45, angle.y - b, angle.z);
            head.eulerAngles = velocityDirection;

            return head.forward * bulletSpeed;
        
        }

    }
}
