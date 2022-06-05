using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;
using Units.Interfaces;
using Units.BossEnemy;
[CreateAssetMenu(fileName = "OnCollision", menuName = "ScriptableObjects/OnCollision/HitObject")]

public class OnCollition_CollideWithObject_SO : OnCollisionSO
{
    public override void OnEnterCollision(Vector3 position, ValueType type, IPoolable type2,GameObject collisionObject, bool OwnerIsPlayer)
    {


        ParticleSystemManager.Instance.Create(onCollisionParticleType, new ParticleSystemScript.Args(position));

        //deal damage to object that had Ihittable interface and deal damage
        var p = collisionObject.GetComponent(typeof(IHittable));
        if (p != null)
        {
            if (OwnerIsPlayer && p.gameObject.tag == "BossEnemy")
            {

                ((Cell)p).GotShot(damage);
            }
            else if (OwnerIsPlayer && p.gameObject.tag=="Target")
            {
             
                ((Units.Types.Enemy)p).GotShot(damage);
            }
            else if (!OwnerIsPlayer && p.gameObject.tag == "Player")
            {
                ((Units.Types.PlayerUnit)p).GotShot(damage);
            }

          
        }

    }
}
