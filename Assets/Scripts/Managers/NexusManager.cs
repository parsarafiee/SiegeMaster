using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class NexusManager : Manager<Nexus, NexusManager>
    {
        Nexus nexus;
        public Transform GetTransform { get { return nexus.transform; } }
        public override void Init()
        {
            nexus = Object.FindObjectOfType<Nexus>();
            Add(nexus);

            base.Init();
        }

        public void DealDamage(float damage)
        {
            nexus.GotShot(damage);
        }
    }
}
