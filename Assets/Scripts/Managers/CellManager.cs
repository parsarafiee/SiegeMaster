using System.Collections.Generic;
using System.Linq;
using General;
using Units.BossEnemy;
using Units.Types;
using UnityEngine;

public enum CellType {Normal,Cube};
public class CellManager : Manager<Cell, CellType, Cell.Args, CellManager>
{
    protected override string PrefabLocation => "Prefabs/Cells/";

    
    public override void Init()
    {
        var hashSet = new HashSet<Cell>(Object.FindObjectsOfType<Cell>().ToList());
        foreach (var item in hashSet)
        {
            Add(item);
        }

        base.Init();
    }
    public Transform GetClosest(Transform currentPosition, float range)
    {
        Transform transform = null;
        range *= range;
        foreach (var cell in collection)
        {
            var newDistance = Vector3.SqrMagnitude(currentPosition.position - cell.transform.position);
            if (newDistance < range ) // if in the range and they are alive (not in the pool)
            {
                range = newDistance;
                transform = cell.transform;
            }
        }
        return transform;
    }

}
