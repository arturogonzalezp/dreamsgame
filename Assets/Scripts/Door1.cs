using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Scene 2");
        }
    }
}
