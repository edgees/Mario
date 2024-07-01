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
    private bool oncol;
    public bool detectplayer;
    private GameObject player;
    public GameObject DamageZone;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        movingleft = true;
        stun = false;
        
        Left = left_coll.transform;
        Right = right_coll.transform;

        oncol = false;
        detectplayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        DetectPlayer();
        SnailMoving(stun);
        CheckCollision(stun);
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

    void CheckCollision(bool stun)
    {
        if (!stun)
        {
            if (!Physics2D.Raycast(obj.position, Vector2.down, 0.1f, groundlayer) || oncol)
            {
                ChangeDirection();
                oncol = false;
            }
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
        if (!stun)
        {
            if (Physics2D.CircleCast(top_coll.position, 0.2f, Vector2.up, 0.1f, Player))
            {
                Stun();
                //Die();
            }

            if (Physics2D.Raycast(left_coll.position, Vector2.left, 0.5f, Player))
            {
                Debug.Log("Damage (left)");
                //StartCoroutine(player.GetComponent<Player>().Dying(2f));
                //player.GetComponent<Player>().die = true;

            }

            if (Physics2D.Raycast(right_coll.position, Vector2.right, 0.5f, Player))
            {
                Debug.Log("Damage (right)");
                //StartCoroutine(player.GetComponent<Player>().Dying(2f));
                //player.GetComponent<Player>().die = true;
            }
        }

    }

    void Stun()
    {
        stun = true;
        Destroy(DamageZone) ;
        anim.SetBool("Stun", true);
    }

    void Die()
    {
        anim.SetBool("Die", true);
        Destroy(gameObject,1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            oncol = true;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        StartCoroutine(player.GetComponent<Player>().Dying(2f));
    //    }
    //}
}
