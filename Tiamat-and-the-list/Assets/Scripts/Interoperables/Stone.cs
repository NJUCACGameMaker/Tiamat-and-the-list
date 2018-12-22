using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Stone : Interoperable {

    public string dialogSection;
    public string nextSceneName = "";
    public SpriteRenderer hintSprite;
    private float hintAlpha = 0f;
    private bool showHint = false;
    private bool isdestroy = false;
    // Use this for initialization
    void Start () {
        InputManager.AddOnInteract(OnInteract);
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
        if (NearPlayer)
        {

            DialogManager.ShowDialog(dialogSection, LoadNextScene);
            interoperable = false;
            transform.position = new Vector3(25, 9, 0);
            isdestroy = true;
        }
    }


    void LoadNextScene()
    {

        SceneItemManager.SaveArchive();
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(nextSceneName);
    }


    public override void ShowHint()
    {
        showHint = true;
    }

    public override void UnshowHint()
    {
        showHint = false;
    }
 /*
    public override string GetArchive()
    {
        JSONClass archive = new JSONClass
        {
            { "stone", new JSONData(isdestroy) }
        };
        return archive.ToString();
    }
    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        var isdestroyNode = root["stone"];
        isdestroy = isdestroyNode.AsBool;
        if (isdestroy)
        {
            transform.position = new Vector3(25, 9, 0);
            interoperable = false;
        }
    }
    */
}
