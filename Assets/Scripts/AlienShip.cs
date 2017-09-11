using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AlienShip : MonoBehaviour
{
    /// <summary>
    /// Class which handles collision on the enemy space ship
    /// </summary>

    public int m_scoreValue;

    private float damage;

    /// <summary>
    /// Function which determines how much damage the enemy will take from player health if it collides with the player
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject controller = GameObject.Find("Controller");
        Controller DamageTaken = controller.GetComponent<Controller>();

        if (other.gameObject.CompareTag("Player"))
        {
            damage = 1f;
            DamageTaken.HealthUpdate(damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject controller = GameObject.Find("Controller");
        Controller DamageTaken = controller.GetComponent<Controller>();
        GameObject player = GameObject.Find("Furry");
        PlayerController_m playerHealth = player.GetComponent<PlayerController_m>();

        var name = other.gameObject.name;
        //Fly is the projectile the player shoots
        if (name == "Fly(Clone)")
        {
            m_scoreValue = 50;
            playerHealth.Score(50);
            DamageTaken.UpdateScore();
            Destroy(gameObject);
        }
    }
}