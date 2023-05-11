using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    Transform players;
    [SerializeField] private int powerUpVariable;

    private bool createClonePlayer = true;
    public bool CreateClonePlayer { get { return createClonePlayer; } set { createClonePlayer = value; } }  
    private string powerUp;
    private int powerUpNumberInput;
    private char signInput;

    private void Awake()
    {
        
       // players = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //PowerUp üstündeki yazýyý bulur ve metodlara atama yapar.
        powerUp = transform.GetChild(0).GetComponent<TextMesh>().text;
        powerUpNumberInput = System.Int32.Parse(powerUp.Substring(1));
        signInput= powerUp.ToLower()[0];
     
    }
    private void OnTriggerEnter(Collider other)
    {

        ClonePlayer(other);
    }

    /// <summary>
    /// Matematilsel iþlem yapýldýktan sonra aktarýlan sayý tekrarý yapýlýr ve üretilir.Players objesinin içine gönderilir.
    /// </summary>
    /// <param name="other"></param>
    void ClonePlayer(Collider other)
    {
        PowerUpController(1);
        if (other.CompareTag("Player")&&other.GetComponent<HumanControl>().cogal== true &&other != null && other.GetComponent<CapsuleCollider>() != null)
        {
           
                other.GetComponent<HumanControl>().cogal = false;
                for (int i = 1; i < powerUpVariable; i++)
                {
                    GameObject clone = Instantiate(other.gameObject, PlayerPosition(other.transform.position, 1.2f), Quaternion.identity);
                    clone.GetComponent<HumanControl>().cogal = false;
                }
            
        }
    }

    public Vector3 PlayerPosition(Vector3 targetPosition, float radius)
    {
        Vector3 pos = Random.insideUnitSphere * radius;
        Vector3 newPos = targetPosition + pos;
        newPos.y = 1f;
        return newPos;
    }

    /// <summary>
    /// Üretimi gecitrimek için oyuncu deneyimini arttýmrak için yapýldý.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    IEnumerator Basla(Collider other)
    {
        yield return new WaitForSeconds(0.05f);
        ClonePlayer(other);
    }


    /// <summary>
    /// Çarpma ve toplama iþlemi yapar.
    /// </summary>
    void PowerUpController(int multiply)
    {
  
        if(signInput == 'x')
        {
           
            Debug.Log(multiply);
            if (multiply == 1)
            {
                powerUpVariable = powerUpNumberInput * multiply;
            }
            else
            {
                powerUpVariable = (powerUpNumberInput * multiply)-multiply+1;
            }
          
        }
      
    }

}
