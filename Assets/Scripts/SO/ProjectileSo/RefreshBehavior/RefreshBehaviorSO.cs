using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshBehaviorSO : ScriptableObject
{
    public LayerMask targetLeyerMask;
    protected GameObject gameObject;
    protected Rigidbody rb;
    protected float damage;
   
    public void FixedRefresh()
    {
    }

    public virtual void Init(GameObject gameobject, float _damage)
    {
        this.gameObject = gameobject;
        rb = gameobject.GetComponent<Rigidbody>();
        damage = _damage;
    }

    public virtual void Refresh()
    {

    }

}
