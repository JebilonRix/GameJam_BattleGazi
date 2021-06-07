using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    //Bunu canvas'ýn içine koy
    private static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void ToMainMenu(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
    public void ExitDekstop()
    {
        Application.Quit();
    }
}