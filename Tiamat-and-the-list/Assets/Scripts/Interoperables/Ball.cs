using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Interoperable
{
    public Switch lightSwitch;
    public Skylight skylight;
    public PlayerManager player;
    //手电筒光所能照到的向前距离
    public float lightLength;
    //判定人物位置的宽容度
    public float tolerance;

    public SpriteRenderer hintSprite;
    public string dialogSection;
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
        if (((Mathf.Abs(transform.position.x - player.transform.position.x - lightLength) < tolerance && 
            !player.isLeft) || 
            (Mathf.Abs(transform.position.x - player.transform.position.x + lightLength) < tolerance &&
            player.isLeft)) && 
            player.currentEquipType == EquipmentType.FlashLight && player.itemOn && !lightSwitch.on)
        {
            skylight.Open();
        }

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
            DialogManager.ShowDialog(dialogSection);
        }
    }
}
