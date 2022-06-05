using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
[CreateAssetMenu(fileName = "FireDamageSO", menuName = "ScriptableObjects/Damage/Fire")]

public class RefreshBehavior_Fire_SO : RefreshBehaviorSO
{

    public override void Refresh()
    {


        //////////////////// Having this logic is so expencive so we decided to remove this behavior////////////////////////////////////










        // MAKE A sphire cast to deal damage around the Bullet;

        //RaycastHit[] hits = Physics.SphereCastAll(gameObject.transform.position,5, gameObject.transform.forward,0, targetLeyerMask);
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    hits[i].collider.gameObject.GetComponent<IHittable>().GotShot(damage);
        //}



    }

}
