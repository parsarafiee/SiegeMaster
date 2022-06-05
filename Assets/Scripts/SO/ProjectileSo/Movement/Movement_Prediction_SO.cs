using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;
using System.Reflection;

using General;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Prediction")]

public class Movement_Prediction_SO : Movement_SO
{

    public override void Init(GameObject _gameObject, Transform _target, float speed, Vector3 _projectileInitialDIrection)
    {
        base.Init(_gameObject, _target, speed, _projectileInitialDIrection);
        _gameObject.transform.forward = _projectileInitialDIrection.normalized;
        rb.velocity = _projectileInitialDIrection;

    }
}