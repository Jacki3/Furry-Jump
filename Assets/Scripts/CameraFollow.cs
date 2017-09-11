using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float m_smoothTimeX;
    public float m_smoothTimeY;
    public GameObject player;

    private Vector2 velocity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Function originally written by 'Gucio Devs'
    public void FixedUpdate()
    {
        float positionX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, m_smoothTimeX);
        float positionY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, m_smoothTimeY);

        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }
}