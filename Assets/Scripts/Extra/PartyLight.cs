using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyLight : MonoBehaviour
{
    Light light;
    float timer;

    public float timeToChangeColler;
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToChangeColler)
        {
            light.color = Random.ColorHSV();
            timer = 0;
        }
    }
}
