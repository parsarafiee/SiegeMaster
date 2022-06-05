using Managers;
using UnityEngine;

namespace SO.Enemies.Targeting
{
    [CreateAssetMenu(fileName = "Target Objective", menuName = "ScriptableObjects/Targeting/Target Nexus")]
    public class TargetObjectiveSo : SO.TowerSo.Targeting.TargetingSo
    {
        #region Fields
        private Transform _nexus;
        #endregion

        #region Methods
        public override void Init(GameObject unit, float range)
        {
            base.Init(unit, range);
            _nexus = NexusManager.Instance.GetTransform;        
        }

        public override Transform GetTheTarget()
        {
            return _nexus;
        }
        #endregion
    }
}
