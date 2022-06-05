using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class PlayerUnitManager : Manager<PlayerUnit, PlayerUnitManager>
    {
        public Rigidbody GetRigidbody { get; private set; }
        public Transform GetTransform { get; private set; }
        public Transform GetHipsTransform { get; private set; }

        public override void Init()
        {
            
            foreach (var player in Object.FindObjectsOfType<PlayerUnit>().ToList())
            {
                Add(player);
                GetTransform = player.transform;
                GetHipsTransform= player.GetComponentInChildren<Hips>().transform;
                GetRigidbody = player.GetComponent<Rigidbody>();
            }

            base.Init();
        }

        public void AddGold(int gold)
        {
            foreach (var player in collection)
            {
                player.AddGold(gold);
            }
        }
    }
}
