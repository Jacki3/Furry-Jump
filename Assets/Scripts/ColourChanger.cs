using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ColourChanger : MonoBehaviour
{
    /// <summary>
    /// Setting different colours of the character and attaching the induvidual functions to buttons within character selection scene
    /// </summary>
    public static Color m_colour;

    Color pink;
    AsyncOperation m_levelLoad;

    public void ChangeToBlack()
    {
        m_colour = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(Load());
    }

    public void ChangeToBlue()
    {
        m_colour = new Color(0f, 0f, 255f, 1f);
        StartCoroutine(Load());
    }

    public void ChangeToRed()
    {
        m_colour = new Color(1f, 0, 0, 1f);
        StartCoroutine(Load());
    }

    public void ChangeToPink()
    {
        pink = Color.magenta;
        m_colour = pink;
        StartCoroutine(Load());
    }

    public void ChangeToGreen()
    {
        m_colour = new Color(0f, 255f, 0f, 1f);
        StartCoroutine(Load());
    }

    public void ChangeToNormal()
    {
        m_colour = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(Load());
    }

    //Loading the scene before playing it
    IEnumerator Load()
    {
        BoostOrb.m_orbCount = 0;

        m_levelLoad = SceneManager.LoadSceneAsync("Dev");
        m_levelLoad.allowSceneActivation = true;
        yield return m_levelLoad;
    }
}
