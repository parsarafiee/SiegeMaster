using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;

using System;
using SO.TowerSo.Targeting;

[CreateAssetMenu(fileName = "Targeting", menuName = "ScriptableObjects/Targeting/T_GetClosetOfManagerType_SO")]

public class T_GetClosetOfManagerType_SO : SO.TowerSo.Targeting.TargetingSo
{
    public enum ManagerType {Enemy,Tower,Projectile,Cell }
    public ManagerType[] managerType;
    private System.Type GetManagerType(ManagerType eType)
    {
        System.Type type = null;
        switch (eType)
        {
            case ManagerType.Enemy:
                type = typeof(EnemyManager);
                break;
            case ManagerType.Tower:
                type = typeof(TowerManager);
                break;
            case ManagerType.Projectile:
                type = typeof(ProjectileManager);
                break;
            case ManagerType.Cell:
                type = typeof(CellManager);
                break;
            default:
                Debug.Log("unhandled SwitchCase");
                break;
        }
        return type;
    }
    public override Transform GetTheTarget()
    {
        foreach (var managerType in managerType)
        {
            return Helper.GetClosetInRange(GetManagerType(managerType), Owner.transform, MaxRange);
        }
         return null;
       
    }

}
