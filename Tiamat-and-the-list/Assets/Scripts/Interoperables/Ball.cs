using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Interoperable
{

    public SpriteRenderer hintSprite;
    public string dialogSection;
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
            DialogManager.ShowDialog(dialogSection);
        }
    }
}
