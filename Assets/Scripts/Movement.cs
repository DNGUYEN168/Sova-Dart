using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //take player input and update the position of the capsule per input 
    }
}
