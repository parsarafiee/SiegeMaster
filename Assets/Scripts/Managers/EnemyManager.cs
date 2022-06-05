using System.Collections.Generic;
using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public enum EnemyType { ArcherEnemy, SneakyEnemy, WarriorEnemy }

    public class EnemyManager : Manager<Enemy, EnemyType, Enemy.Args, EnemyManager>
    {
        public int Count => collection.Count;
        protected override string PrefabLocation => "Prefabs/Enemies/";

        public override void Init()
        {
            var hashSet = new HashSet<Enemy>(Object.FindObjectsOfType<Enemy>().ToList());
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
            foreach (var enemy in collection)
            {
                var newDistance = Vector3.SqrMagnitude(currentPosition.position - enemy.transform.position);

                if (newDistance < range && enemy.alive) // if in the range and they are alive (not in the pool)
                {
                    range = newDistance;
                    transform = enemy.transform;
                }
            }
            return transform;
        }
    }
}
