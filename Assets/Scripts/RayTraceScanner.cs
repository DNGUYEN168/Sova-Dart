using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class RayTraceScanner : MonoBehaviour
{
    //public Vector3 targetScale = new Vector3(20f, 20f, 2f); // Target scale
    public float scaleSpeed = 2f; // Speed of scaling
    private string desiredLayer = "Enemy";

    public Material highlightMat;
    public Material oldMat;

    private void Update()
    {
        float ScaleTime = Time.deltaTime * scaleSpeed;
        //transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
        Vector3 newScale = new Vector3(transform.localScale.x + ScaleTime, transform.localScale.y + ScaleTime, transform.localScale.z + ScaleTime);
        transform.localScale = newScale;
    }

    private void OnTriggerEnter(Collider other)
    {

        Renderer objectRenderer = other.gameObject.GetComponent<Renderer>();
        if (other.gameObject.layer == LayerMask.NameToLayer(desiredLayer)) // will always hit because it passes through walls 
        {
            if (PerformRaycast(other.transform.position))
            {
                StartCoroutine(ChangeColorCoroutine(objectRenderer));
            }
        }
        
    }


    private bool PerformRaycast(Vector3 position)
    {
        Vector3 castAngle = CalcAngle(transform.position, position);
        // perform a raycast from 
        // transform.position -> position, if raycast is not obstructed, do something 

        RaycastHit hit;
        //Debug.DrawRay(transform.position, castAngle * transform.localScale.z, Color.red);
        return (Physics.Raycast(transform.position, castAngle, out hit, transform.localScale.z) && hit.collider.tag == desiredLayer); // only if raycast hits enenmy tag
       
        //
    }

    private Vector3 CalcAngle(Vector3 position1, Vector3 position2)
    {
        // vec3 angle is p2 - p1
        Vector3 angleVector = position2 - position1;
        return angleVector;
    }

    private IEnumerator ChangeColorCoroutine(Renderer objectRenderer)
    {
        // Get the Renderer component
        objectRenderer.material = highlightMat;
        // Wait for the specified duration
        yield return new WaitForSeconds(0.5f);

        // Restore the original color
        objectRenderer.material = oldMat;
    }
}
