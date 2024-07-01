using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    public Transform obj;
    private Rigidbody2D rb2d;
    private Animator anim;
    public LayerMask groundlayer;
    public float speed = 1f;
    private bool movingleft;

    public Transform left;
    public Transform right;
    public Transform top;
    public LayerMask player;
    private bool stun;
    private bool oncol;
    public GameObject DamageZone;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        movingleft = true;
        stun = false;
    }

    // Update is called once per frame
    void Update()
    {
        detectplayer();
        EnemyWalking(stun);
        CheckCollision(stun);   
    }

    void EnemyWalking(bool a)
    {
        if (a)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        else if (movingleft)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y); //moving left
        }
        else
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y); //moving rignt
        }

    }

    void CheckCollision(bool stun)
    {
        if (!stun)
        {
            if (!Physics2D.Raycast(obj.position, Vector2.down, 0.5f, groundlayer) || oncol)
            {
                ChangeDirection();
                oncol = false;
            }
        }
    }

    void ChangeDirection()
    {
        //Debug.Log("Change Direction");
        movingleft = !movingleft;
        Vector3 temp = transform.localScale;

        if (movingleft) 
        { 
            temp.x = Mathf.Abs(temp.x) ; //look left
        } 
        else if (!movingleft)
        { 
            temp.x = -Mathf.Abs(temp.x); //look right
        }

        transform.localScale = temp;
    }

    private void detectplayer()
    {
        if (!stun)
        {
            if (Physics2D.CircleCast(top.position, 0.2f, Vector2.up, 0.1f, player))
            {
                die();
            }

            Debug.DrawRay(right.position, Vector2.right, Color.blue, 0.5f);
            if (Physics2D.Raycast(right.position, Vector2.right, 0.5f, player))
            {
                Debug.Log("damage (right)");
            }

            Debug.DrawRay(left.position, Vector2.left, Color.blue, 0.5f);
            if (Physics2D.Raycast(left.position, Vector2.left, 0.5f, player))
            {
                Debug.Log("damage (left)");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            oncol = true;
        } 
    }

    void die()
    {
        stun = true;
        Destroy(DamageZone);
        anim.SetBool("die",true);
        Destroy(gameObject,0.5f);
    }
}
