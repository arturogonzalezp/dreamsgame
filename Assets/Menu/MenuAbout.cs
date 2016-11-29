using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAbout : MonoBehaviour {
	public Button goBack;


	// Use this for initialization
	void Start () {
		goBack.onClick.AddListener (() => {
			OpenMenu ();
		});
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("joystick button 1")) {
			OpenMenu ();
	}

	}
	private void OpenMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
