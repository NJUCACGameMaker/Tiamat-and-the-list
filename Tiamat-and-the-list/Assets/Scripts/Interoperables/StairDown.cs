using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDown : Interoperable {

    public PlayerManager player;
    public Vector3 targetPos;
    public SpriteRenderer hintSprite;
    public SpriteRenderer stairSprite;

    private float hintAlpha = 0f;
    private bool showHint = false;

    // Use this for initialization
    void Start () {
        InputManager.AddOnDownStair(OnDown);
	}
	
	// Update is called once per frame
	void Update () {
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
    void OnDown()
    {
        if (NearPlayer)
        {
            player.floorLayer--;
            player.transform.position = targetPos;
            stairSprite.sortingLayerName = "BackItem";

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

}
