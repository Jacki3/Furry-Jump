using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScroll : MonoBehaviour
{
    /// <summary>
    /// Handles the parallax scrolling of the background image 
    /// </summary>

    public float m_speed;
    public static MainMenuScroll s_current;
    [HideInInspector]

    float m_pos = 0;
    Renderer m_renderer;

    // Use this for initialization
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        s_current = this;
    }

    // Update which determines whether or not the player is moving right and scrolls the image accordingly
    public void Update()
    {
        m_pos += m_speed;
        if (m_pos > 1.0f)
        {
            m_pos -= 1.0f;
        }
        m_renderer.material.mainTextureOffset = new Vector2(m_pos, 0);

    }
    //Functions which are attached to buttons on both the main and pasuse menus
    public void PlayGame()
    {
        SceneManager.LoadScene("HeroPicker");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
