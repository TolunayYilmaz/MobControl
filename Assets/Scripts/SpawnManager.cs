using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private int countspawn;
    [SerializeField] private int maxSpawnCount;
    PlayerController playerController;
    float timer = 0f; // baþlangýç zamaný
    float interval = 0f;
    public bool spawnTarget;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();    
        InvokeRepeating(nameof(CreateEnemy), 1f, spawnSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && !transform.parent.CompareTag("Target")&&!playerController.battle)
        {

            timer += Time.deltaTime;
            if (timer >= interval)
            {
                interval = 0.2f;
                // Interval'a ulaþýldýðýnda metodunuza çaðrý yapýn
                Create();
                timer = 0f; // Zamanlayýcýyý sýfýrla
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)&&!playerController.battle)
        {
            interval = 0f;
            timer = 0f; // Tuþ býrakýldýysa zamanlayýcýyý sýfýrla
        }
    }

    void Create()
    {

        GameObject player = Instantiate(playerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        player.transform.localRotation = transform.rotation;
    }
    private void CreateEnemy()
    {
       
        if (transform.parent.CompareTag("Target") && maxSpawnCount >= 0&&spawnTarget)
        {
            maxSpawnCount--;
            for (int i = 1; i < countspawn; i++)
            {
                GameObject clone = Instantiate(playerPrefab.gameObject,
                    PlayerPosition(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1.2f),
                    Quaternion.identity);

                // GameObject player = Instantiate(playerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                clone.transform.localRotation = transform.rotation;
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

}
