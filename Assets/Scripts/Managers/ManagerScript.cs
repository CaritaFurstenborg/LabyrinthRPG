using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

    private static ManagerScript managers;

    void Awake()
    {
        // Removes multiple copies if exits
        if (managers == null)
        {
            managers = this;
        }
        else if (managers != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
