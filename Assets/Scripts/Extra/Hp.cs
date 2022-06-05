using General;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hp : MonoBehaviour,IUpdatable, IPoolable, ICreatable<Hp.Args>
{
    public HPType type;

    public void PostInit()
    {
    }

    public void Init()
    {
    }

    public void FixedRefresh()
    {
    }

    public void LateRefresh()
    {
        
    }

    public void Refresh()
    {
       
    }

    public void Pool()
    {
        gameObject.SetActive(false);
    }
    
    public void Depool()
    {
        gameObject.SetActive(true);
    }

    public void Construct(Args constructionArgs)
    {
        transform.SetParent(constructionArgs.parent);
        transform.position = constructionArgs.parent.position;
        transform.localScale = Vector3.one;
        transform.localEulerAngles = Vector3.zero;
    }
    

    public class Args : ConstructionArgs
    {
        public Transform parent;
        public Args(Vector3 _spawningPosition,Transform _parent) : base(_spawningPosition)
        {
            parent = _parent;
        }
    }
}
