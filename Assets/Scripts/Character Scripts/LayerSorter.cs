using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour {

    private SpriteRenderer pRenderer;

    private List<Obstacle> obstacles = new List<Obstacle>();

	// Use this for initialization
	void Start () {
        pRenderer = transform.parent.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Obstacle")
        {
            Obstacle o = other.GetComponent<Obstacle>();
            o.FadeOut();

            if(obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder -1 < pRenderer.sortingOrder)
            {
                pRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
            }

            obstacles.Add(o);
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Obstacle o = other.GetComponent<Obstacle>();
            o.FadeIn();

            obstacles.Remove(o);

            if(obstacles.Count == 0)
            {
                pRenderer.sortingOrder = 200;
            }
            else
            {
                obstacles.Sort();
                pRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
            }            
        }
        
    }
}
