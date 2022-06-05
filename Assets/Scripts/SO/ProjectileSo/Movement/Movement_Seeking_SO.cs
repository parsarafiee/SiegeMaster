using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;
using System.Reflection;

using General;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Seeking")]

public class Movement_Seeking_SO : Movement_SO
{
    private Enemy enemy;
    public override void Refresh()
    {

        if (enemy.alive == true)
        {
            rb.velocity = (target.position - gameobject.transform.position).normalized * initialSpeed;
            gameobject.transform.forward = rb.velocity.normalized;

        }
    }
    public override void Init(GameObject _gameObject, Transform _target, float speed, Vector3 _projectileInitialDIrection)
    {
        base.Init(_gameObject, _target, speed, _projectileInitialDIrection);
        enemy = _target.gameObject.GetComponent<Enemy>();
    }
}
