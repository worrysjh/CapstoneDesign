using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public Rigidbody playerRidbody;
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) == true){
            //  위 방향키
            playerRidbody.AddForce(0f, 0f, speed);
        }
        if(Input.GetKey(KeyCode.DownArrow) == true){
            //  아래 방향키
            playerRidbody.AddForce(0f, 0f, -speed);
        }
        if(Input.GetKey(KeyCode.RightArrow) == true){
            //  오른쪽 방향키
            playerRidbody.AddForce(speed, 0f, 0f);
        }
        if(Input.GetKey(KeyCode.LeftArrow) == true){
            //  위 방향키
            playerRidbody.AddForce(-speed, 0f, 0f);
        }
    }

    public void Die(){
        gameObject.SetActive(false);
    }
}
