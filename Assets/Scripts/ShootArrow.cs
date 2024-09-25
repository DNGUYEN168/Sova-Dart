using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootArrow : MonoBehaviour
{

    [Header("Arrow Information")]
    public static int BounceAmount;
    public Image chargeBar;
    public Image Bounce1;
    public Image Bounce2;

    public float chargeRate = 1.0f;
    float chargeAmount = 0;

    float MaxChargeAmount = 100.0f;

    public GameObject SonarDart;
    public Camera PlayerPOV;
    

    private Vector3 customRotationAngles = new Vector3(90, 0, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (0 <= chargeAmount && chargeAmount < MaxChargeAmount)
            { chargeAmount += chargeRate; UpdateChargeBar(chargeAmount); }
        }

        // Check if the left mouse button was released
        if (Input.GetMouseButtonUp(0))
        {
            SpawnArrow();
            chargeAmount = 0.0f; // reset the charge 
            UpdateChargeBar(chargeAmount);
            Bounce1.color = Color.white; Bounce2.color = Color.white; // reset the bouncers


        }
        if (Input.GetMouseButtonDown(1)) // right mouse button change how many bounces 
        {
            if (0 <= BounceAmount && BounceAmount < 2) {BounceAmount++; }
            else {BounceAmount = 0; }
            ChangeBounceIcons();

        }
        

    }

    public void ChangeBounceIcons()
    {
        if (BounceAmount == 0) { Bounce1.color = Color.white; Bounce2.color = Color.white; }
        else if (BounceAmount == 1) { Bounce1.color = Color.blue; }
        else if (BounceAmount == 2) { Bounce2.color = Color.blue; }
    }
    public void UpdateChargeBar(float chargeAmount)
    {
        chargeBar.fillAmount =(float)( chargeAmount / MaxChargeAmount);
    }
    // on leftmouse release spawn the arrow with speed based on the chargeAmount 
    public void SpawnArrow()
    {
        Vector3 shootfrom = PlayerPOV.transform.position + PlayerPOV.transform.forward;

        Quaternion arrowRotation = PlayerPOV.transform.rotation * Quaternion.Euler(customRotationAngles);

        GameObject Dart = Instantiate(SonarDart, shootfrom, arrowRotation);

        Rigidbody rb = Dart.GetComponent<Rigidbody>(); // darts rigidbody

        rb.AddForce(PlayerPOV.transform.forward * chargeAmount, ForceMode.Impulse);
    }
    
}
