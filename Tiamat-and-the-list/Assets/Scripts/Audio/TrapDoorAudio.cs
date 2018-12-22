using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorAudio : MonoBehaviour {
    private AudioSource audiosource;
    public AudioClip doorDown;
    public AudioClip doorUp;
    private float lastPositionY;
    private float currentPositionY;
    private float lastSpeedY;
    private float currentSpeedY;

    private bool isOpening;
    private bool isCloseing;
    // Use this for initialization
    void Start () {
        audiosource = GetComponent<AudioSource>();
        lastPositionY = transform.position.y;
        lastSpeedY = 0;
    }
	
	// Update is called once per frame
	void Update () {
        currentPositionY = transform.position.y;
        currentSpeedY = (currentPositionY - lastPositionY) / Time.deltaTime;
        // door open
        if (lastSpeedY >= 0 && currentSpeedY < 0)
        {
            playDoorCloseAudio();
        } 
        if (lastSpeedY <= 0 && currentSpeedY > 0)
        {
            playDoorOpenAudio();
        }
        lastPositionY = currentPositionY;
        lastSpeedY = currentSpeedY;

    }

    void playDoorOpenAudio()
    {
        audiosource.clip = doorUp;
        audiosource.Play();
    }

    void playDoorCloseAudio()
    {
        audiosource.clip = doorDown;
        audiosource.Play();
    }
}
