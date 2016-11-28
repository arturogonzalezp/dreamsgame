using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;
	private string direction;

	void Start()
	{
		// Values
		speed = 3.5f;
	}

	void Update()
	{
		Move();
	}
	private void Move()
	{
		if (direction == "right")
		{
			transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
		}
		else if (direction == "left")
		{
			transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
		}
		else if (direction == "up")
		{
			transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
		}
		else if (direction == "down")
		{
			transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
		}
	}
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
	void ChangeDirection(string dir)
	{
		direction = dir;
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Zombie")
		{
			Vector2 dir = new Vector2(0, 0);
			if (direction == "left")
			{
				dir.x = -1;
			}
			else if (direction == "right")
			{
				dir.x = 1;
			}

			collider.gameObject.GetComponent<EnemyLevel2>().Hit(dir);
		}
		if (collider.gameObject.name != "Player")
		{
			Destroy(gameObject);
		}
	}
}
