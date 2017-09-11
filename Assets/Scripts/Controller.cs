using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Controller : MonoBehaviour
{
    /// <summary>
    /// Handles large general operations such as updating player health and score
    /// Also handles writing text to the canvas
    /// </summary>

    public Text m_boostText;
    public Text m_boostNotification;
    public Text m_scoreText;
    public Text m_highScoreText;
    public GameObject m_deathScreen;
    public GameObject boostBig;

    int score;
    int highScore;

    // Use this for initialization
    void Start()
    {
        //Turning vSync off (for desktop use)
        QualitySettings.vSyncCount = 0;
        //Setting frame rate to 30 frames per second
        //Application.targetFrameRate = 60;
        SetBoostText();
        UpdateScore();
        HighScore();
        //Initializing high score
        highScore = PlayerPrefs.GetInt("HighScore1", 0);
    }

    // Update is called once per frame
    public void Update()
    {
        SetBoostText();
        HighScore();
        m_highScoreText.text = "High Score: " + highScore;
    }
    //Displays the 'Boost!' text when the boost has been activated
    private void SetBoostText()
    {
        m_boostText.text = "Boost: " + (BoostOrb.m_orbCount).ToString() + "/5";
        if (BoostOrb.m_orbCount == BoostOrb.m_maxorbCount)
        {
            StartCoroutine(BoostBig());
        }
    }
    /// <summary>
    /// This function writes the current score to the screen as an integer 
    /// Currently score is calculated as distance travelled plus score from destroying enemies
    /// </summary>
    public void UpdateScore()
    {
        var player = GameObject.Find("Furry");
        var playerHealth = player.GetComponent<PlayerController_m>();
        m_scoreText.text = "SCORE: " + (int)playerHealth.m_distance;

        score = (int)playerHealth.m_distance;
    }

    /// <summary>
    /// Function which calculates the total score (distance plus additional score) 
    /// This is given to the UpdateScore function above 
    /// </summary>
    /// <param name="value"></param>
    public void UpdateKillScore(int value)
    {
        var player = GameObject.Find("Furry");
        var playerHealth = player.GetComponent<PlayerController_m>();
        playerHealth.m_distance += value;
        UpdateScore();
    }
    //Displays the boost text for 2 seconds 
    private IEnumerator BoostBig()
    {
        // m_boostNotification.text = "BOOST!";
        boostBig.SetActive((true));
		if(PlayerController_m.maxBoost == true)
		{
        yield return new WaitForSeconds(1f);
        boostBig.SetActive((false));// m_boostNotification.text = "";
		}
    }
    /// <summary>
    /// Function which updates player health with any damage taken and then displays this as an image (health bar)
    /// </summary>
    /// <param name="damage"></param>
    public void HealthUpdate(float damage)
    {
        GameObject player = GameObject.Find("Furry");
        PlayerController_m playerHealth = player.GetComponent<PlayerController_m>();

        GameObject saw = GameObject.FindGameObjectWithTag("Enemy");
        Hazards hazard = saw.GetComponent<Hazards>();

        GameObject alien = GameObject.FindGameObjectWithTag("Alien");
        Enemy enemy = alien.GetComponent<Enemy>();

        playerHealth.currentHealth = playerHealth.currentHealth - damage;

        GameObject healthBar = GameObject.Find("HealthBar");
        Image healthBarImage = healthBar.GetComponent<Image>();

        //Calculating how much health fills the bar
        healthBarImage.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;

        //If player dies
        if (playerHealth.currentHealth <= 0)
        {
            //save high score
            HighScore();
            StartCoroutine(DeathOverlay());
        }
    }
    /// <summary>
    /// Function used for when a health orb is collected
    /// </summary>
    public void AddHealth()
    {
        GameObject player = GameObject.Find("Furry");
        PlayerController_m playerHealth = player.GetComponent<PlayerController_m>();
        GameObject healthBar = GameObject.Find("HealthBar");
        Image healthBarImage = healthBar.GetComponent<Image>();
        //Ensures player health never exceeds maximum value
        if (playerHealth.currentHealth + 1 > 10)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
            healthBarImage.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;

        }
        //Adds health if an orb is collceted
        else if (playerHealth.currentHealth < 10)
        {
            playerHealth.currentHealth = playerHealth.currentHealth + 1.5f;
            healthBarImage.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
        }
    }
    //Saving high sore as an integer
    public void HighScore()
    {
        if (score > highScore)

        {
            PlayerPrefs.SetInt("HighScore1", score);
        }
    }
    //Displays game over screen for 3 seconds then loads game over scene
    IEnumerator DeathOverlay()
    {
        m_deathScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
}