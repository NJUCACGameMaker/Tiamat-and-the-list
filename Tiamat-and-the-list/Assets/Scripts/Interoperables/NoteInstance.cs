using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class NoteInstance : Interoperable {

    public Specialpaint temp_paint;
    public SpriteRenderer hintSprite;
    public bool if_picked = false;
    public string diaologsection1;
    
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
        }
    }
    public override void ShowHint()
    {
        if(interoperable)
            hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 1f);
    }

    public override void UnshowHint()
    {
            hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 0f);
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
