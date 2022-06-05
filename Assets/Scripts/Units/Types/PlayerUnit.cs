using System;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public abstract class PlayerUnit : Unit,ICameraController
    {
        public Transform RespawnPosition;

        public Action OnRespawn;
        protected abstract override Vector3 AimedPosition { get; }
        
        public override void Init()
        {
            base.Init();
            OnRespawn = OnRespawnEvent;
        }
        

        public override void GotShot(float damage)
        {
            stats.health.Current -= damage;
           // Debug.Log("Player got damage, current health : " + stats.health.Current);
        }

        protected override void OnDeathEvent()
        {
            transform.position = RespawnPosition.position;
            stats.health.Current = stats.health.Maximum;
        }

        protected void OnRespawnEvent()
        {
            //TODO: Reset HP and Mana
        }

        public abstract void Look();
    }
}