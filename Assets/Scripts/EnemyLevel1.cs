using UnityEngine;
using System.Collections;

public class EnemyLevel1 : MonoBehaviour {
    public GameObject player;
    public float speed = 2f;
    private float minDistance;
    private float range;
    private Vector3 target;
    private bool exit;
    private Animator animator;
    private float pastDistance;
    // Use this for initialization
    void Start () {
        minDistance = 0f;
        target = player.transform.position;
        exit = false;
        pastDistance = transform.position.x - target.x;
        animator = GetComponent<Animator>();
        StartCoroutine(targetChange());
    }
	
	// Update is called once per frame
	void Update () {
        if (!exit)
        {
            range = Vector2.Distance(transform.position, target);
            if (range > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Player")
        {
            exit = true;
            collider.gameObject.GetComponent<PlayerMovement>().PublicDead();
        }
    }
    IEnumerator targetChange()
    {
        while (true)
        {
            target = player.transform.position;
            float distance = transform.position.x - target.x;
            if (Mathf.Sign(pastDistance) != Mathf.Sign(distance))
            {
                animator.transform.Rotate(0, 180, 0);
            }
            pastDistance = distance;
            yield return new WaitForSeconds(2.5f);
        }
    }
}
