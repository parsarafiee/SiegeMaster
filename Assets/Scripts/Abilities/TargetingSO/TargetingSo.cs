using Units.Interfaces;
using UnityEngine;

namespace Abilities.TargetingSO
{
    public abstract class TargetingSo : ScriptableObject
    {
        protected ITargetAcquirer Owner;
        protected float MaxRange;
        protected Transform TemporaryTransform;
        

        public Transform TargetTransform => TemporaryTransform;
        
        public virtual void Init(ITargetAcquirer owner, float maxRange)
        {
            Owner = owner;
            MaxRange = maxRange;
            TemporaryTransform = new GameObject("tempTransform_targetingSO").transform;
        }

        public abstract void Refresh();
    }
}