using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Flashlight : Pickable
{
    public string dialogSection;

    bool picked = false;

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        InputManager.AddOnPick(OnPick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnInteract()
    {
        if (NearPlayer)
        {
            DialogManager.ShowDialog(dialogSection);

        }
    }
    public SpriteRenderer spriteRender;
    public override void ShowHint()
    {
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 1f);
    }
    public override void UnshowHint()
    {
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 0f);
    }
    void OnPick()
    {
        gameObject.transform.position = new Vector3(48.0f, -20.0f, 0.0f);
        picked = true;
    }
    public override string GetArchive()
    {
        var flashlight = new JSONClass();
        flashlight.Add("picked", new JSONData(picked));
        return flashlight.ToString();
    }
    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        var pickedNode = root["picked"];
        picked = pickedNode.AsBool;
        if(picked==true)
            gameObject.transform.position = new Vector3(48.0f, -20.0f, 0.0f);
    }
}
