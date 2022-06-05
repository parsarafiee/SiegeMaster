using Managers;
using Units.Types;
using Units.Types.Towers;
using UnityEngine;

namespace Abilities.AbilitySO
{ 
    [CreateAssetMenu(fileName = "TowerAbility", menuName = "ScriptableObjects/Abilities/TowerAbility")]
    public class TowerBuildAbility : AbilitySo
    {
        public TowerType towerType;

        protected override void ReadyStateRefresh()
        {
            
        }

        protected override void OnCast()
        {
            TowerManager.Instance.Create(towerType, new Tower.Args(TargetTransform.position));
        }

        protected override void OnActiveCast()
        {
            
        }

        protected override void ActiveStateRefresh()
        {
            
        }
    }
}
