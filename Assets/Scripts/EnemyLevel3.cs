using UnityEngine;
using System.Collections;

public class EnemyLevel3 : MonoBehaviour {

	// Values
	private int life;
    private int multiplicator;
	private float speed;
	private float threeshold;

	// Physics and animator
	private Rigidbody2D rigidBody;
	private Animator animator;

	// Animator Values
	private bool faceRight;
	private bool moving;

	// Player
	public GameObject player;

	// Use this for initialization
	void Start () {
		// Values
		life = 20;
        multiplicator = life;
        speed = 1f;
		threeshold = 0.27f;

		// Physics and animator
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		// Animator Values
		faceRight = true;
		moving = false;

        StartMoving();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}
	private void Move()
	{
		float distance = transform.position.x - player.transform.position.x;
		if(distance < 0 && !faceRight)
		{
			Rotate();
		}else if(distance > 0 && faceRight)
		{
			Rotate();
		}
		if (moving && life > 0)
		{
            float m = (float)multiplicator / life;
            if(m > 3.5f)
            {
                m = 3.5f;
            }
            transform.position = new Vector2(Vector2.MoveTowards(transform.position, player.transform.position, (speed * m) * Time.deltaTime).x,transform.position.y);
		}
	}
	private void Rotate()
	{
		faceRight = !faceRight;
		animator.transform.Rotate(0, 180, 0);
	}
	public void StartMoving()
	{
		moving = true;
		animator.SetBool("Moving", true);

	}
	public void Hit(float pushForce, Vector2 dir)
	{
		life--;
		if(life <= 0)
		{
			animator.SetTrigger("Die");
		}else
		{
			rigidBody.AddForce(dir * pushForce, ForceMode2D.Impulse);
		}
	}
    public void Die()
    {
        Destroy(gameObject);
    }
}
