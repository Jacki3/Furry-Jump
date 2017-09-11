using UnityEngine;
using System.Collections;

public class BouncePlatform : MonoBehaviour
{
    /// <summary>
    /// Class which adds the jump effect to platforms 
    /// </summary>

    public float m_bounce = 20f;
    public GameObject spawner;

    bool hasSpawned = false;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn");
    }
    /// <summary>
    /// If the player enters the collision area and is falling then the velocity of player is set to 0 then force is added in an upward vector
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * m_bounce, ForceMode2D.Impulse);
            }
        }
        //Checking to see if each platform has spawned
        if (!hasSpawned)
        {
            hasSpawned = true;
        }
    }
}