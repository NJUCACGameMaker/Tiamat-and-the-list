using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public float backgroundMusicVolume = 1.0f;
    public float effectSoundVolume = 1.0f;
    public bool muteBackgroundMusic = false;
    public bool muteEffectSound = false;
    // Use this for initialization
    void Awake () {
        // 同一场景不能有两个Manager
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            Destroy(this.gameObject);
        this.gameObject.tag = "AudioManager";
        // 要求这个manager可以跨场景
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject uiAudioPlayer = GameObject.Find("UIAudioPlayer");
        if (uiAudioPlayer != null && !uiAudioPlayer.GetComponent<AudioSource>().isPlaying)
            Destroy(uiAudioPlayer.gameObject);
        var audios = GameObject.FindObjectsOfType<AudioSource>();
        foreach (var audio in audios)
        {
            var gameObject = audio.gameObject;
            if (gameObject.tag == "BackgroundMusic")
            {
                audio.volume = backgroundMusicVolume;
                audio.mute = muteBackgroundMusic;
            } else
            {
                audio.volume = effectSoundVolume;
                audio.mute = muteEffectSound;
            }
            
        }
    }

}
