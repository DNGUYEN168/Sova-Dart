using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager instance;

    // Property to get the Singleton instance
    public static CoroutineManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Create a new GameObject to hold the CoroutineManager
                GameObject obj = new GameObject("CoroutineManager");
                instance = obj.AddComponent<CoroutineManager>();

                // Ensure the CoroutineManager isn't destroyed when loading new scenes
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
}
