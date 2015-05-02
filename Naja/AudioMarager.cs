using UnityEngine;
using System.Collections;

public class AudioMarager : MonoBehaviour {
    public static AudioMarager m_instance;
    private AudioSource audio;
    public AudioClip collectibleAudioClip;

    void Awake() {
        m_instance = this;
        audio = this.GetComponent<AudioSource>();
    }

    public void PlayCollectible() {
        audio.PlayOneShot(collectibleAudioClip);
    }
}
