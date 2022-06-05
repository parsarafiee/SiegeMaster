using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AttackStates { GetReadyToShoot, Shoot, Cooldown, CheackIfReadyToAttack }

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Attack/Projectile Attack")]
public class ProjectileAttack_SO : Attack_SO
{
    #region Fields
    #region Set Projectile Type
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float projectileSpeed;
    [SerializeField] float AttackAnimationLengh;
    AudioSource audio;
    #endregion

    #region Game Flow Control
    AttackStates attackState;
    #endregion
    #endregion

    #region Methods
    #region Game Flow
    public override void Init(NavMeshAgent _ownerNavMesh, Transform _ownerPos, Transform _target)
    {
        base.Init(_ownerNavMesh, _ownerPos, _target);
        attackState = AttackStates.GetReadyToShoot;
        audio = _ownerNavMesh.GetComponent<AudioSource>();
    }

    public override void Refresh(Animator _anim)
    {
        switch (attackState)
        {
            case AttackStates.GetReadyToShoot:
                StandbyToShoot(_anim);
                break;
            case AttackStates.Shoot:
                Attack(_anim);
                break;
            case AttackStates.Cooldown:
                Cooldown(_anim);
                break;
            case AttackStates.CheackIfReadyToAttack:
                CheackIfReadyToAttack(_anim);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Shooting Phase
    void StandbyToShoot(Animator _anim)
    {
        _anim.SetFloat(Speed, 0);
        ownerNavMesh.isStopped = true;
        _anim.ResetTrigger(movementAnimState);
        attackState = AttackStates.Shoot;
    }

    protected override void Attack(Animator _anim)
    {
        timer += Time.deltaTime;
        base.Attack(_anim);
        if (timer > AttackAnimationLengh)
        {
            timer = 0;
            InstantiateAProjectile();
        }
    }

    void InstantiateAProjectile()
    {
        ProjectileManager.Instance.Create(projectileType,
            new Projectile.Args(ownerPos.position, projectileType,
            target, projectileSpeed, attackDamage, Vector3.zero, false));

        attackState = AttackStates.Cooldown;
    }
    #endregion

    #region Cooldown
    protected override void Cooldown(Animator _anim)
    {
        base.Cooldown(_anim);
        if (!audio.isPlaying) audio.Play();
        _anim.SetFloat(Speed, 0);
        if (isAnimSetted)
            _anim.SetTrigger(movementAnimState);
        isAnimSetted = false;
        attackState = AttackStates.CheackIfReadyToAttack;
    }

    void CheackIfReadyToAttack(Animator _anim)
    {
        timer += Time.deltaTime;
        if (timer > cooldownTimer)
        {
            timer = 0;
            _anim.ResetTrigger(movementAnimState);
            attackState = AttackStates.GetReadyToShoot;
        }
    }
    #endregion

    public override void ResetBehaviors(Animator _anim)
    {
        base.ResetBehaviors(_anim);
        attackState = AttackStates.GetReadyToShoot;
        timer = 0;
        audio.Stop();
    }
    #endregion
}
