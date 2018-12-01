using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class NoteInstance : Interoperable {

    public Specialpaint temp_paint;
    public SpriteRenderer hintSprite;
    public bool if_picked = false;
    public string diaologsection1;
    public string noteKey;

    private float hintAlpha = 0f;
    private bool showHint = false;

    // Use this for initialization
    void Start () {
        InputManager.AddOnInteract(OnInteract);
        interoperable = false;
	}
	
	// Update is called once per frame
	void Update () { 
        if (temp_paint.interoperable == false && if_picked == false)
        {
            interoperable = true;
        }
        else interoperable = false;

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


    void OnInteract()
    {
        if (NearPlayer&&interoperable)
        {
            DialogManager.ShowDialog(diaologsection1);
            interoperable = false;
            if_picked = true;
            transform.position = new Vector3(48.0f, -10.0f, 0.0f);
            hintSprite.transform.position = new Vector3(48.0f, -8.0f, 0.0f);

            CollectionArchive.NoteCollect(noteKey);
        }
    }
    public override void ShowHint()
    {
        if (interoperable)
            showHint = true;
    }

    public override void UnshowHint()
    {
        showHint = false;
    }

    public override string GetArchive()
    {
        var note = new JSONClass
        {
            { "if_picked", new JSONData(if_picked) }
        };
        return note.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        var pickednote = root["if_picked"];
        if_picked = pickednote.AsBool;
        if(if_picked)
        {
            transform.position = new Vector3(48.0f, -10.0f, 0.0f);
            hintSprite.transform.position = new Vector3(48.0f, -8.0f, 0.0f);
            interoperable = false;
        }
    }
}
