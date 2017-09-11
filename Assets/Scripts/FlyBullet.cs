using UnityEngine;
using System.Collections;

public class FlyBullet : MonoBehaviour
{
    /// <summary>
    /// Class which handles touch input: calculates where the touch/click has happened and changes the objects velocity's direction towards that location multiplied by a custom speed value
    /// </summary>

    public float m_speed = 5f;

    Vector2 m_myPos;
    private Transform camTrans, myTrans;

    // Use this for initialization
    void Start()
    {
        camTrans = Camera.main.transform;
        myTrans = this.transform;
    }

    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Calculates where the touch has begun and converts the value of screen coordinates to world coordinates
            Vector2 tempTouch = new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y);
            m_myPos = new Vector3(transform.position.x, transform.position.y + 1);
            //Direction is the difference of where the touch began and where it came from
            Vector2 direction = tempTouch - m_myPos;
            direction.Normalize();
            //Using the object pooler rather 
            GameObject obj = ObjectPooler.s_current.GetPooledObject();

            if (obj == null)
                return;

            obj.transform.position = transform.position;
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().velocity = direction * m_speed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Same as the touch calculation except for mouse clicks on the screen
            Vector2 target = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            m_myPos = new Vector3(transform.position.x, transform.position.y + 1);
            Vector2 direction = target - m_myPos;
            direction.Normalize();

            GameObject obj = ObjectPooler.s_current.GetPooledObject();

            if (obj == null)
                return;

            obj.transform.position = transform.position;
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().velocity = direction * m_speed;
        }
    }

}


