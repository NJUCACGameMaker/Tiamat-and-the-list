using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairUp : Interoperable {

    public PlayerManager player;
    public Vector3 targetPos;
    public SpriteRenderer hintSprite;
    // Use this for initialization
    void Start () {
        InputManager.AddOnUpStair(OnUp);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void ShowHint()
    {
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 1f);
    }

    public override void UnshowHint()
    {
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 0f);
    }

    void OnUp()
    {
        if (NearPlayer)
        {
            player.floorLayer++;
            player.transform.position = targetPos;
        }
    }
}
