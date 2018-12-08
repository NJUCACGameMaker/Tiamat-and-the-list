using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skylight : Interoperable {


    public SpriteRenderer hintSprite;
    private Animator skylightAnima;
    public bool opened = false;
    private float hintAlpha = 0f;
    private bool showHint = false;

    // Use this for initialization
    void Start () {
        interoperable = false;
        skylightAnima = GetComponent<Animator>();
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

    public void Open()
    {
        if (!opened)
        {
            skylightAnima.SetTrigger("open");
            opened = true;
            interoperable = true;
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

    public override string GetArchive()
    {
        if (opened)
        {
            return "opened";
        }
        else
        {
            return "closed";
        }
    }

    public override void LoadArchive(string archiveLine)
    {
        if (archiveLine == "opened")
        {
            skylightAnima.SetTrigger("State:open");
            opened = true;
            interoperable = true;
        }
    }
}
