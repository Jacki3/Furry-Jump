using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    public GameObject m_deathScreen;

    /// <summary>
    /// Function in which if the player enters the collision zone, the player is dead and if the score beat the current high score then it is updated
    /// </summary>
    /// <param name="other"></param>

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GameObject controller = GameObject.Find("Controller");
            Controller highScore = controller.GetComponent<Controller>();
            highScore.HighScore();
            StartCoroutine(DeathOverlay());
        }
    }
    //Displays game over screen for 3 seconds then loads the game over scene 
    IEnumerator DeathOverlay()
    {
        m_deathScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
}

    

