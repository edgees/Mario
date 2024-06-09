using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;

    public float speed = 1f;
    public float jumppower = 10f;

    public Transform obj;
    public LayerMask groundlayer;
    private bool onground;

    private bool jumped;
    private bool isgrounded;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Physics2D.Raycast(obj.position,Vector2.down,0.5f,groundlayer))
        //{
        //    Debug.Log("is ground");
        //    onground = true;
        //}
        //else
        //{
        //    onground = false;
        //}

        OnGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
        
    }
    void PlayerWalk()
    {
        float h = Input.GetAxis("Horizontal");
        myBody.velocity = new Vector2(h * speed, myBody.velocity.y);
        //ChangeDirection((int)h);
        if (h > 0)
        {
            //myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            //myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        //if (onground == true && Input.GetKeyDown(KeyCode.Space))
        //{
        //    myBody.velocity = new Vector2(myBody.velocity.x, jumppower);
        //}

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        //if (direction == 0)
        //{ direction = 1; }

        Vector3 temp = transform.localScale;
        temp.x = direction;
        transform.localScale = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tri"))
        {
            Debug.Log("is a triangle");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Square")
        {
            Debug.Log("is a square");
        }
    }

    private void OnGrounded()
    {
                                     //gameobject, arrow position, arrow range, target layer
        isgrounded = Physics2D.Raycast(obj.position, Vector2.down, 0.1f,groundlayer);

        if (isgrounded && jumped)
        {
            jumped = false;

            anim.SetBool("Jump", false);
        }
        
    }

    private void PlayerJump()
    {
        if (isgrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumped = true;

                myBody.velocity = new Vector2(myBody.velocity.x, jumppower);
                anim.SetBool("Jump", true);
            }
        }
    }
}
