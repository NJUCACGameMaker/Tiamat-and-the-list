using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handwriting : Interoperable
{

    public string dialogSection;
    public SpriteRenderer hintSprite;
    public Switch getSwitch;
    // Use this for initialization

    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ShowHint()
    {
        if(getSwitch.on)
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 1f);
    }

    public override void UnshowHint()
    {
        if(getSwitch.on)
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 0f);
    }

    void OnInteract()
    {
        if (NearPlayer&&getSwitch.on==true)
        {
            DialogManager.ShowDialog(dialogSection);
        }
    }
}

