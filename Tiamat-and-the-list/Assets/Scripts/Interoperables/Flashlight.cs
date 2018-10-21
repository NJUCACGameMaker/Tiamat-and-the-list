using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Interoperable
{
    public string dialogSection;
    float Movespeed = 1000f;

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
        float step = Movespeed * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(48, -20, 0), step);

    }
}
