using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    public GameObject stopemenu;
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip clicksound;


    public void Click()

    {
        if (audioSource != null && clicksound != null)
        {
            audioSource.PlayOneShot(clicksound,0.5f);
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        stopemenu.SetActive(true);
    }


    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        stopemenu.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoChooseMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChooseMap");
    }
    public void GoMap1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map1");
    }
    public void GoEnemyTree()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("EnemyTree");
    }
    public void GoSkillTree()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SkillTree");
    }
    public void GoMode()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mode");
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Credit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credit");
    }
}