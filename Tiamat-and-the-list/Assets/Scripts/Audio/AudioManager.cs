using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    float volume = 1.0f;
    float t = 0;
    bool destroy = false;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (destroy && t<=1.0f) {
            volume = 1 - t;
            this.GetComponent<AudioSource>().volume = volume;
            t += Time.deltaTime;
        }
        if (t>1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void SceneChange()
    {
        destroy = true;
    }
}
