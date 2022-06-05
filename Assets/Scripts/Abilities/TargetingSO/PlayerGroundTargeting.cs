using Units.Interfaces;
using UnityEngine;

namespace Abilities.TargetingSO
{
    [CreateAssetMenu(fileName = "PlayerGroundTargeting", menuName = "ScriptableObjects/TargetingSo/PlayerGroundTargeting")]
    public class PlayerGroundTargeting : TargetingSo
    {
        public override void Refresh()
        {
            //Debug.Log("sds");
            if (!Physics.Raycast(Owner.TargetPosition, Vector3.down, out var hitDown, MaxRange))
            {
                //if we are looking at the ground we dont make the second ray to snap to the ground
                TemporaryTransform.position = Owner.TargetPosition;
                return;
            }

            Vector3 endPoint;
                
            if (Physics.Raycast(Owner.ShootingPosition.position, Owner.AimingDirection, out var aimDirectionHit,
                    MaxRange))
                endPoint = aimDirectionHit.point;
            else
                endPoint = Owner.ShootingPosition.position + Owner.AimingDirection.normalized * MaxRange;

            TemporaryTransform.position = new Vector3(endPoint.x, hitDown.point.y, endPoint.z);
       
        }
    }
}