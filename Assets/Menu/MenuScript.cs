using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public Button start;
    public Button options;
    public Button exit;
	public Button about;
	// Use this for initialization
	void Start () {
        start.onClick.AddListener(() => { StartGame(); });
        options.onClick.AddListener(() => { OpenOptions(); });
        exit.onClick.AddListener(() => { ExitGame(); });
		about.onClick.AddListener (() => {
			AboutGame ();
		});
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("joystick button 1")) {
			StartGame ();
		} else if (Input.GetKeyDown ("joystick button 0")) {
			OpenOptions ();
		} else if (Input.GetKeyDown ("joystick button 2")) {
			ExitGame ();
		} else if (Input.GetKeyDown ("joystick button 3")) {
			AboutGame ();
		}
	}
    private void StartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    private void OpenOptions()
    {
        SceneManager.LoadScene("PanelControl");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
	private void AboutGame()
	{
	SceneManager.LoadScene ("Menu About");
	}


	
}
