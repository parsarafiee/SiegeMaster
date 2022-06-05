using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Proj_Arrow, Proj_Seeking, Proj_FireBall, Proj_Catapult , Proj_PredictionArrow ,EnemyArrow }
public class ProjectileManager : General.Manager<Projectile, ProjectileType, Projectile.Args, ProjectileManager>
{
    protected override string PrefabLocation => "Prefabs/Projectiles/";


}
