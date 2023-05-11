using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool stop;
    public bool IsStop { get { return stop; } set { stop = value; } }
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextLevelPanel;
    private int sceneIndex;
    public int SceneIndex { get { return sceneIndex; } }
    void Start()
    {
        stop = false;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Restart()
    {

        SceneManager.LoadScene(sceneIndex);

    }
    public void NextLevel()
    {
        if (sceneIndex + 1 <= SceneManager.sceneCountInBuildSettings - 1)
        {

            SceneManager.LoadScene(sceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);

        }


    }
    public void NextLevelPanel()
    {

        nextLevelPanel.SetActive(true);
       // gameOver = true;
     
    }
    public void GameOverPanel()
    {

        gameOverPanel.SetActive(true);
       

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void DestroyAll()
    {
        ObjectDestory("Enemy");
        ObjectDestory("Player");
    }
    private void ObjectDestory(string objectName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(objectName);
        foreach (GameObject item in objects)
        {
            Destroy(item);
        }
    }
}
