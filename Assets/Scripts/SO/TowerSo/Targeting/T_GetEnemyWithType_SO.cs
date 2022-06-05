using General;
using Managers;
using System;
using SO.TowerSo.Targeting;
using UnityEngine;
[CreateAssetMenu(fileName = "Targeting", menuName = "ScriptableObjects/Targeting/GetEnemyWithType")]

public class T_GetEnemyWithType_SO : SO.TowerSo.Targeting.TargetingSo
{
    public EnemyType enemyType;
    public override Transform GetTheTarget()
    {
       // Debug.Log(" Get Closest of enemytype");
        return null;
    }

}
