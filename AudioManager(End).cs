using UnityEngine;
using UnityEngine.UIElements;

public class AudioManagerEnd : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;

    public AudioClip win;
    public AudioClip lose;

    private void Start()
    {
        if (QuestionOutput.gameComplete == true)
        {
            sfxSource.clip = win;
            sfxSource.Play();
        }
        else
        {
            sfxSource.clip = lose;
            sfxSource.Play();
        }
    }
}
