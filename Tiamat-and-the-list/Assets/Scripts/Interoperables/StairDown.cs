using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDown : Interoperable {

    public PlayerManager player;
    public Vector3 targetPos;
    public SpriteRenderer hintSprite;
    public SpriteRenderer stairSprite;

    // Use this for initialization
    void Start () {
        InputManager.AddOnDownStair(OnDown);
	}
	
	// Update is called once per frame
	void Update () {
		
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
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 1f);
    }
    public override void UnshowHint()
    {
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 0f);
    }

}
