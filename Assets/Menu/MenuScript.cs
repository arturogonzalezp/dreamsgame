using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public Button start;
    public Button options;
    public Button exit;
	// Use this for initialization
	void Start () {
        start.onClick.AddListener(() => { StartGame(); });
        options.onClick.AddListener(() => { OpenOptions(); });
        exit.onClick.AddListener(() => { ExitGame(); });
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("joystick button 1"))
        {
            StartGame();
        }else if (Input.GetKeyDown("joystick button 0"))
        {
            OpenOptions();
        }
        else if (Input.GetKeyDown("joystick button 2"))
        {
            ExitGame();
        }
	}
    private void StartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    private void OpenOptions()
    {
        Debug.Log("Open Options");
    }
    private void ExitGame()
    {
        Debug.Log("Exit Game");
    }
}
