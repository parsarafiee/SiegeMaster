using General;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using SO.TowerSo.Targeting;
using System.Collections;
using Units.Interfaces;

namespace Units.Types
{
    public enum EnemyStates
    {
        Wander,
        DeathAnimation,
        Death,
        Attacking
    }

    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        #region Fields
        #region Enemy states
        EnemyStates enemyState;
        #endregion

        #region Set Enemy Type
        public EnemyType enemyType;
        public EnemyMovement_SO movement_SO;
        public TargetingSo targeting_SO;
        public Attack_SO attack_SO;
        const int SpeedOffset = 2;

        protected override Vector3 AimedPosition
        {
            get
            {
                //useless for enemies right now
                return Vector3.zero;
            }
        }
        #endregion

        #region Get Components
        private NavMeshAgent _enemyAgent;
        private Transform _player;
        private Transform _objective;
        #endregion

        #region Death
        public bool alive;
        float _delayToPool = 8;
        Collider collider;
        #endregion

        #region Attacking
        public float rangeToMoveTowardPlayer;
        public float maxAttackRange;
        public float minAttackRange;
        float attackRange;
        const float EnemyDamageToNexus = 1;
        #endregion

        #region Animation
        static readonly int Speed = Animator.StringToHash("Speed");
        static readonly int IsDead = Animator.StringToHash("IsDead");
        #endregion

        #region UI & HP
        public Canvas canvasParent;
        public Transform hpInPoolParent;
        public int currentHp;
        int _fullHp;
        Stack<Hp> _hpStack;
        Vector3 _facingDirUI;

        #endregion

        #region Sounds
        [SerializeField] AudioSource loseHpSound;
        [SerializeField] AudioSource deathSound;
        [SerializeField] AudioSource nexusArrivedSound;
        #endregion
        #endregion

        #region Methods
        #region Game Control & Flow
        public override void Init()
        {
            base.Init();
            _enemyAgent = GetComponent<NavMeshAgent>();
            _player = PlayerUnitManager.Instance.GetHipsTransform;
            _objective = NexusManager.Instance.GetTransform;

            enemyState = EnemyStates.Wander;
            alive = true;
            speed = Random.Range(speed, speed * SpeedOffset);
            _enemyAgent.speed = speed;
            collider = GetComponent<Collider>();
            attackRange = Random.Range(minAttackRange, maxAttackRange);

            _fullHp = currentHp;
            _hpStack = new Stack<Hp>();

            movement_SO = Instantiate(movement_SO);
            targeting_SO = Instantiate(targeting_SO);
            movement_SO.Init(gameObject, _objective, speed);
            targeting_SO.Init(gameObject, rangeToMoveTowardPlayer);
            if (attack_SO)
            {
                attack_SO = Instantiate(attack_SO);
                attack_SO.Init(_enemyAgent, ShootingPosition, _player);
            }

        }

        public override void PostInit()
        {
            base.PostInit();
        }

        public override void Refresh()
        {
            switch (enemyState)
            {
                case EnemyStates.Wander:
                    Move(targeting_SO.GetTheTarget().position);
                    FacingUIToPlayer();
                    GetReadyToAttack();
                    break;
                case EnemyStates.DeathAnimation:
                    DeathAnimation();
                    break;
                case EnemyStates.Death:
                    Death();
                    break;
                case EnemyStates.Attacking:
                    AttackState();
                    GetReadyToAttack();
                    break;
                default:
                    break;
            }
        }

        public override void FixedRefresh()
        {
        }

        #endregion

        #region Factory & Pool manage
        public class Args : ConstructionArgs
        {
            public Transform parent;

            public Args(Vector3 _spawningPosition, Transform parent) : base(_spawningPosition)
            {
                this.parent = parent;
            }
        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            _enemyAgent.enabled = true;
            transform.SetParent(constructionArgs.parent);
            collider.enabled = true;
            enemyState = EnemyStates.Wander;
            currentHp = _fullHp;
            alive = true;
            _delayToPool = 8;
            attackRange = Random.Range(minAttackRange, maxAttackRange);
            speed = Random.Range(speed, speed * SpeedOffset);
            _hpStack.Clear();
            CreateHp(_fullHp);
        }

