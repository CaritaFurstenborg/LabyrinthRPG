using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

    private BoxCollider2D bounds;
    private CameraFollow theCamera;


    void Update()
    {
        if(bounds == null)
        {
            bounds = GetComponent<BoxCollider2D>();
        }

        if(theCamera == null)
        {
            theCamera = FindObjectOfType<CameraFollow>();
            //theCamera.SetBounds(bounds);
        }
    }
}
