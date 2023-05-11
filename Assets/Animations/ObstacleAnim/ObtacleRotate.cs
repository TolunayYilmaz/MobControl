using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtacleRotate : MonoBehaviour
{
    [SerializeField] int speed;
    PlayerController playerController;

    // Update is called once per frame
    private void Awake()
    {
        playerController=FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if (!playerController.battle)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
      
    }
}
