using UnityEngine;
using System.Collections;

public class PlayerMovement2 : MonoBehaviour {
    // Values
    public float speed;
    public float jumpSpeed;
    private int life;
    private float pushForce;
    private int secondsToDie;

    // Physics and animator
    private Rigidbody2D rigidBody;
    private Animator animator;

    // Animator Values
    private bool grounded;
    private bool faceRight;

    // Coroutines
    private IEnumerator dieTimer;

    // Use this for initialization
    void Start () {
        // Values
        speed = 1.5f;
        jumpSpeed = 4f;
        life = 100;
        pushForce = 2.3f;
        secondsToDie = 3;

        // Physics and animator
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Animator Values
        grounded = false;
        faceRight = true;

        // Coroutines
        dieTimer = dieTimerCorutine();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
        // Player Motion
        PlayerMotion();
    }
    void PlayerMotion()
    {
        if(life > 0)
        {
            CheckMove();
            CheckJump();
            CheckAttack();
        }
    }
    private void CheckMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((h < 0 && faceRight) || (h > 0 && !faceRight))
        {
            Rotate();
        }
        float movementSpeed = Mathf.Abs(h * speed * Time.deltaTime);
        transform.Translate(movementSpeed, 0, 0);
        animator.SetFloat("Speed", movementSpeed);
    }
    private void CheckJump()
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.Joystick1Button1)) && grounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
    }
    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Attack();
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attacking");
    }
    private void Rotate()
    {
        faceRight = !faceRight;
        animator.transform.Rotate(0, 180, 0);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "MovingFloor")
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }
    }
    void OnBecameInvisible()
    {
        StartCoroutine(dieTimer);
    }
    void OnBecameVisible()
    {
        StopCoroutine(dieTimer);
    }
    IEnumerator dieTimerCorutine()
    {
        int count = 0;
        while (true)
        {
            count++;
            if(count != 1)
            {
                //Debug.Log("Timer: " + (count-1));
                // counter - 1 (Segundo actual)
            }
            if(count > secondsToDie)
            {
                Debug.Log("Die");
                StopCoroutine(dieTimer);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
