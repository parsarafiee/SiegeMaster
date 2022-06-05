using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public enum ParticleType {Blood ,Smoke, FireBloodTower, MagicParticleSystem ,ElectricSpark }
public class ParticleSystemManager : Manager<ParticleSystemScript, ParticleType, ParticleSystemScript.Args, ParticleSystemManager>
{
    protected override string PrefabLocation => "Prefabs/ParticleSystems/";
}
