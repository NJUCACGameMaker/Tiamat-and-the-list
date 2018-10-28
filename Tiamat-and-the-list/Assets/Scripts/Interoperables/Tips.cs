using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : Interoperable
{
    public string dialogsection;
    
    // Use this for initialization
    void Start()
    {
        InputManager.AddOnUpStair(OnInteract);
    }

    // Update is called once per frame
    void Update()
    {

    }
   void OnInteract()
    {
        if (NearPlayer)
            DialogManager.ShowDialog(dialogsection);
    }
}
