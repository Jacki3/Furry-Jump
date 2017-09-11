using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Class which handles functions for the buttons in the pause menu
    /// </summary>

    AsyncOperation m_operation;
    private bool isPaused;
    public GameObject m_pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            m_pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            m_pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

    }

    public void IsPaused()
    {
        isPaused = !isPaused;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Restart()
    {
        StartCoroutine(LoadLevel());
    }

    public void HeroPicker()
    {
        SceneManager.LoadScene("HeroPicker");
    }

    public void MenuExit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel()
    {
        m_operation = SceneManager.LoadSceneAsync("Dev");
        m_operation.allowSceneActivation = true;
        yield return m_operation;
    }
}
