using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement3 : MonoBehaviour {
	//Values
	public float speed;
	public float jumpSpeed;
	private int life;
	private float pushForce;
	public AudioSource shootEffect;
	public AudioSource hurtEffect;
	public AudioSource deathEffect;
	public AudioSource jumpEffect;




	//Physics and animator
	private Rigidbody2D rigidBody;
	private Animator animator;

	//Animator Values
	private bool grounded;
	private bool faceRight;
	private bool shooting;


	//Weapons
	public GameObject Bullet;


	//Coroutines


	//UI Elements
	public Text lifeDisplay;


	// Use this for initialization
	void Start () {
		//Values
		speed = 3.0f;
		jumpSpeed = 7f;
		life = 100;
		pushForce = 3.7f;

		// Physics and animator
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		// Animator Values
		grounded = false;
		faceRight = true;
		shooting = false;

		// UI elements
		//lifeDisplay.text = life + "%";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate(){
		//PlayerMotion
		PlayerMotion();
	}
	void PlayerMotion(){
		if (life > 0) {
			CheckMove ();
			CheckJump ();
			CheckShoot ();		
		} else {
			life = 0;
		}
	}
	private void CheckMove(){
		float h = Input.GetAxisRaw("Horizontal");
		if((h < 0 && faceRight) || (h > 0 && !faceRight))
		{
			Rotate();
		}
		float movementSpeed = Mathf.Abs(h * speed * Time.deltaTime);
		transform.Translate(movementSpeed, 0, 0);
		animator.SetFloat("Speed", movementSpeed);
	
	}
	private void CheckJump(){
		if ((Input.GetKeyDown("up") || Input.GetKeyDown("joystick button 1")) && grounded)
		{
			jumpEffect.Play ();
			rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
			grounded = false;
			animator.SetBool("Grounded", grounded);
		}
	
	}
	private void CheckShoot(){
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("joystick button 0"))
		{
			Shoot();
			shootEffect.Play ();
		}
	}
	private void Rotate(){
		faceRight = !faceRight;
		animator.transform.Rotate(0, 180, 0);
	}
	public void Shoot(){
		if (GameObject.FindWithTag("Bullet") == null)
		{
			animator.SetTrigger("Shoot");
			if (faceRight)
			{
				GameObject bulletclone = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
				bulletclone.SendMessage("ChangeDirection", "right");
			}
			else
			{
				GameObject bulletclone = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, -180)));
				bulletclone.SendMessage("ChangeDirection", "left");
			}
		}
	}
	public void Die()
	{
		SceneManager.LoadScene("MenuGameOver");
		deathEffect.Play ();
	}
	/*public void Hit(int value, Vector2 dir)
	{
		life -= value;
		//dir.y *= 1.2f;
		if (life <= 0)
		{
			life = 0;
			lifeDisplay.text = life + "%";
			animator.SetTrigger("Die");

		}
		else
		{
			rigidBody.AddForce(dir * pushForce, ForceMode2D.Impulse);

		}
		//lifeDisplay.text = life + "%";
	}*/
    public void Hit(Vector2 dir)
    {
        rigidBody.AddForce(dir * pushForce, ForceMode2D.Impulse);
		hurtEffect.Play ();
    }
    void OnBecameInvisible()
    {
        if (!GameObject.FindGameObjectWithTag("Santa").GetComponent<EnemyLevel3>().isDead())
        {
            SceneManager.LoadScene("MenuGameOver");
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "MovingFloor") {
			grounded = true;
			animator.SetBool ("Grounded", grounded);
		}else if(collision.gameObject.tag == "Santa")
        {
            Vector2 dir = collision.contacts[0].point - (Vector2)transform.position;
            dir = dir.normalized;
            Hit(-dir);
        }
	}

}
