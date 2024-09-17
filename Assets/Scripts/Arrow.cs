using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // arrwos stick to wall 
    public LayerMask Ground;
    public LayerMask Wall;

    private void Start()
    {
        Debug.Log(ShootArrow.BounceAmount);
        
    }



}
