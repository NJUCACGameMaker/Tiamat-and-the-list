using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : Interoperable
{
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

    void OnInteract()
    {
        if (NearPlayer)
        {
            DialogManager.ShowDialog(dialogSection);
        }
    }
    public SpriteRenderer spriteRender;
    public override void ShowHint()
    {
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 1f);
    }
    public override void UnshowHint()
    {
        spriteRender.color = new Color(spriteRender.color.r,spriteRender.color.g, spriteRender.color.b, 0f);
    }
}
