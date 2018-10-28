using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairUp : Interoperable {

    PlayerManager player;
	// Use this for initialization
	void Start () {
        InputManager.AddOnUpStair(OnUp);
	}
	
	// Update is called once per frame
	void Update () {
		
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
    void OnUp()
    {
        if (NearPlayer)
        {
            player.floorLayer++;
            player.transform.position =new Vector3(13.98f, 2.57f, 0);
        }
    }
}
