using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Flashlight : Pickable
{
    public string dialogSection;
    public PlayerManager player;
    public bool picked = false;
    public SpriteRenderer hintSprite;

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
    public override void ShowHint()
    {
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 1f);
    }
    public override void UnshowHint()
    {
        hintSprite.color = new Color(hintSprite.color.r, hintSprite.color.g, hintSprite.color.b, 0f);
    }
    void OnPick()
    {
        if (NearPlayer)
        {
            gameObject.transform.position = new Vector3(48.0f, -20.0f, 0.0f);
            gameObject.GetComponent<Interoperable>().interoperable = false;
            UIManager.SetEquipmentIcon("EquipmentSprite\\Stage00_shoudiantong");
            player.setEquip(EquipmentType.FlashLight);
            picked = true;
        }
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
