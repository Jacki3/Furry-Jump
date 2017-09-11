using System;
using UnityEngine;
using System.Collections;

public class PlayerController_m : MonoBehaviour
{
    /// <summary>
    /// Class which handles player movement, measuring distance travelled and setting a boost value
    /// Movement is derived from Unity's input manager and accelerometer controls are based upon these values
    /// </summary>
    public static float m_maxSpeed = 30f;
    public float m_jumpForce = 1000f;
    public float m_boostSpeed = 15f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float maxHealth = 10f;
    public float currentHealth;
	public static bool maxBoost = false;
    [HideInInspector]
    public float hor;
    public bool isMobile;
    public float currentPos;
    public float previousPos;
    public float m_distance;
    Vector3 lastPos;
    float m_newPos;
    public bool lookingRight = true;
    public Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded = false;
    GameObject m_backGround;
    GameObject m_startText;
	Transform m_transform;

	private GameObject platform;
	public float distance = 5;
	private bool playerCloseEnough = false;

	Vector3 startPos;
	float minSwipeDistX, minSwipeDistY;
	bool isJump = false;


	bool isSwipe = false;
	bool isTouch = false;
	bool swipedLeft = false;
	public static bool swipedUp = false;
	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			switch (touch.phase)
			{
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Moved:
				isTouch = true;
				float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

				if (swipeDistHorizontal > minSwipeDistX)
				{
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					if (swipeValue > 0 && !isSwipe)//to right swipe
					{
						isTouch = false;
						isSwipe = true;
						Debug.Log("Right");
					}
					else if (swipeValue < 0 && !isSwipe)//to left swipe
					{
						swipedLeft = true;
						isTouch = false;
						isSwipe = true;
					}
				}
				// add swipe to up
				if (swipeDistVertical > minSwipeDistY)
				{
					float swipeValueY = Mathf.Sign(touch.position.y - startPos.y);
					if (swipeValueY > 0 && !isSwipe)
					{
						isTouch = false;
						isSwipe = true;
						swipedUp = true;
					}
				}
				break;
			case TouchPhase.Stationary:
				isJump = true;
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				isTouch = false;
				isSwipe = false;
				break;
			}
		}		

		platform = GameObject.FindGameObjectWithTag ("ground");

		if (Vector3.Distance (transform.position, platform.gameObject.GetComponent<Transform>().position) < distance) 
		{
			playerCloseEnough = true;
		}
		else
		{
			playerCloseEnough = false;
		}

		if (playerCloseEnough = true) 
		{
			if (Time.timeScale == 1.0F)
				Time.timeScale = 0.7F;
			else
				Time.timeScale = 1.0F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
	}

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_startText = GameObject.Find("StartText");
        //Assigning last known position to compare against new position
        currentPos = transform.position.x;
        previousPos = transform.position.x;
        lastPos = transform.position;
        currentHealth = maxHealth;

		m_transform = GetComponent<Transform> ();

		minSwipeDistX = minSwipeDistY = Screen.width / 6;

    }

    void FixedUpdate()
    {
		//m_transform.Translate (Vector3.right * 10.0f * Time.deltaTime); 

        //Grabbing background to scroll on move
        m_backGround = GameObject.Find("Background");
        Scroll scroll = m_backGround.GetComponent<Scroll>();
        //Input for accelerometer
        if (isMobile == true)
            hor = Input.acceleration.x * 1.5f;
        else
        {
            hor = Input.GetAxis("Horizontal");
        }
        //Getting the animation for speed
        anim.SetFloat("Speed", Mathf.Abs(hor));
        //Setting the speed of the player
        rb2d.velocity = new Vector2(hor * m_maxSpeed, rb2d.velocity.y);
        //Test to check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround);
        //Setting the animation for when player is grounded
        anim.SetBool("IsGrounded", isGrounded);
        //Flips player dependent on which way it is facing
        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
            Flip();
        //Animation for falling downwards
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        //Scroll if player moves
        if (hor > 0)
            //scroll.Go();
        //Initializes boost if player has collceted five orbs
		if (BoostOrb.m_orbCount == BoostOrb.m_maxorbCount) //&& swipedLeft)
		{			
			StartCoroutine (Boost ());
			maxBoost = true;
		}

        //Resets orb count if the player is currently boosting
        if (m_maxSpeed > 42f)
            BoostOrb.m_orbCount = 0;
        	Score();
    }

    IEnumerator Boost()
    {
        m_maxSpeed = m_boostSpeed;
        yield return new WaitForSeconds(20);
        m_maxSpeed = 42f;
    }

    //Flipping the player to face in the right direction
    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    /// <summary>
    /// This function sends the score value as distance travelled
    /// Distance travelled is calculated by subtracting the current position from the previous position
    /// </summary>
    void Score()
    {
        GameObject controller = GameObject.Find("Controller");
        Controller ScoreText = controller.GetComponent<Controller>();

        if (previousPos < currentPos)
        {
            m_newPos = transform.position.x;
            m_distance += Mathf.Abs(m_newPos - lastPos.x);
            lastPos = transform.position;
        }
        previousPos = currentPos;
        currentPos = transform.position.x;

        ScoreText.UpdateScore();

    }
    /// <summary>
    /// This function is used to calculate total score 
    /// It grabs all score which is added for destroying enemies
    /// Distance is measured in the above function and score is then calculated as a total within the update kill score function
    /// </summary>
    /// <param name="value"></param>
    public void Score(int value)
    {
        GameObject controller = GameObject.Find("Controller");
        Controller ScoreText = controller.GetComponent<Controller>();

        ScoreText.UpdateKillScore(value);
    }

    public void MobileControls()
    {
        isMobile = true;
    }

    public void KeyboardControls()
    {
        isMobile = false;
    }
}