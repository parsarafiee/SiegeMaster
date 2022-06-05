using General;
using UnityEngine;

public class SpellUI : MonoBehaviour,IUpdatable, ICreatable<SpellUI.Args>, IPoolable
{
    public class Args : ConstructionArgs
    {
        public Args(Vector3 spawningPosition) : base(spawningPosition)
        {
            
        }
    }

    public void Init()
    {
        
    }

    public void PostInit()
    {
        
    }

    public void Refresh()
    {
        
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
        gameObject.SetActive(false);
    }

    public void Depool()
    {
        gameObject.SetActive(true);
    }
}