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
            InputManager.onAnimated = true;
            StartCoroutine(player.MoveTo(new Vector2(8.0f, -5.0f), 
                () => StartCoroutine(MoveSmooth(new Vector2(8.0f, -5.0f), new Vector2(13.5f, 1.32f), 5.15f))));
        }
    }
    
    IEnumerator MoveSmooth(Vector2 from, Vector2 target, float duration)
    {
        stairSprite.sortingLayerName = "ForeItem";
        if (!player.itemOn)
        {
            player.playerAnima.SetTrigger("UpStairNormal");
        }
        else
        {
            player.playerAnima.SetTrigger("UpStairTorch");
        }
        if (from.x < target.x)
        {
            player.SetLeft(false);
        }
        else
        {
            player.SetLeft(true);
        }
        float time = 0.0f;
        player.transform.position = from;
        while (time < duration)
        {
            player.transform.position = new Vector3(Mathf.Lerp(from.x, target.x, time / duration),
                Mathf.Lerp(from.y, target.y, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        player.transform.position = target;
        player.floorLayer++;
        StartCoroutine(player.MoveTo(targetPos, () => InputManager.onAnimated = false));
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
