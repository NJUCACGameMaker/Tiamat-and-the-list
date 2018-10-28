using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDown : Interoperable {

    PlayerManager player;
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
            player.transform.position = new Vector3(6.97f, 0.42f, 0);
        }
    }
    public SpriteRenderer spriteRender;
    public override void ShowHint()
    {
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 1f);
    }
    public override void UnshowHint()
    {
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 0f);
    }

}
