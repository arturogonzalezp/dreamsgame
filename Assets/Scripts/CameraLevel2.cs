using UnityEngine;
using System.Collections;

public class CameraLevel2 : MonoBehaviour {
    public float speed;
    public Transform[] nodes;
    public float threshold;
    private int actualNode;
	// Use this for initialization
	void Start () {
        actualNode = 0;
        threshold = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Mathf.Abs(Vector3.Distance(transform.position, nodes[actualNode].position));
        if(distance < threshold)
        {
            actualNode++;
        }
        if (actualNode != nodes.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, nodes[actualNode].position, speed * Time.deltaTime);
        }
        
	}
}
