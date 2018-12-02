using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBackgroundMusicManager : MonoBehaviour {
    public GameObject BackgroundMusic;
    GameObject myMusic;
    public AudioClip clip;

	// Use this for initialization
	void Start () {
        string name = this.gameObject.scene.name;
        if (name == "Setting")
            name = "Cover";
        if (name == "Tutorial-Scene2")
            name = "Tutorial-Scene1";
        myMusic = GameObject.Find("BackgroundMusic_" + name);
        if (myMusic == null)
        {
            myMusic = (GameObject)Instantiate(BackgroundMusic);
            myMusic.name = "BackgroundMusic_" + name;
            myMusic.GetComponent<AudioSource>().clip = clip;
            myMusic.GetComponent<AudioSource>().Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        GameObject uiAudioPlayer = GameObject.Find("UIAudioPlayer");
        if (uiAudioPlayer != null && !uiAudioPlayer.GetComponent<AudioSource>().isPlaying)
            Destroy(uiAudioPlayer.gameObject);
    }
}
