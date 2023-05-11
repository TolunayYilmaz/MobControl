using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    PlayerController playerController;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController =GameObject.Find("Players").GetComponent<PlayerController>();
        InvokeRepeating(nameof(AnimControl), 5f, 2f);
        animator.SetBool("Power", true);
    }

    void AnimControl()
    {
        if (playerController.battle)
        {
            animator.SetBool("Power", false);
            Debug.Log("Çalýþtrý");
        }
        else
        {
            animator.SetBool("Power", true);
        }
        

    }
}
