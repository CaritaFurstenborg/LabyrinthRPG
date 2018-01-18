using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stats health; 
    [SerializeField]
    private Stats resource;

    [SerializeField]
    private GameObject[] spellPrefabs;

    [SerializeField]
    private BlockLOS[] blocks;      // blocks that disables shooting if not facing the target

    [SerializeField]
    private Transform[] exitPoints; // Exitpoints for ranged attacks! Implementation 11/48
    private int exitIndex;

    public Transform MyTarget { get; set; }

    private float initialHealth = 50;
    private float initialResource = 0;
    private float maxResource = 100;

    private float gcd = 1.5f; // NOT IMPLEMENTED


    // Use this for initialization
    protected override void Start () {

        health.Initialize(initialHealth, initialHealth);
        resource.Initialize(initialResource, maxResource);

        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();

        base.Update();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.Alpha1)) // TESTING & DEBUGGING
        {
            health.MyCurrentValue -= 1;
            resource.MyCurrentValue -= 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            health.MyCurrentValue += 5;
            resource.MyCurrentValue += 5;
        }                                   // TESTING & DEBUGGING

        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BlockLOS();

            if (MyTarget != null && !isAttacking && InLineOfSight())
            {
                attackRoutine = StartCoroutine(Attack());
            }            
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        animator.SetBool("isAttacking", isAttacking);

        CastSpell();

        yield return new WaitForSeconds(0.2f); //Attacktime

        StopAttack();
    }

    public void CastSpell()
    {
        Instantiate(spellPrefabs[0], transform.position, Quaternion.identity);
    }

    private bool InLineOfSight()
    {
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

        if(hit.collider == null) // if raycast does not hit then los is clear
        {
            return true;
        }

        return false; // if raycast hit somthing then los is blocked
    }

    private void BlockLOS()
    {
        foreach(BlockLOS b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
}
