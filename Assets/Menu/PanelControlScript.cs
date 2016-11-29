using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelControlScript : MonoBehaviour
{
    public Button returnButton;
    // Use this for initialization
    void Start()
    {
        returnButton.onClick.AddListener(() => { ReturnToMenu(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            ReturnToMenu();
        }
    }
    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
