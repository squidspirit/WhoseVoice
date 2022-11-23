using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource audioSource;

    void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayClip(AudioClip clip, float volume) {
        if (clip == null) {
            Debug.LogWarning("The audio clip is null");
            return;
        }
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayClip(AudioClip clip) {
        PlayClip(clip, 1f);
    }
}
