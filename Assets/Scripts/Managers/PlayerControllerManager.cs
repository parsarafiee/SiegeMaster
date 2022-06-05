using General;
using Inputs;
using UnityEngine;
using System.Linq;

namespace Managers
{
    public class PlayerControllerManager : Manager<PlayerController, PlayerControllerManager>
    {
        public override void Init()
        {
            foreach (var player in Object.FindObjectsOfType<PlayerController>().ToList())
            {
                Add(player);
            }
            
            base.Init();
        }
    }
}
