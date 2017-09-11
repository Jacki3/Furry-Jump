using UnityEngine;

public class Enemy : MonoBehaviour {
    /// <summary>
    /// Handles enemy movement and collision - due to the fixedupdate movement this class consumes some processing power
    /// </summary>
    public Transform m_target;
    public Transform m_reverseTarget;
    public float m_speed = 2f;
    public int m_scoreValue;

    private bool dirRight = true;
    private float m_damage;

    /// <summary>
    /// Function which moves the enemy between two positions located on the platforms and flips the sprites on direction change
    /// </summary>
	void FixedUpdate () {

        float step = m_speed * Time.deltaTime;
        
        if (dirRight)
            transform.position = Vector3.MoveTowards(transform.position, m_target.position, step);
        //Direction left
        else
            transform.position = Vector3.MoveTowards(transform.position, m_reverseTarget.position, step);

        if (transform.position == m_target.position)
        {
            Flip();
            dirRight = false;
        }

        if (transform.position == m_reverseTarget.position)
        {
            Flip();
            dirRight = true;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject controller = GameObject.Find("Controller");
        Controller DamageTaken = controller.GetComponent<Controller>();

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            m_damage = 2f;
            //Update player's health with how much damage the enemy gives
            DamageTaken.HealthUpdate(m_damage);
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
    /// <summary>
    /// Flipping the enemy so it appears that it is moving in the right direction
    /// </summary>
    public void Flip()
    {
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
