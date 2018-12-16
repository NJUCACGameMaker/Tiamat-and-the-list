using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Interoperable {

    public string dialogSection;
	// Use this for initialization
	void Start () {
        InputManager.AddOnInteract(OnInteract);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnInteract()
    {
        DialogManager.ShowDialog(dialogSection);
        interoperable = false;
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
    }
}
