using UnityEngine;

namespace SO.TowerSo.Targeting
{
    public abstract class TargetingSo : ScriptableObject
    {
        protected GameObject Owner;
        protected float MaxRange;
    
        public void FixedRefresh()
        {
        }

        public virtual void Init(GameObject owner,float maxRange)
        {
            this.Owner = owner;
            MaxRange = maxRange;
        }
        public virtual Transform GetTheTarget()
        {
            return null;
        }
        public virtual void Refresh()
        {

        }

    }
}
