using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTraceScanner : MonoBehaviour
{
    //public Vector3 targetScale = new Vector3(20f, 20f, 2f); // Target scale
    public float scaleSpeed = 2f; // Speed of scaling
    public LayerMask EnemyLayer;

    private void Update()
    {
        float ScaleTime = Time.deltaTime * scaleSpeed;
        //transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
        Vector3 newScale = new Vector3(transform.localScale.x + ScaleTime, transform.localScale.y + ScaleTime, transform.localScale.z + ScaleTime);
        transform.localScale = newScale;
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("hiot");
        if (other.gameObject.layer == EnemyLayer) { Debug.Log("We just hit: " + other.gameObject.name); }
        
    }

}
