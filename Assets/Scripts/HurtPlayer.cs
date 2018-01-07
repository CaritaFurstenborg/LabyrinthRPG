using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageAmount;
    public GameObject dmgNumDisplay;

    private PlayerStats pStats;
    private int currentDmg;

	// Use this for initialization
	void Start () {
        pStats = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player") // check if colliding with player
        {
            currentDmg = damageAmount - pStats.currentDefLevel;

            if(currentDmg <= 0)
            {
                currentDmg = 1;
            }
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDmg);

            var clone = (GameObject)Instantiate(dmgNumDisplay, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().dmgNumber = currentDmg;
        }

    }
}
