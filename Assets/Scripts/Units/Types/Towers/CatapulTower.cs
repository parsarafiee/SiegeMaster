using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;
using Units.Types.Towers;

namespace Units.Types
{
    public class CatapulTower : Tower
    {

        private const float towerXrotation = 45; // fixed value for the rotation to shoot bullet

        public override void Fire(Transform targetTransform)
        {
            //https://byjus.com/trajectory-formula/
            Vector3 catapultPosition = new Vector3(transform.position.x, targetTransform.transform.position.y, transform.position.z);
            Vector3 pointToObject = (targetTransform.position - transform.position).normalized;
            head.forward = pointToObject;
            Vector3 vector32 = head.eulerAngles;
            head.eulerAngles = new Vector3(towerXrotation, vector32.y, vector32.z);
            float distanceTotarget = Vector3.Distance(catapultPosition, targetTransform.position);
            Vector3 finalvelocity = distanceTotarget * Mathf.Sqrt(-Physics.gravity.y / (barrel.position.y - targetTransform.transform.position.y + distanceTotarget)) * barrel.transform.forward;

            ProjectileManager.Instance.Create(projectileType, new Projectile.Args(barrel.position, projectileType, targetTransform, 0, 0, finalvelocity,true));

            base.Fire(targetTransform);
        }

    }
}