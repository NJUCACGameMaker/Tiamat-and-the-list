﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tran_Right : Interoperable {
    public PlayerManager Apkal;
    public Tran_Left left;
    public Image image;
    public TextMeshPro number;
    public string nextSceneName;
    
    private AudioSource audioSource;
    public AudioClip transferAudio;

    public Coroutine lightCoroutine;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
    public override void WithinRange()
    {
        image.color = new Color(0, 0, 0, 1);
        Apkal.transform.position = new Vector3(-6, Apkal.transform.position.y, 0);
        lightCoroutine = StartCoroutine(SetBackLight());
        int currentNum = MazeController.GoRight();
        if (currentNum < 10) number.text = "0" + currentNum.ToString();
        else number.text = currentNum.ToString();
        audioSource.clip = transferAudio;
        audioSource.Play();
        if (MazeController.PuzzleFinished())
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(nextSceneName);
        }
        Debug.Log("Finish-WithinRange");
    }
    
    IEnumerator SetBackLight()
    {
        while (image.color.a > 0.05f)
        {
            image.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(image.color.a, 0.0f, 0.02f));
            yield return null;
        }
        image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}
