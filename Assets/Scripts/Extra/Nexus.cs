using General;
using UnityEngine;
using Units.Interfaces;
using UnityEngine.SceneManagement;

public class Nexus : MonoBehaviour, IUpdatable, IPoolable, IHittable, ICreatable<Nexus.Args>
{
    public int FullHp { get; private set; }
    public int currentHp;

    public void Init()
    {
        FullHp = currentHp;
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

    public void Pool()
    {

    }

    public void Depool()
    {

    }

    public void Construct(Args constructionArgs)
    {
    }

    public class Args : ConstructionArgs
    {
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {

        }
    }
    public void GotShot(float damage)
    {
        currentHp -= (int)damage;
        if (currentHp<0)
        {
            SceneManager.LoadScene("StartMenuPC");
        }
    }
}
