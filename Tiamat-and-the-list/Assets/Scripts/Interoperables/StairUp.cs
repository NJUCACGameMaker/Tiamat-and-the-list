using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StairUp : Interoperable {

    public PlayerManager player;
    public Vector3 targetPos;
    public SpriteRenderer hintSprite;
    public SpriteRenderer stairSprite;

    private float hintAlpha = 0f;
    private bool showHint = false;

    // Use this for initialization
    void Start () {
        InputManager.AddOnUpStair(OnUp);
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

    public override void ShowHint()
    {
        showHint = true;
    }

    public override void UnshowHint()
    {
        showHint = false;
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
