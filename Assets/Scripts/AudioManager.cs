using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

public static AudioManager instance;

AudioSource audioSource;

    private void Awake()
    {
        if(instance != null)
        Destroy(this);
        else
        instance = this;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playAudio(AudioClip clip)
    {
        audioSource.pitch = Random.Range(.9f,1.1f);
        audioSource.PlayOneShot(clip,1);
    }
}
