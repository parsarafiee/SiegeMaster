using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Catapult")]

public class Movement_Catapult_SO : Movement_SO
{

    public override void Init(GameObject _unit, Transform _targetTransform, float _speed, Vector3 _projectileInitialDIrection)
    {
        base.Init(_unit,_targetTransform, _speed, _projectileInitialDIrection);
        try
        {

            rb.velocity = _projectileInitialDIrection;
        }
        catch (System.Exception)
        {

            throw;
        }
        
    }

}
