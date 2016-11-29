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
		speed = 2.0f;
		jumpSpeed = 4f;
		life = 100;
		pushForce = 2.3f;

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
			rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
			grounded = false;
			animator.SetBool("Grounded", grounded);
		}
	
	}
	private void CheckShoot(){
		if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown("joystick button 3"))
		{
			Shoot();
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
				GameObject bulletclone = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
				bulletclone.SendMessage("ChangeDirection", "right");
			}
			else
			{
				GameObject bulletclone = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, -270)));
				bulletclone.SendMessage("ChangeDirection", "left");
			}
		}
	}
	public void Die()
	{
		SceneManager.LoadScene("MenuGameOver");
	}
	public void Hit(int value, Vector2 dir)
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
		lifeDisplay.text = life + "%";
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "MovingFloor") {
			grounded = true;
			animator.SetBool ("Grounded", grounded);
		}
	}

}
