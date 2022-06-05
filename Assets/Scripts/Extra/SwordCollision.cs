using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;

public class SwordCollision : MonoBehaviour
{
    #region Fields
    PlayerUnit target;
    Collider collider;
    float damage;
    [SerializeField] AudioSource attackSound;
    #endregion

    #region Methods
    public void Init(float _damage, Transform _target)
    {
        damage = _damage;
        target = _target.GetComponentInParent<PlayerUnit>();
        collider = GetComponent<Collider>();
    }

    public void ToggleColliderActive(bool setActive)
    {
        collider.enabled = setActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackSound.Play();
            target.GotShot(damage);
            ToggleColliderActive(false);
        }
    }
    #endregion
}
