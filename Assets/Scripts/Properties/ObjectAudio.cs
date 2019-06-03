using UnityEngine;

public class ObjectAudio
{
    public AudioSource AudioSource { get; set; }

    public ObjectAudio(MonoBehaviour monoBehaviour, AudioClip audioClip) {
        AudioSource = monoBehaviour.gameObject.AddComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        AudioSource.clip = audioClip;
    }
}