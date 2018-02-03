using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {

    private static CameraFollow cam;

    private Transform target;

    [SerializeField]
    private BoxCollider2D boundBox;

    private float xMax, xMin, yMin, yMax;
    

	// Use this for initialization
	void Start () {

        if (cam == null)
        {
            cam = GetComponent<CameraFollow>();
        }

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
               
	}
	
	private void LateUpdate()
    {
        if (FindObjectOfType<MainGameObjectsManager>().gameObject.activeSelf)
        {
            if (boundBox == null)
            {
                boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();

                Vector3 minTile = boundBox.bounds.min;
                Vector3 maxTile = boundBox.bounds.max;

                SetLimits(minTile, maxTile);
            }

            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10);
        }       
    }

    private void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xMin = minTile.x + width / 2;
        xMax = maxTile.x - width / 2;

        yMin = minTile.y - height / 2;
        yMax = maxTile.y - height / 2;
    }
}
