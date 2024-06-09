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
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        movingleft = true;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyWalking();
        CheckCollision();
    }

    void EnemyWalking()
    {
        if (movingleft)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y); //moving left
        }
        else
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y); //moving rignt
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
        Debug.Log("Change Direction");
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
}
