using General;
using UnityEngine;
namespace UI
{
    public abstract class UIElement : MonoBehaviour, IUpdatable
    {
        public virtual void Init()
        {
            
        }

        public virtual void PostInit()
        {
            
        }

        public virtual void Refresh()
        {
            
        }

        public virtual void FixedRefresh()
        {
            
        }

        public virtual void LateRefresh()
        {
            
        }
    }
}