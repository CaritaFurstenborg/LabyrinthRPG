using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject damageBurst;
    public Transform hitArea;
    public GameObject dmgNumDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            Instantiate(damageBurst, hitArea.position, hitArea.rotation);

            var clone = (GameObject) Instantiate(dmgNumDisplay, hitArea.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().dmgNumber = damageToGive;
        }
    }
}
