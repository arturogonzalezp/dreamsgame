using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float jumpSpeed;
    private bool facingRight;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool grounded;
    private bool dead;
	public AudioSource deathSoundEffect;
    // Use this for initialization
    void Start()
    {
        speed = 1.75f;
        jumpSpeed = 4.5f;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        grounded = false;
        facingRight = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (!dead)
        {
            MovePlayer();
        }
    }
    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            animator.SetBool("Moving", true);
            if ((h > 0 && !facingRight) || (h < 0 && facingRight))
            {
                facingRight = !facingRight;
                animator.transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        if (facingRight)
        {
            transform.Translate(h * speed * Time.deltaTime, 0, 0);
        }else
        {
            transform.Translate(-h * speed * Time.deltaTime, 0, 0);
        }
        
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("joystick button 1")) && grounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "MovingFloor")
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }else if (collision.gameObject.name == "Underworld" || collision.gameObject.tag == "Spike")
        {
            PublicDead();
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingFloor")
        {
            //grounded = true;
            //animator.SetBool("Grounded", grounded);
            transform.parent = collision.transform;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingFloor")
        {
            transform.parent = null;
        }
    }
    public void PublicDead()
    {
        dead = true;
        animator.SetBool("Dead", dead);
		deathSoundEffect.Play ();
    }
    void Die()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
