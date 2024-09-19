using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // arrwos stick to wall 
    public LayerMask Ground;
    public LayerMask Wall;
    public LayerMask Player;
    Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        StopArrow();
        return;
    }

    private void Update()
    {
        if (rb.velocity.sqrMagnitude < 0.001f && rb.angularVelocity.sqrMagnitude < 0.001f) // only if our arow is not moving we do the ray casting 
        {
            
            //PerformRaycast();
          
        }
    }

    private void StopArrow()
    {
        // we check if the arrow has hit a wall 
        bool isTouchingGround = Physics.CheckSphere(transform.position, 0.4f, Ground);
        bool isTouchingWall = Physics.CheckSphere(transform.position, 0.4f, Wall);
        bool isTouchingPlayer = Physics.CheckSphere(transform.position, 0.4f, Player);

        if (ShootArrow.BounceAmount == 0 && (isTouchingGround || isTouchingWall || isTouchingPlayer)) // no bounces and touches wall/floor
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            float Lifetime = 3.0f;
            Destroy(gameObject, Lifetime);
        }
        else if (isTouchingGround || isTouchingWall || isTouchingPlayer)
        {
            ShootArrow.BounceAmount--;
        }

    }

    private void PerformRaycast()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out RaycastHit hitInfo, 20f))
        {
            Debug.Log("hittttt");

        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) *20f, Color.red);
    }


}
