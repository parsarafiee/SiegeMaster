using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Units.Interfaces;
using Units.Types;

public class Hips : MonoBehaviour, IHittable
{
    //dont look at this please :|
    private PlayerUnit player;
    public void GotShot(float damage)
    {
        player.GotShot(damage);
    }
    private void Awake()
    {
        player = GetComponentInParent<PlayerUnit>();
    }
}
