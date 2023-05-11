using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    public bool run = true;
    public bool battle = false;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] public Transform[] targets;
    private bool isLeft = true;
    private bool isRight = true;
    public bool IsLeft { set { isLeft = value; } }
    public bool IsRight { set { isRight = value; } }

    private int targetQueue;
    public int TargetQueue { get { return targetQueue; } set { targetQueue = value; } }
    public int Health;
    GameManager gameManager;

    Animator animCar;
    private void Awake()
    {
        targetQueue = 0;
        animCar = GameObject.Find("Car").GetComponent<Animator>();
        Debug.Log(targets.Length+"sayý");
        gameManager=FindObjectOfType<GameManager>();
    }
    private void LateUpdate()
    {
        Move();

    }

    public Transform TargetSelect()
    {
     return targets[targetQueue];
    }

    private void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        if (run)
        {
            //  transform.position += Vector3.forward * speed * Time.deltaTime;
            animCar.SetBool("go",false);
            if (isRight && horizontalInput > 0)
            {
                transform.localPosition += transform.right * horizontalInput * speed;

                isLeft = true;
            }
            //Left
            else if (isLeft && horizontalInput < 0)
            {
                transform.localPosition += transform.right * horizontalInput * speed;
                isRight = true;
            }
        }
        else if (battle&& targets.Length-1>targetQueue)
        {
            animCar.SetBool("go", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetSelect().position.x, transform.localPosition.y, TargetSelect().position.z), (speed / 2) * Time.deltaTime);


        }


        

    }
    private void OnDestroy()
    {
        gameManager.DestroyAll();
    }
    public int Shot()
    {
        return Health--;
    }

}
