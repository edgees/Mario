using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;
    public Transform obj;

    public Transform left_coll;
    private Transform Left;
    public Transform right_coll;
    private Transform Right;
    public Transform top_coll;

    public LayerMask groundlayer;
    public float speed;
    private bool movingleft;
    public Transform snailRay;
    public LayerMask Player;
    private bool stun;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        movingleft = true;
        stun = false;
        
        Left = left_coll.transform;
        Right = right_coll.transform;

    }

    // Update is called once per frame
    void Update()
    {
        SnailMoving(stun);
        CheckCollision();
        DetectPlayer();
    }

    void SnailMoving(bool a)
    {
        if (a == false)
        {
            if (movingleft)
            {
                myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(speed, myBody.velocity.y);
            }
        }
        else if (a == true)
        {
            myBody.velocity = new Vector2 (0f, myBody.velocity.y);
        }
    }

    void CheckCollision()
    {
        if (!Physics2D.Raycast(obj.position, Vector2.down, 0.1f, groundlayer))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        //direction = direction * -1;
        //transform.localScale = new Vector2 (temp,transform.localScale.y);  
        movingleft = !movingleft;

        Vector3 tempScale = transform.localScale;

        if (movingleft) 
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            //left_coll.position = Left.position;
            //right_coll.position = Right.position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            //left_coll.position = Right.position;
            //right_coll.position = Left.position;
        }


 
        transform.localScale = tempScale;
    }

    private void DetectPlayer()
    {
        if (Physics2D.Raycast(left_coll.position, Vector2.left, 0.5f, Player))
        {
            Debug.Log("Damage (left)");
        }

        if (Physics2D.Raycast(right_coll.position, Vector2.right, 0.5f, Player))
        {
            Debug.Log("Damage (right)");
        }

        if (Physics2D.CircleCast(top_coll.position,0.2f,Vector2.up,0.1f,Player))
        {
            Stun();
            //Die();
        }
    }

    void Stun()
    {
        stun = true;
        anim.SetBool("Stun", true);
    }

    void Die()
    {
        anim.SetBool("Die", true);
        Destroy(gameObject,1f);
    }
}
