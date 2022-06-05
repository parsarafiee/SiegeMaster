using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Units.Types;

public enum PlayerStateAnimation { Falling,Running ,landed }
public class PlayerAnimation : MonoBehaviour
{
    public Transform groundCheckPosition;
    public LayerMask layerMask;
    PlayerStateAnimation  playerStateAnimation= new PlayerStateAnimation();
    Animator animator;
    PlayerPC playerPC;
    Rigidbody rb;
    bool canLand;
   [HideInInspector]public  bool isGrounded= true;
    private void Start()
    {
        playerPC = GetComponent<PlayerPC>();    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerStateAnimation = PlayerStateAnimation.Running;
    }
    public void Update()
    {
       
        RaycastHit hit;
        Ray downRay = new Ray(groundCheckPosition.position, -Vector3.up);
        if (Physics.Raycast(downRay, out hit, layerMask))
        {
            float distanceToGround = (Vector3.Distance(groundCheckPosition.position, hit.point));
            //  Debug.Log(hit.collider.gameObject.name);
            if (distanceToGround > 10)
            {
                isGrounded = false;
                canLand = true;
                playerStateAnimation = PlayerStateAnimation.Falling;
            }
            else if (distanceToGround > 1.3)
            {
                isGrounded =false;
            }
            else if (distanceToGround < 1.3 && canLand)
            {
                playerStateAnimation = PlayerStateAnimation.landed;
                canLand =false;
                isGrounded = true;

            }
            else
            {
                playerStateAnimation = PlayerStateAnimation.Running;
                isGrounded = true;
            }
        }


        switch (playerStateAnimation)
        {
            case PlayerStateAnimation.Falling:
                {
                    animator.SetBool("Flying", true);
                    break;
                }

               
            case PlayerStateAnimation.landed:
                {
                    animator.SetBool("Flying", false);
                    animator.SetTrigger("Land");
                    playerStateAnimation = PlayerStateAnimation.Running;

                    break;
                }
            case PlayerStateAnimation.Running:
                {
                    float anglForward = Vector3.Angle(transform.forward, rb.velocity);
                    float anglLeftRight = Vector3.Angle(transform.right, rb.velocity);
                    animator.SetFloat("SP", Mathf.Cos(anglLeftRight * Mathf.Deg2Rad) * rb.velocity.magnitude / playerPC.maxSpeed);
                    animator.SetFloat("SpeedZ", Mathf.Cos(anglForward * Mathf.Deg2Rad) * rb.velocity.magnitude / playerPC.maxSpeed);
                    break;
                }               
            default:
                break;
        }
        //.Log(grandCheckDistance);        
    }
    public void Jump()
    {
        animator.SetTrigger("Jump");
    }
    
}
