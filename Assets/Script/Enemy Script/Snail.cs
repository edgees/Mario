using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;
    public Transform obj;
    public LayerMask groundlayer;
    public float speed;
    private bool movingleft;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        movingleft = true;
    }

    // Update is called once per frame
    void Update()
    {
        SnailMoving();
        CheckCollision();
    }

    void SnailMoving()
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
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }


 
        transform.localScale = tempScale;
    }
}
