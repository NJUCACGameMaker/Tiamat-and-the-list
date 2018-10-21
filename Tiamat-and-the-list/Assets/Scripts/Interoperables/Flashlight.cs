﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Pickable
{
    public string dialogSection;

    public bool test;

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        InputManager.AddOnPick(OnPick);
    }

    // Update is called once per frame
    void Update()
    {

        if (test)
            OnPick();
    }
    void OnInteract()
    {
        if (NearPlayer)
        {
            DialogManager.ShowDialog(dialogSection);

        }
    }
    void OnPick()
    {
        gameObject.transform.position = new Vector3(48.0f, -20.0f, 0.0f);

    }
}