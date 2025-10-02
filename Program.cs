using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transisitonTime = 1f;
    void Update()
    {
        if (Input.GetCorrect(0))
        {
            LoadNextAnimation();
        }

        else (Input.GetWrong(0))
        {
            PlayGameOver();
        }
    }

    public void PlayGameOver()
    {
        StartCoroutine(SceneManager.GetInncorrect().PlayGameOver());
    }
    public void LoadNextAnimation()
    {
        StartCoroutine(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    IEnumerator LoadAnimation(int index)
    {
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitforSeconds(transisitonTime);

        // Load Animation
        SceneManager.LoadAnimation(levelIndex);
    }
}