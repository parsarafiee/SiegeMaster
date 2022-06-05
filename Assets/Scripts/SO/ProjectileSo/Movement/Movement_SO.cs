using UnityEngine;
using Units.Types;
using General;

public class Movement_SO : ScriptableObject
{
    [HideInInspector]protected GameObject gameobject;
    [HideInInspector]public IPoolable ipoolable;
    [HideInInspector]public Transform target;
    [HideInInspector]public Rigidbody rb;
    [HideInInspector]public Vector3 projectileInitialDIrection;
    [HideInInspector]public float initialSpeed;
    
    public virtual void FixedRefresh()
    {
    }


    public virtual void Init(GameObject _gameObject, Transform _target,float speed,Vector3 _projectileInitialDIrection)
    {
        gameobject = _gameObject;
        ipoolable = gameobject.GetComponent<IPoolable>();
        target = _target;
        rb = gameobject.GetComponent<Rigidbody>();
        initialSpeed = speed;
        projectileInitialDIrection = _projectileInitialDIrection;
    }

    public virtual void Refresh()
    {
    }
}