        public override void Pool()
        {
            base.Pool();
            alive = false;
            gameObject.SetActive(false);
        }

        public override void Depool()
        {
            base.Depool();
            gameObject.SetActive(true);
        }

        #endregion

        #region Movement
        public override void Move(Vector3 direction)
        {
            Animator.SetFloat(Speed, speed);
            if (direction != Vector3.zero)
                movement_SO.MoveToPoint(direction);
        }

        #endregion

        #region Damage & Death manage
        public override void GotShot(float damage)
        {
            if (currentHp >= 0)
            {
                if (!loseHpSound.isPlaying) loseHpSound.Play();
                damage = Mathf.Clamp(damage, 0, currentHp);
                currentHp -= (int)damage;

                for (var i = 0; i < damage; i++)
                {
                    var h = _hpStack.Pop();
                    h.transform.SetParent(hpInPoolParent);
                    HPManager.Instance.Pool(HPType.EnemyHp, h);
                }
            }

            if (alive && currentHp <= 0)
            {
                enemyState = EnemyStates.DeathAnimation;
                PlayerUnitManager.Instance.AddGold(goldValue);
            }
        }

        private void DeathAnimation()
        {
            if (gameObject.activeInHierarchy)
                _enemyAgent.ResetPath();
            if (!deathSound.isPlaying) deathSound.Play();
            Animator.SetTrigger(IsDead);
            alive = false;
            enemyState = EnemyStates.Death;
        }

        void Death()
        {
            loseHpSound.Stop();
            collider.enabled = false;
            StartCoroutine(DealyToPool());
        }

        IEnumerator DealyToPool()
        {
            yield return new WaitForSeconds(_delayToPool);
            nexusArrivedSound.Stop();
            deathSound.Stop();
            _enemyAgent.enabled = false;
            EnemyManager.Instance.Pool(enemyType, this);
        }

        #endregion

        #region HP & UI manage
        private void CreateHp(int numberOfHp)
        {
            for (int i = 0; i < numberOfHp; i++)
            {
                _hpStack.Push(HPManager.Instance.Create(HPType.EnemyHp,
                    new Hp.Args(Vector3.zero, canvasParent.transform)));
            }
        }

        private void FacingUIToPlayer()
        {
            _facingDirUI = _player.transform.position - transform.position;
            canvasParent.transform.forward = _facingDirUI;
        }

        #endregion

        #region Attacking Player & Nexus
        void GetReadyToAttack()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= attackRange)
                enemyState = EnemyStates.Attacking;
            if (Vector3.Distance(transform.position, _player.transform.position) > attackRange)
            {
                if (attack_SO) attack_SO.ResetBehaviors(Animator);
                enemyState = EnemyStates.Wander;
            }
        }

        void AttackState()
        {
            if (attack_SO)
            {
                FacingToTarget();
                attack_SO.Refresh(Animator);
            }
        }

        void FacingToTarget()
        {
            Vector3 dir = (_player.position - transform.position).normalized;
            transform.forward = dir;

            Vector3 rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
        }

        void OnCollisionEnter(Collision collision)
        {
            #region Deal damage to Nexus
            if (!collision.gameObject.CompareTag("Nexus")) return;
            if (!nexusArrivedSound.isPlaying) nexusArrivedSound.Play();
            GotShot(currentHp);
            _delayToPool = 1;
            NexusManager.Instance.DealDamage(EnemyDamageToNexus);
            #endregion
        }

        #endregion

        #region Old targeting logic
        /*public Transform[] targets;
        int waypointCounter = 0;
        void WaypointsCheck()
        {
            if (Vector3.Distance(targets[waypointCounter].position, player.position) < 0.1f)
            {
                waypointCounter++;
                Debug.Log(waypointCounter);
            }
            if (targets.Length <= waypointCounter)
            {
                waypointCounter = 0;
            }
            //movement_SO.MoveToPoint(targets[waypointCounter].position);
            //rb.velocity = 20 * (targets[waypointCounter].position - player.position).normalized;
        }*/

        #endregion
        #endregion
    }
}