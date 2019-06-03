
using UnityEngine;

public class Explosion: MonoBehaviour
{
    public GameObject explosoinAnimation;
    public AudioClip explosionSound;

    public AudioSource AudioSource { get; private set; }

    private void Start()
    {
        var audio = new ObjectAudio(this, explosionSound);
        AudioSource = audio.AudioSource;
    }
}