using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {

    //Audio tracks to play
    public AudioClip[] tracks;

    //The audiosource in question
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[0];
        audioSource.Play();
	}
	
}
