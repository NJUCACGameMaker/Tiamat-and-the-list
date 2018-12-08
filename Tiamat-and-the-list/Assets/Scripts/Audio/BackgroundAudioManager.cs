using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {
    float volume = 1.0f;
    float t = 0;
    bool destroy = false;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
            
    }

    public void SceneChange()
    {
        Destroy(this.gameObject);
    }
}
