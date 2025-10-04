
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] bool isPaused = false;
    [SerializeField] GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pausing game");
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
                GameResume();
            else
                GamePause();
        }
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }
}
