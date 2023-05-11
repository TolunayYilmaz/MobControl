using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanControl : MonoBehaviour
{
    [SerializeField] private GameObject targetCastle;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private GameObject fx;
    Rigidbody rb;
    bool run;
    Vector3 hedef;
    public bool cogal;
    private GameManager gameManager;    
    private void Awake()
    {
        cogal = true;
        rb = GetComponent<Rigidbody>();
        run = true;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (run)
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }
        else if (!run)
        {
            transform.position = Vector3.MoveTowards(transform.position, hedef, speed * Time.deltaTime);
            transform.LookAt(new Vector3(hedef.x, transform.localPosition.y, hedef.z));
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("TowerRotate")&&gameObject.CompareTag("Player"))
        {
            run = false;
            hedef = new Vector3(other.transform.parent.transform.localPosition.x,
                transform.localPosition.y, other.transform.parent.transform.localPosition.z);
       }
      
        if (other.CompareTag("PlayerCenter") && gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Düþman Yok Ol");
            Destroy(gameObject);
            PlayerController Player = other.GetComponentInParent<PlayerController>();
             Player.Shot();
            if (Player.Health <= 0)
            {
                Destroy(other.transform.parent.gameObject);
                gameManager.DestroyAll();
                gameManager.GameOverPanel();
            }
        }
        if (other.CompareTag("Enemy") && gameObject.CompareTag("Player")&&!other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        if (other.CompareTag("Obstacle") && gameObject.CompareTag("Player") && !other.CompareTag("PowerUp"))
        {
            Destroy(gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Top") && gameObject.CompareTag("Enemy"))
        {
            run = false;
            hedef = new Vector3(other.transform.parent.transform.localPosition.x,
                transform.localPosition.y, other.transform.parent.transform.localPosition.z);
        }
    }

    private void OnDestroy()
    {
        if (fx&&!gameManager.IsStop)
        {
            Instantiate(fx,transform.position,Quaternion.identity);
        }
    }

}
