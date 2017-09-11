using UnityEngine;
using System.Collections;

public class HealthOrb : MonoBehaviour
{
    /// <summary>
    /// Class which handles collision on the red health orbs 
    /// Once player collides with one, a function is called which adds a small number to the player's health
    /// </summary>
    /// <param name="other"></param>

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject m_controller = GameObject.Find("Controller");
        Controller m_HealthPickup = m_controller.GetComponent<Controller>();

        if (other.gameObject.CompareTag("Player"))
        {
            m_HealthPickup.AddHealth();
            Destroy(gameObject);
        }

    }
}
