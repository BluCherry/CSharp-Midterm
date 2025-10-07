using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button startButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetUsername.username != null)
        {
            startButton.interactable = true;
        }
    }
}