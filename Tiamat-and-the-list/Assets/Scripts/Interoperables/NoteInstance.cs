using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class NoteInstance : Interoperable {
    
    public SpriteRenderer hintSprite;
    public string diaologsection1;
    public string noteKey;

    private float hintAlpha = 0f;
    private bool showHint = false;
    
    public AudioClip audioNote;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        InputManager.AddOnInteract(OnInteract);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioNote;
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


    void OnInteract()
    {
        if (NearPlayer&&interoperable)
        {
            audioSource.Play();
            DialogManager.ShowDialog(diaologsection1);
            interoperable = false;
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
            { "interoperable", new JSONData(interoperable) },
            {"Position", new JSONArray{
                new JSONData(transform.position.x),
                new JSONData(transform.position.y),
                new JSONData(transform.position.z)
                }
            }
        };
        return note.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        interoperable = root["interoperable"].AsBool;
        transform.position = new Vector3(root["Position"][0].AsFloat, root["Position"][1].AsFloat, root["Position"][2].AsFloat);
    }
}
