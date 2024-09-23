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

    public GameObject TerrainScannerPrefab;
    public float duration;
    public float size;

    private int SingleCall = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //spawn a single occurance of the sphere object
        if (SingleCall == 0) { StopArrow(); SingleCall++; return; }
        return;


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
            float Lifetime = 2.0f;
            
            StartCoroutine(TriggerEffectTwice(Lifetime));
   

        }
        else if (isTouchingGround || isTouchingWall || isTouchingPlayer)
        {
            ShootArrow.BounceAmount--;
        }

    }

    void SpawnTerrainScanner()
    {
        
        GameObject terrainScanner = Instantiate(TerrainScannerPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(terrainScanner, duration);
    }

    IEnumerator TriggerEffectTwice(float Delay)
    {
        yield return new WaitForSeconds(.2f);
        // Trigger the effect the first time
        SpawnTerrainScanner();

        // Wait for the delay
        yield return new WaitForSeconds(Delay);

        // Trigger the effect the second time
        SpawnTerrainScanner();
        Destroy(gameObject);
    }


}
