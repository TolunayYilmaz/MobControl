using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform transformTarget;
   public int Health;
    // Start is called before the first frame update
    public int i;
    Vector3 look;
    bool targetDestroy;
    PlayerController playerController;
    GameManager gameManager;
    TextMesh txtHealth;
    CameraFollow cameraFollow;
    Animator animator;
    [SerializeField] ParticleSystem explosionParticle;
    ScoreManager scoreManager;
    private int score;
    private void Awake()
    {
        score = Health;
       playerController=GameObject.FindGameObjectWithTag("Players").GetComponent<PlayerController>();
        targetDestroy = true;
        transformTarget = transform;
        gameManager=FindObjectOfType<GameManager>();
        txtHealth = transform.GetChild(0).GetComponentInChildren<TextMesh>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        animator = GetComponentInChildren<Animator>();
        explosionParticle.transform.localPosition = transform.GetChild(0).transform.localPosition;
        scoreManager=FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        txtHealth.text = Health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Players"))
       {
       
        playerController.TargetQueue++;
        playerController.battle = false;
        playerController.run = true;
        look = playerController.TargetSelect().position;
        playerController.TargetSelect().gameObject.GetComponentInChildren<SpawnManager>().spawnTarget = true;
       }
        if (other.CompareTag("Player"))
        {
            Shot(); 
            if (Health == 0)
            {
                playerController.battle = true;
                playerController.run = false;
                gameManager.DestroyAll();
            }
            Destroy(other.gameObject);
          
            if (Health <= 0)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                scoreManager.ScoreUpdate(score);
                scoreManager.HighScoreUpdate();
                if (playerController.targets.Length - 1 == playerController.TargetQueue)
                {
                    gameManager.IsStop = true;
                    Debug.Log("Next LEvel");
                    gameManager.NextLevelPanel();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Players"))
        {
            
            Vector3 targetDirection = look - other.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            other.transform.rotation = Quaternion.Lerp(other.transform.rotation, targetRotation, Time.deltaTime * 15);
           
            if (other.transform.rotation == targetRotation&&targetDestroy)
            {
                
                targetDestroy = false;
                gameObject.SetActive(false);
            }
           
        }
    }

    
    public int Shot()
    {
       StartCoroutine(Animator());
       return Health--;
    }

    private IEnumerator Animator()
    {
        explosionParticle.Play();
        animator.SetBool("TargetAnim",true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("TargetAnim",false);
        explosionParticle.Stop();
        
    }

}
