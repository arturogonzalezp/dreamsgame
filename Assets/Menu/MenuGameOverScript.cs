using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameOverScript : MonoBehaviour
{
    public Button restart;
    public Button exit;
    // Use this for initialization
    void Start()
    {
        restart.onClick.AddListener(() => { RestartGame(); });
        exit.onClick.AddListener(() => { ExitGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            RestartGame();
        }
        else if (Input.GetKeyDown("joystick button 2"))
        {
            ExitGame();
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    private void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
