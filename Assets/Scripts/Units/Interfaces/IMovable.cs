using UnityEngine;

namespace Units.Interfaces
{
    public interface IMovable
    {
        public void Move(Vector3 direction);
        public void Jump();
        
    }
}