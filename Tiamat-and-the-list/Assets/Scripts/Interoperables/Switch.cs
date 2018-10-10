using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Switch : Interoperable {

    public wholeLightController lightController;
    public bool on = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (on)
        {
            StartCoroutine(SetLightAlpha(0f));
        }
        else
        {
            StartCoroutine(SetLightAlpha(0.8f));
        }
	}

    public new string GetArchive()
    {
        JSONClass archive = new JSONClass
        {
            { "state", new JSONData(on) }
        };
        return archive.ToString();
    }

    public new void LoadArchive(string archiveLine)
    {
        JSONNode archive = JSON.Parse(archiveLine);
        on = archive["state"].AsBool;
        if (on)
        {
            lightController.Alpha = 0.8f;
        }
        else
        {
            lightController.Alpha = 0f;
        }
    }

    IEnumerator SetLightAlpha(float targetAlpha)
    {
        float currentAlpha = lightController.Alpha;
        while (Mathf.Abs(currentAlpha - targetAlpha) >= 0.002)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, 0.25f);
            lightController.Alpha = currentAlpha;
            yield return null;
        }
        lightController.Alpha = targetAlpha;
    }
}
