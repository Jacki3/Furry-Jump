using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour
{
    /// <summary>
    /// Class which scrolls the background 
    /// Attached to player controller which determines when the player is moving 
    /// </summary>

    public float m_speed;
    public static Scroll current;

    float pos = 0;
	float posY = 0;
    Renderer m_renderer;

    // Use this for initialization
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        current = this;
    }

    // Function which states if player is moving, scroll
    public void Update()
    {
		GameObject player = GameObject.Find("Furry");
		PlayerController_m playerPref= player.GetComponent<PlayerController_m>();

		pos += playerPref.rb2d.velocity.x / m_speed;
		posY += playerPref.rb2d.velocity.y / m_speed;
		m_renderer.material.mainTextureOffset = new Vector2 (pos, 0);
    }
}
