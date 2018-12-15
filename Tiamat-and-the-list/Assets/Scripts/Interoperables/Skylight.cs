using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skylight : Interoperable {


    public SpriteRenderer hintSprite;
    private Animator skylightAnima;
    public string dialogSection;
    public bool opened = false;
    private float hintAlpha = 0f;
    private bool showHint = false;
    
    void Start () {
        interoperable = false;
        skylightAnima = GetComponent<Animator>();
        InputManager.AddOnPick(OnPick);
	}
	
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

    void OnPick()
    {
        if (NearPlayer)
        {
            DialogManager.ShowDialog(dialogSection, Depart);
        }
    }

    void Depart()
    {
        SceneManager.LoadScene("Level1-Scene1", LoadSceneMode.Single);
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
