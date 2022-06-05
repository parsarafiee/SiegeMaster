using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using General;
using Units.Interfaces;

public class OnCollisionSO : ScriptableObject
{
    protected GameObject gameObject;
    protected float damage;
    protected bool ownerIsPlayer;
    
    public ParticleType onCollisionParticleType;
    public virtual void Init(GameObject gameobject, float _damage, bool _isPlayer)
    {
        gameObject = gameobject;
        damage = _damage;
        ownerIsPlayer = _isPlayer;
    }

    public virtual void OnEnterCollision(Vector3 position, ValueType type, IPoolable type2,GameObject collisionObject,bool isPlayer)
    {
    }

}
