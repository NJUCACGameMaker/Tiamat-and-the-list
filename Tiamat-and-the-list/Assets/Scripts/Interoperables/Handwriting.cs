using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handwriting : Interoperable
{

    public string dialogSection1;
    public string dialogSection2;
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
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 1f);
    }

    public override void UnshowHint()
    {
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 0f);
    }

    void OnInteract()
    {
        if (NearPlayer)
        {
            if (getSwitch.on == false)
                DialogManager.ShowDialog(dialogSection2);
            else
                DialogManager.ShowDialog(dialogSection1);
        }
    }
}

