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
            InputManager.onAnimated = true;
            StartCoroutine(player.MoveTo(new Vector2(13.75f, 1.42f),
                () => StartCoroutine(MoveSmooth(new Vector2(13.75f, 1.42f), new Vector2(7.5f, -5.25f), 3.45f))));
        }
    }

    IEnumerator MoveSmooth(Vector2 from, Vector2 target, float duration)
    {
        player.playerAnima.SetTrigger("DownStair");
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
        stairSprite.sortingLayerName = "BackItem";
        player.transform.position = target;
        player.floorLayer--;
        StartCoroutine(player.MoveTo(targetPos, () => InputManager.onAnimated = false));
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
