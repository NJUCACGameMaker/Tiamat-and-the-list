using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassEffect : MonoBehaviour
{
    // only attached to MainCamera
    private bool ghostExisted;

    private AudioLowPassFilter filter;
    private float currentFrequency;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<AudioLowPassFilter>() == null)
            gameObject.AddComponent<AudioLowPassFilter>();
        filter = GetComponent<AudioLowPassFilter>();
        filter.cutoffFrequency = 8000;
        currentFrequency = 8000;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("SkillCharacter(Clone)") == null)
            ghostExisted = false;
        else
            ghostExisted = true;
        if (ghostExisted && filter.cutoffFrequency > 1000)
        {
            currentFrequency -= Time.deltaTime * 6000;
            if (currentFrequency <= 1000)
                currentFrequency = 1000;
            filter.cutoffFrequency = currentFrequency;
        }
        if (!ghostExisted && filter.cutoffFrequency < 8000)
        {
            currentFrequency += Time.deltaTime * 6000;
            if (currentFrequency >= 8000)
                currentFrequency = 8000;
            filter.cutoffFrequency = currentFrequency;
        }
    }
}
