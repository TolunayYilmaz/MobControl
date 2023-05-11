using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    PlayerController playerController;
    void Start()
    {
        anim =GetComponent<Animator>();
        playerController=GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!playerController.battle)
        {
            anim.SetBool("Fire", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("Fire", false);
        }
    }
}
