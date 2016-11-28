using UnityEngine;
using System.Collections;

public class CameraLevel2 : MonoBehaviour {
    private float speed;
    public Transform[] nodes;
    private float threshold;
    private int actualNode;
	// Use this for initialization
	void Start () {
        //speed = 0.25f;
        speed = 0.9f;
        actualNode = 0;
        threshold = 0.02f;

        // Test
        GameObject player = GameObject.Find("Player");
        transform.position = nodes[1].position;
        actualNode = 1;
        player.gameObject.transform.position = new Vector2(0f, 1.37f);
	}
	
	// Update is called once per frame
	void Update () {
        if(actualNode < nodes.Length)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, nodes[actualNode].position));
            transform.position = Vector3.MoveTowards(transform.position, nodes[actualNode].position, speed * Time.deltaTime);
            if (distance < threshold)
            {
                actualNode++;
            }
        }
        
        if(actualNode == 1)
        {
            speed = 0.8f;
        }else if(actualNode == 2)
        {
            speed = 0.25f;
        }
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, nodes[0].position);
        for (int x = 0; x < nodes.Length-1; x++)
        {
            Gizmos.DrawLine(nodes[x].position, nodes[x+1].position);
        }
    }
}
