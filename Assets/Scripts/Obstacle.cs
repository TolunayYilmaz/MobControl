using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float rotateSpeed;
    private PlayerController playerController;


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Players").GetComponent<PlayerController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        playerPosition(collision,false);
    

    }

    void playerPosition(Collision collision,bool isDirection)
    {    

        if (collision.gameObject.tag == "Players")
        {
           // playerController = collision.gameObject.GetComponentInParent<PlayerController>();
            if(gameObject.tag == "WallRight")
            {
                playerController.IsRight = isDirection;
                playerController.IsLeft = !isDirection;
            }
            else if (gameObject.tag == "WallLeft")
            {
                playerController.IsLeft = isDirection;
                playerController.IsRight = !isDirection;
            }
        }
        else
        {
            return;
            
        }
        
    }
 
}
