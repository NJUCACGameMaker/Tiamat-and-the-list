using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handwriting : Interoperable
{

    public string dialogSection1;
    public string dialogSection2;
    public SpriteRenderer hintSprite;
    public Switch getSwitch;

    private float hintAlpha = 0f;
    private bool showHint = false;
    // Use this for initialization

    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
    }

    // Update is called once per frame
    void Update()
    {
        if (showHint && hintAlpha < 1.0f)
        {
            hintAlpha += Time.deltaTime * 4;
            if (hintAlpha > 1.0f)
                hintAlpha = 1.0f;
            hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, hintAlpha);
        }
        if (!showHint && hintAlpha > 0f)
        {
            hintAlpha -= Time.deltaTime * 4;
            if (hintAlpha < 0f)
                hintAlpha = 0.0f;
            hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, hintAlpha);
        }
    }

    public override void ShowHint()
    {
        showHint = true;
    }

    public override void UnshowHint()
    {
        showHint = false;
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

