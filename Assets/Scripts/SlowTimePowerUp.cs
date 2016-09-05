using UnityEngine;
using System.Collections;

public class SlowTimePowerUp : MonoBehaviour {
    public GameObject fastTile;
    private MovingTileLevel1 tileScript;
    // Use this for initialization
    void Start () {
        tileScript = fastTile.GetComponent<MovingTileLevel1>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Player")
        {
            tileScript.speed = 2.0f;
            Destroy(gameObject);
        }
    }
}
