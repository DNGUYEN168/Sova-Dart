using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{

    [Header("Arrow Information")]
    public static int BounceAmount;
    public int chargeRate = 1;
    int chargeAmount = 0;



    public GameObject SonarDart;
    public Camera PlayerPOV;
    

    private Vector3 customRotationAngles = new Vector3(90, 0, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (0 <= chargeAmount && chargeAmount < 100)
            { chargeAmount += chargeRate; }
        }

        // Check if the left mouse button was released
        if (Input.GetMouseButtonUp(0))
        {
            SpawnArrow();
            chargeAmount = 0;


        }
        if (Input.GetMouseButtonDown(1)) // right mouse button change how many bounces 
        {
            if (0 <= BounceAmount && BounceAmount < 2) {BounceAmount++; }
            else {BounceAmount = 0; }
            Debug.Log("charged to: " + BounceAmount);
        }

    }

    // on leftmouse release spawn the arrow with speed based on the chargeAmount 
    public void SpawnArrow()
    {
        Vector3 shootfrom = PlayerPOV.transform.position;

        Quaternion arrowRotation = PlayerPOV.transform.rotation * Quaternion.Euler(customRotationAngles);

        shootfrom.z = shootfrom.z + 0.7f;
        

        GameObject Dart = Instantiate(SonarDart, shootfrom, arrowRotation);

        Rigidbody rb = Dart.GetComponent<Rigidbody>(); // darts rigidbody

        rb.AddForce(PlayerPOV.transform.forward * chargeAmount, ForceMode.Impulse);
    }
    
}
