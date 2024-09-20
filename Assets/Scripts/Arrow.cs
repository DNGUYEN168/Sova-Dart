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

            //TriggerEffectTwice();


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
            float Lifetime = 2.0f;
            
            StartCoroutine(TriggerEffectTwice(Lifetime));
   

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

    void SpawnTerrainScanner()
    {
        GameObject terrainScanner = Instantiate(TerrainScannerPrefab, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem terrainScannerPS = terrainScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

        if (terrainScannerPS != null)
        {
            var main = terrainScannerPS.main;
            main.startLifetime = duration;
            main.startSize = size;
        }
        else
        {
            Debug.Log("dont got so particles here son");
        }
        Destroy(terrainScanner, duration + 1);
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
