using System.Collections.Generic;
using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public enum HPType { EnemyHp, NexusHp  }

    public class HPManager : Manager<Hp, HPType, Hp.Args, HPManager>
    {
        protected override string PrefabLocation => "Prefabs/Canvas/Enemies UI/";    
    }
}