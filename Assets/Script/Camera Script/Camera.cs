using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject Player;
    public float dropY;
    public float upY;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {                                                                  //set the camere regular height
        transform.position = new Vector3 (Player.transform.position.x,Player.transform.position.y + upY,transform.position.z);
        
        //limit the camera dropping
        if (transform.position.y <= dropY)
        {
            transform.position = new Vector3(Player.transform.position.x, dropY , transform.position.z);
        }
    }
}
