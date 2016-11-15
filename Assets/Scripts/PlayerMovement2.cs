﻿using UnityEngine;
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
    private bool attacking;

    // Weapons
    public GameObject kunai;

    // Coroutines
    private IEnumerator dieTimer;

    // Use this for initialization
    void Start () {
        // Values
        speed = 1.5f;
        jumpSpeed = 4f;
        life = 100;
        pushForce = 2.3f;
        secondsToDie = 2;

        // Physics and animator
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Animator Values
        grounded = false;
        faceRight = true;
        attacking = false;

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
            CheckThrowKunai();
        }else
        {
            life = 0;
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
    private void CheckThrowKunai()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            ThrowKunai();
        }
    }
    public void Attack()
    {
        attacking = true;
        animator.SetTrigger("Attacking");
    }
    public void StopAttack()
    {
        attacking = false;
    }
    private void Rotate()
    {
        faceRight = !faceRight;
        animator.transform.Rotate(0, 180, 0);
    }
    public void ThrowKunai()
    {
        if (GameObject.FindWithTag("Kunai") == null)
        {
            animator.SetTrigger("ThrowKunai");
            if (faceRight)
            {
                GameObject kunaiclone = (GameObject)Instantiate(kunai, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                kunaiclone.SendMessage("ChangeDirection", "right");
            }
            else
            {
                GameObject kunaiclone = (GameObject)Instantiate(kunai, transform.position, Quaternion.Euler(new Vector3(0, 0, -270)));
                kunaiclone.SendMessage("ChangeDirection", "left");
            }
        }
    }
    private void StartDying()
    {
        animator.SetTrigger("Die");
    }
    public void Die()
    {
        // Die
        Debug.Log("Died");
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
                StartDying();
                StopCoroutine(dieTimer);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}