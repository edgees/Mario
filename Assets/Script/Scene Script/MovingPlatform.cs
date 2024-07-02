using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 1;
    private bool facingright;
    public LayerMask layer;
    public GameObject plat;
    private bool detect = true;
    private GameObject FindChildren (GameObject ParentGameObject, string GameObjectName)
    {
        for (int i = 0; i < ParentGameObject.transform.childCount ; i++)
        {
            if (ParentGameObject.transform.GetChild(i).gameObject.name == GameObjectName)
            {
                return ParentGameObject.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        facingright = true;
        FindChildren(gameObject, "start").transform.SetParent(plat.transform);
        FindChildren(gameObject, "end").transform.SetParent(plat.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMovement();
        DetectEdge(detect);
    }

    void PlatformMovement()
    {
        if (facingright)
        {
            transform.position = Vector2.MoveTowards(transform.position,FindChildren(plat,"end").transform.position,Time.deltaTime * speed);
        }
        else if (!facingright)
        {
            transform.position = Vector2.MoveTowards(transform.position,FindChildren(plat, "start").transform.position, Time.deltaTime * speed);
        }  
    }

    void DetectEdge(bool a)
    { 
        if (a)
        {
            if (Physics2D.Raycast(gameObject.transform.position, Vector2.right, 0.5f, layer) || Physics2D.Raycast(gameObject.transform.position, Vector2.left, 0.5f, layer))
            {
                Debug.Log("change direction");
                facingright = !facingright;
                a = false; //a is false, but it won't affect the boolean outside
            }
        }
    }


    void ChangeDirection()
    {
        Vector3 temp = transform.localScale;
        if (facingright)
        {
            temp.x = Mathf.Abs(temp.x);
        }
        else
        {
            temp.x = -Mathf.Abs(temp.x);
        }
        transform.localScale = temp;
    }

    //set player in the gameobject so player stick on it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
