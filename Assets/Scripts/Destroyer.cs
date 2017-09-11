using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    /// <summary>
    /// Literally, destroys all the game objects with the following tags
    /// </summary>
    /// <param name="other"></param>
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Alien")
            || other.gameObject.CompareTag("ground")
            || other.gameObject.CompareTag("Orb")
            || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
