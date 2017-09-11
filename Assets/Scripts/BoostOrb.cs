using UnityEngine;

public class BoostOrb : MonoBehaviour
{
    //As variables need to accesed by other scripts
    [HideInInspector]
    public  static int m_orbCount;
    public static int m_maxorbCount = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_orbCount < m_maxorbCount)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                m_orbCount++;
                Destroy(gameObject);
            }
        }
    }
}