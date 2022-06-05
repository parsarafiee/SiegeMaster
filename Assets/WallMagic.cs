using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMagic : MonoBehaviour
{
    public Transform midleMap;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = midleMap.position;
    }
}
