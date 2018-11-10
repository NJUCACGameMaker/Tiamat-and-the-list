using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StairUp : Interoperable {

    public PlayerManager player;
    public Vector3 targetPos;
    public SpriteRenderer hintSprite;
    public SpriteRenderer stairSprite;

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
            stairSprite.sortingLayerName = "ForeItem";
           
        }
    }
    public override string GetArchive()
    {
        var Stair = new JSONClass
        {
            { "floor", new JSONData(player.floorLayer) }
        };
        return Stair.ToString();
    }
    public override void LoadArchive(string archiveLine)
    {
        var root = JSONClass.Parse(archiveLine);
        int temp = root["floor"].AsInt;
        if (temp == 0)
            stairSprite.sortingLayerName = "BackItem";
        else
            stairSprite.sortingLayerName = "ForeItem";
    }
}
