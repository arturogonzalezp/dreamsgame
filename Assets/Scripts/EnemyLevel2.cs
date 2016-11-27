using UnityEngine;
using System.Collections;

public class EnemyLevel2 : MonoBehaviour {

    // Values
    private int life;
    private float pushForce;

    // Physics and animator
    private Rigidbody2D rigidBody;
    private Animator animator;

    // Use this for initialization
    void Start () {

        // Values
        life = 4;
        pushForce = 1.7f;

        // Physics and animator
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Hit(Vector2 dir)
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
        animator.SetTrigger("Visible");
    }
}
