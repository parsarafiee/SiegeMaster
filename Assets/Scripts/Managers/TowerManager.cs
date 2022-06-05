using System.Collections.Generic;
using System.Linq;
using Units.Types.Towers;

namespace Managers
{
    public enum TowerType { ArcherTower, Catapult,SeekingTower,FireBall , ArcherTowerPrediction }
    public class TowerManager : General.Manager<Tower, TowerType, Tower.Args, TowerManager>
    {
        protected override string PrefabLocation => "Prefabs/Towers/";

        public override void Init()
        {
            var hashSet = new HashSet<Tower>(UnityEngine.Object.FindObjectsOfType<Tower>().ToList());
            foreach (var item in hashSet)
            {
                Add(item);
            }

            base.Init();
        }

    }
}