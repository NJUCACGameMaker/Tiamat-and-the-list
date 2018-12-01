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

    private float hintAlpha = 0f;
    private bool showHint = false;
    
    public AudioClip audioPick;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        InputManager.AddOnPick(OnPick);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioPick;
    }

    // Update is called once per frame
    void Update()
    {
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
            DialogManager.ShowDialog(dialogSection);

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
    void OnPick()
    {
        if (NearPlayer)
        {
            audioSource.Play();
            gameObject.transform.position = new Vector3(48.0f, -20.0f, 0.0f);
            hintSprite.transform.position = new Vector3(48.0f, -18.0f, 0.0f);
            gameObject.GetComponent<Interoperable>().interoperable = false;
            UIManager.SetEquipmentIcon("EquipmentSprite\\Stage00_shoudiantong");
            player.setEquip(EquipmentType.FlashLight);
            picked = true;
            interoperable = false;
            CollectionArchive.CollectionCollect("Flashlight");
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
        if (picked == true)
        {
            gameObject.transform.position = new Vector3(48.0f, -20.0f, 0.0f);
            hintSprite.transform.position = new Vector3(48.0f, -18.0f, 0.0f);
            interoperable = false;

        }
    }
}
