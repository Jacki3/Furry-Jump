using UnityEngine;
using System.Collections;

public class Hazards : MonoBehaviour
{
    Rigidbody2D m_rbd;
    CircleCollider2D m_bcol;

    private float m_damage;

    // Use this for initialization
    void Start()
    {
        m_rbd = GetComponent<Rigidbody2D>();
        m_bcol = GetComponent<CircleCollider2D>();

    }

    /// <summary>
    /// Handles collision for the hazards: currently this is just the saw 
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject m_controller = GameObject.Find("Controller");
        Controller m_DamageTaken = m_controller.GetComponent<Controller>();

        if (other.gameObject.CompareTag("Player"))
        {
            m_damage = 0.5f;
            //Updating player health with the damage the hazard gives
            m_DamageTaken.HealthUpdate(m_damage);
            m_rbd.isKinematic = false;
            m_bcol.isTrigger = true;

        }
    }

}
