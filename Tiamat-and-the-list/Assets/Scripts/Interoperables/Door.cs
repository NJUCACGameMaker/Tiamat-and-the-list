using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Door : Interoperable
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
        {
            test = false;
        }
    }
    void OnInteract()
    {
        DialogManager.ShowDialog(dialogSection);
    }
    void OnPick()
    {
    }
}
