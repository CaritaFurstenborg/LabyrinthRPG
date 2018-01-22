using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    
    [SerializeField]
    private Stats resource;
    
    [SerializeField]
    private BlockLOS[] blocks;      // blocks that disables shooting if not facing the target

    [SerializeField]
    private Transform[] exitPoints; 
    private int exitIndex = 2;

    private SpellBook spellBook;
    
    private float initialResource = 0;
    private float maxResource = 100;
    

    // Use this for initialization
    protected override void Start () {        

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
        MyDirection = Vector2.zero;

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
            MyDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            MyDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            MyDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            MyDirection += Vector2.right;
        }
        if(isMoving)
        {
            StopAttack();
        }
    }

    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;
        Spell newSpell = spellBook.CastSpell(spellIndex);

        if(!newSpell.IsMele)
        {
            IsMele = false; //this is Character class IsMele bool
        }
        else if(newSpell.IsMele)
        {
            IsMele = true; //this is Character class IsMele bool
        }
        
        IsAttacking = true;

        MyAnimator.SetBool("isAttacking", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime); //Attacktime

        if (currentTarget != null && InLineOfSight() && !newSpell.IsMele) // Mele spell prefabs not working (remove last check)
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            
            s.Initialize(currentTarget, newSpell.MyDamage, IsMele, transform); //spells target = players target with set damage and type of spell
        }
        else if (currentTarget != null && InLineOfSight() && newSpell.IsMele)
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

            s.Initialize(currentTarget, newSpell.MyDamage, IsMele, transform); //spells target = players target with set damage and type of spell
        }

            StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        BlockLOS();

        if (MyTarget != null && MyTarget.GetComponentInParent<Enemy>().IsAlive && !IsAttacking && !isMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }        
    }

    private bool InLineOfSight()
    {
        if(MyTarget != null)
        {
            //Calculation for target's direction
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
            // Raycast in the direction of target
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null) // if raycast does not hit then los is clear
            {
                return true;
            }
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

    public void StopAttack()
    {
        spellBook.StopCasting();

        IsAttacking = false;
        MyAnimator.SetBool("isAttacking", IsAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }
}
