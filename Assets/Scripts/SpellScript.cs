using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float speed;

    public Transform MyTarget { get; private set; } //Property

    private Transform source;

    public bool IsMele { get; private set; } //check for mele

    private int damage;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Transform target, int damage, bool isMele, Transform source)
    {
        this.MyTarget = target;
        this.damage = damage;
        this.IsMele = isMele; // setting IsMele based on Character class 
        this.source = source;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            ChooseType();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HitBox" && other.transform == MyTarget)
        {
            Character c = other.GetComponentInParent<Character>();
            speed = 0;

            c.TakeDamage(damage, source);
            GetComponent<Animator>().SetTrigger("onHit");
            myRigidBody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }

    private void ChooseType()
    {
        if(!IsMele)
        {
            Vector2 direction = MyTarget.position - transform.position;

            myRigidBody.velocity = direction.normalized * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector2 direction = MyTarget.position - transform.position;

            myRigidBody.velocity = direction.normalized * 0;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
