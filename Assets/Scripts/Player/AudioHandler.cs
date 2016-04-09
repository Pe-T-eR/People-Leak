using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

    public AudioClip DumpSound;
    public AudioClip PickupSound;

	public void Play(AudioClip clip)
    {
        var source = GetComponentInParent<AudioSource>();
        source.clip = clip;
        source.Play();
    }
}
