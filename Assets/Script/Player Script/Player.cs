using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


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
    private int coin;

    private TMP_Text ScoreText;
    private AudioSource CoinPickUpSound;

    public string CurrentScene;
    public string NextScene;
    public AudioSource Win;
    private bool Stun;
    public AudioSource DieSound;
    public AudioSource JumpSound;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CoinPickUpSound = GetComponent<AudioSource>();
        ScoreText = GameObject.Find("ScoreText (TMP)").GetComponent<TMP_Text>();
        //Win = GetComponent<AudioSource>();
        Stun = false;

        
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

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

        PlayerWalk(Stun);
        OnGrounded();
        PlayerJump(Stun);
        Die();
    }

    //private void FixedUpdate()
    //{
        
    //}
    void PlayerWalk(bool a)
    {
        if (!a)
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
        else if (a)
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }
    }

    void ChangeDirection(int direction)
    {
        //if (direction == 0)
        //{ direction = 1; }

        Vector3 temp = transform.localScale;
        if (direction == 1)
        {
            temp.x = Mathf.Abs(temp.x);
        }
        else if (direction == -1)
        {
            temp.x = -Mathf.Abs(temp.x);
        }

        transform.localScale = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tri"))
        {
            CoinPickUpSound.Play();
            Debug.Log("is a triangle");
            coin = coin +1;
            Debug.Log("Coins: " + coin);
            ScoreText.text = "X " + coin.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.name == "castle" || collision.name == "Flag")
        {
            StartCoroutine(Wining(2f));
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
        isgrounded = Physics2D.CircleCast(obj.position,0.2f, Vector2.down, 0.1f,groundlayer);
        Debug.DrawRay(obj.position, Vector3.down, Color.red, 0.1f);

        if (isgrounded && jumped)
        {
            jumped = false;

            anim.SetBool("Jump", false);
        }
        
    }

    private void PlayerJump(bool a)
    {
        if (!a)
        {
            if (isgrounded)
            {
                //Debug.Log("is ground");
                if (Input.GetKey(KeyCode.Space))
                {
                    jumped = true;
                    JumpSound.Play();

                    myBody.velocity = new Vector2(myBody.velocity.x, jumppower);
                    anim.SetBool("Jump", true);
                }
            }
        }
    }

    private void Die()
    {
        if (transform.position.y < -14)
        {
            StartCoroutine(Dying(2f));
        }
    }

    IEnumerator Wining(float second)
    {
        Debug.Log("time: " + Time.time);
        GameObject.Find("Background Music").SetActive(false);
        Win.Play();
        Stun = true;
        anim.SetInteger("Speed",0);
        

        yield return new WaitForSeconds(second);
        Debug.Log("time: " + Time.time);
        SceneManager.LoadScene(NextScene);
    }

    public IEnumerator Dying(float second)
    {
        this.Stun = true;
        GameObject.Find("Background Music").SetActive(false);
        anim.Play("Player Die");
        DieSound.Play();
        

        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(CurrentScene);   
    }

}
