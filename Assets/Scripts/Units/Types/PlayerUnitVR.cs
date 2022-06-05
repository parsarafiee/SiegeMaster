using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public class PlayerUnitVR : PlayerUnit
    {
        Vector3 hitpoint;
        public LayerMask layerMask;
        public Transform rightController;
        public float maxDistanceAiming;
        protected override Vector3 AimedPosition
        {
            get
            {
                return hitpoint;
              
            }

        }
        public override void Init()
        {
            base.Init();
        }
        public override void Refresh()
        {
            base.Refresh();
            hitpoint = RayCastVR(rightController, maxDistanceAiming);

        }
        public override void Look()
        {
           
        }
        public Vector3 RayCastVR(Transform rightController,float maxDistanceForRay)
        {

            RaycastHit hit;
            Vector3 hitPoint;
            if (Physics.Raycast(rightController.position , rightController.forward, out hit, maxDistanceForRay, layerMask))

            {
                hitPoint = hit.point;

            }
            else
            {
                // if player look at sky we send the maxDistancePosition 
                hitPoint = (rightController.forward * maxDistanceForRay) + rightController.position;


            }
            return hitPoint;
        }

    }
}
