using UnityEngine;
using System.Collections;
using Assets.Scripts.Configuration;

public class AudioHandler : MonoBehaviour {

    public AudioClip DumpSound;
    public AudioClip PickupSound;
    public AudioClip CollisionSound;
    public AudioClip CoastGuardSiren;
    public AudioClip RefugeeDelivered;

    public GameObject GameMusic;

    void Start()
    {
        var music = GameObject.FindGameObjectWithTag(Constants.Tags.Music);
        if (music == null)
        {
            DontDestroyOnLoad(Instantiate(GameMusic).transform);
        }
    }

	public void Play(AudioClip clip)
    {
        var source = GetComponentInParent<AudioSource>();
        source.clip = clip;
        source.Play();
    }
}
