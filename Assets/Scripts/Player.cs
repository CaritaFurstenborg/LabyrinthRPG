using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stats health; 
    [SerializeField]
    private Stats resource;
    
    [SerializeField]
    private BlockLOS[] blocks;      // blocks that disables shooting if not facing the target

    [SerializeField]
    private Transform[] exitPoints; 
    private int exitIndex = 2;

    private SpellBook spellBook;

    public Transform MyTarget { get; set; }

    private float initialHealth = 50;
    private float initialResource = 0;
    private float maxResource = 100;
    

    // Use this for initialization
    protected override void Start () {

        health.Initialize(initialHealth, initialHealth);
        resource.Initialize(initialResource, maxResource);

        spellBook = GetComponent<SpellBook>();

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

        if (Input.GetKey(KeyCode.Alpha8)) // TESTING & DEBUGGING
        {
            health.MyCurrentValue -= 1;
            resource.MyCurrentValue -= 1;
        }
        if (Input.GetKey(KeyCode.Alpha7))
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
    }

    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;
        Spell newSpell = spellBook.CastSpell(spellIndex);
        SpellScript scComp = newSpell.MySpellPrefab.GetComponent<SpellScript>();

        if(!newSpell.IsMele)
        {
            IsMele = false;
            scComp.IsMele = false;
        }
        else
        {
            IsMele = true;
            scComp.IsMele = true;
        }
        
        isAttacking = true;

        animator.SetBool("isAttacking", isAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime); //Attacktime

        if (currentTarget != null && InLineOfSight() && !newSpell.IsMele) // Mele spell prefabs not working (remove last check)
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            

            s.MyTarget = currentTarget; //spells target = players target
        }
        /*else if(MyTarget != null && InLineOfSight() && newSpell.IsMele)
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.IsMele = true;
            IsMele = true;

            s.MyTarget = MyTarget; //spells target = players target
        } */

        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        BlockLOS();

        if (MyTarget != null && !isAttacking && !isMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }        
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

    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
    }
}
