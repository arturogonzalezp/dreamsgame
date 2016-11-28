using UnityEngine;
using System.Collections;

public class EnemyLevel2 : MonoBehaviour {

    // Values
    private int life;
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
        life = 4;
        speed = 0.35f;
        threeshold = 0.27f;

        // Physics and animator
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Animator Values
        faceRight = false;
        moving = false;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        //CheckAttack();
	}
    private void CheckAttack()
    {
        float distance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
        Debug.Log(distance + ", " + threeshold);
        if (threeshold >= distance)
        {
            animator.SetTrigger("Attack");
            
        }
    }
    public void StopAttack()
    {
        animator.SetTrigger("StopAttack");
    }
    private void Move()
    {
        //float distance = Vector2.Distance(transform.position, player.transform.position);
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
            transform.position = new Vector2(Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime).x,transform.position.y);
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
    public void Die()
    {
        Destroy(gameObject);
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
    void OnBecameVisible()
    {
        Debug.Log(gameObject.name + " is visible");
        animator.SetTrigger("Visible");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            animator.SetTrigger("Attack");
        }
    }
}
