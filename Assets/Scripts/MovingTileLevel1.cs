using UnityEngine;
using System.Collections;

public class MovingTileLevel1 : MonoBehaviour {
    public float speed;
    public GameObject nodeLeft, nodeRight;
    private bool moveLeft;
	// Use this for initialization
	void Start () {
        moveLeft = true;
	}

    // Update is called once per frame
    void Update() {
        float step = speed * Time.deltaTime;
        Transform target = null;
        if (moveLeft)
        {
            target = nodeLeft.transform;
        } else
        {
            target = nodeRight.transform;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        if (transform.position.x <= nodeLeft.transform.position.x || transform.position.x >= nodeRight.transform.position.x)
        {
            moveLeft = !moveLeft;
        }
    }
}
