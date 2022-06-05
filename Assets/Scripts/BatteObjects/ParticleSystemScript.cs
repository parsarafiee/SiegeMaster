using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
public class ParticleSystemScript : MonoBehaviour,IUpdatable,ICreatable<ParticleSystemScript.Args>,IPoolable
{
    public  ParticleType type;
    private ParticleSystem ps;
    float timer;
    float timeToPool;
    public void Init()
    {
        ps = GetComponent<ParticleSystem>();
#pragma warning disable CS0618
        timeToPool = ps.duration;
#pragma warning restore CS0618
    }


    public void PostInit()
    {
        
       
    }

    public void Refresh()
    {
        timer += Time.deltaTime;
        if (timer> timeToPool)
        {
            ParticleSystemManager.Instance.Pool(type, this);
        }
    }

    public void FixedRefresh()
    {
    }

    public void LateRefresh()
    {
        
    }

    public void Construct(Args constructionArgs)
    {
        transform.position = constructionArgs.spawningPosition;
    }

    public void Pool()
    {
        this.gameObject.SetActive(false);
    }

    public void Depool()
    {
        this.gameObject.SetActive(true);
        timer = 0;  
    }
    public class Args : ConstructionArgs
    {
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {
        }
    }
}
