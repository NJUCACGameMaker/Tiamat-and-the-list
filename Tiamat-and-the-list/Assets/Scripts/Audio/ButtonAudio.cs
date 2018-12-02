﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    private AudioSource audioSource;
    public AudioClip audioButtonSlide;
    public AudioClip audioButtonClick;
    private GameObject uiAudioPlayer;
	// Use this for initialization
	void Start () {
        uiAudioPlayer = GameObject.Find("UIAudioPlayer");
        if (uiAudioPlayer == null)
        {
            uiAudioPlayer = new GameObject();
            uiAudioPlayer.name = "UIAudioPlayer";
            audioSource = uiAudioPlayer.AddComponent<AudioSource>();
            DontDestroyOnLoad(uiAudioPlayer);
        } else { 
            audioSource = uiAudioPlayer.GetComponent<AudioSource>();
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.clip = audioButtonSlide;
        audioSource.Play();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = audioButtonClick;
        audioSource.Play();
    }
}
