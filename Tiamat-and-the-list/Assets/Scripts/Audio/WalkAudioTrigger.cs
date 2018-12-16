using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAudioTrigger : MonoBehaviour {
	public AudioClip walkLeft;
	public AudioClip walkRight;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		//animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onWalkLeft(){
		audioSource.clip = walkLeft;
		audioSource.Play();
	}

	void onWalkRight(){
		audioSource.clip = walkLeft;
		audioSource.Play();
	}
}
