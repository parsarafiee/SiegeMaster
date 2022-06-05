using System;
using Abilities;
using General;
using Units.Interfaces;
using Units.Statistics;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Animator), typeof(AbilityHandler))]
    public abstract class Unit : MonoBehaviour, IUpdatable, IPoolable, IMovable, ITargetAcquirer, IHittable

    {
        #region Properties and Variables
        //Component Cache
        protected Rigidbody Rigidbody;
        protected Animator Animator;
        public AbilityHandler AbilityHandler { get; private set; }

        //Stats
        [SerializeField] public float turningSpeed;
        private float _turnSmoothVelocity;
        [SerializeField] public float speed;

        #region Targeting stuff

        [SerializeField] private Transform shootingPosition;
        public Transform ShootingPosition => shootingPosition;
        public Vector3 AimingDirection => TargetPosition - shootingPosition.position;

        protected abstract Vector3 AimedPosition { get; }

        public Vector3 TargetPosition => AimedPosition;
        public Transform TargetTransform { get; set; }

        #endregion

        #region Gold

        [SerializeField] protected int goldValue;
        public int GoldValue => goldValue;

        public int Gold { get; private set; }

        public void AddGold(int gold)
        {
            Gold += gold;
        }

        public void RemoveGold(int gold)
        {
            Gold -= gold;
        }

        #endregion
        
        public Stats stats;
        
        #region Events

        public bool IsDead { get; protected set; }
        public Action OnDeath;
        #endregion

        [SerializeField] private bool isPlayer;
        public bool IsPlayer => isPlayer;

        #endregion

        public virtual void Init()
        {
            //Caching Components

            TryGetComponent<Rigidbody>(out Rigidbody);
            Animator = GetComponent<Animator>();
            AbilityHandler = GetComponent<AbilityHandler>();
            AbilityHandler.Init();

            OnDeath = OnDeathEvent;
            stats.Init(this);
        }

        public virtual void PostInit()
        {
            AbilityHandler.PostInit();
        }

        public virtual void Refresh()
        {
            AbilityHandler.Refresh();
            if(!IsDead)
                stats.Refresh();
        }

        public virtual void FixedRefresh()
        {
        }

        public void LateRefresh()
        {
        }

        public virtual void Pool()
        {
        }

        public virtual void Depool()
        {
        }

        public virtual void Move(Vector3 direction)
        {
            if (Rigidbody == null) return;
            var smoothAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg,
                    ref _turnSmoothVelocity, turningSpeed);

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }

        public virtual void Jump()
        {
        }

        public abstract void GotShot(float damage);
        protected virtual void OnDeathEvent()
        {
            
        }
    }
}