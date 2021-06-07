using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource MenuMusic;
    public float Seconds;
    public GameObject MainCanvas;
    public GameObject CreditsCanvas;

    private void Start()
    {
        MenuMusic = GetComponent<AudioSource>();
        MenuMusic.PlayDelayed(Seconds);
        MenuMusic.loop = true;

        MainCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }
    public void NewGame(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}