using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject damageBurst;
    public Transform hitArea;
    public GameObject dmgNumDisplay;

    private int currentDmg;

    private PlayerStats pStats;

	// Use this for initialization
	void Start () {
        pStats = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentDmg = damageToGive + pStats.currentAttackLevel;

            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDmg);
            Instantiate(damageBurst, hitArea.position, hitArea.rotation);

            var clone = (GameObject) Instantiate(dmgNumDisplay, hitArea.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().dmgNumber = currentDmg;
        }
    }
}
