using System;
using Units.Types;
using UnityEngine;


namespace Units.Statistics
{
    [Serializable]
    public abstract class Stat
    {
        protected Unit Owner;
        public float Current { get; set; }

        public float Maximum => baseValue;

        [SerializeField] protected float baseValue;

        public virtual void Init(Unit owner)
        {
            Current = baseValue;
            Owner = owner;
        }
    }
    [Serializable]
    public class Regeneration : Stat
    {
        [SerializeField] public float timeInterval;
        private float _currentTime;
        private Stat _stat;

        public void InitRegen(Stat stat, Unit owner)
        {
            _stat = stat;
            Init(owner);
        }
        public override void Init(Unit owner)
        {
            base.Init(owner);
            _currentTime = timeInterval;
        }

        public void Refresh()
        {
            _currentTime -= Time.deltaTime;

            if (!(_currentTime <= 0) || !(_stat.Current < _stat.Maximum)) return;
            _stat.Current += baseValue;
            _currentTime = timeInterval;
        }
    }
    [Serializable]
    public class Health : Stat
    {
        public Regeneration regeneration;

        public override void Init(Unit owner)
        {
            base.Init(owner);
            regeneration.InitRegen(this, owner);
        }

        public void Refresh()
        {
            regeneration.Refresh();
            if (Current <= 0)
            {
                Owner.OnDeath.Invoke();
            }
        }
    }

    [Serializable]
    public class Mana : Stat
    {
        public Regeneration regeneration;

        public override void Init(Unit owner)
        {
            base.Init(owner);
            regeneration.InitRegen(this, owner);
        }

        public void Refresh()
        {
            regeneration.Refresh();
        }
    }
}