using UnityEngine;
using System.Collections;

public class JumpOrb : MonoBehaviour
{
    SpriteRenderer m_renderer;

    // Use this for initialization
    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			if(PlayerController_m.swipedUp == true)
			{
            StartCoroutine(AddBounce());
            //As destroying the object here would terminate the 'AddBounce' function, the sprite renderer is turned off instead
            m_renderer.enabled = false;
			}
        }
    }

    IEnumerator AddBounce()
    {
        GameObject player = GameObject.Find("Furry");
        PlayerController_m playermass = player.GetComponent<PlayerController_m>();

        playermass.GetComponent<Rigidbody2D>().mass = 0.9f;
        yield return new WaitForSeconds(10);
        //Now the object is no longer needed, it is deleted
        Destroy(gameObject);
        playermass.GetComponent<Rigidbody2D>().mass = 1.5f;
    }
}
