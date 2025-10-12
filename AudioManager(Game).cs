using UnityEngine;

public class AudioManagerGame : MonoBehaviour
{
    [SerializeField] AudioSource mSource;

    public AudioClip background;

    private void Start()
    {
        mSource.clip = background;
        mSource.Play();
    }
}
