using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SimpleJSON;

public class Specialpaint : Interoperable
{
    public string dialogSection1;
    public string dialogSection2;
    public string dialogSection3;
    public int section = 0;

    public Animator dropAnimator;
    public SpriteRenderer hintSprite;

    public AudioClip audioPainting;
    public AudioClip audioPaintingDrop;
    private AudioSource audioSource;

    private float hintAlpha = 0f;
    private bool showHint = false;

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioPainting;

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
            section++;
            if (section == 3)
                audioSource.clip = audioPaintingDrop;
            audioSource.Play();
            if (section == 1)
                DialogManager.ShowDialog(dialogSection1);
            else if (section == 2)
                DialogManager.ShowDialog(dialogSection2);
            else if (section == 3)
            {
                DialogManager.ShowDialog(dialogSection3);
                interoperable = false;
                dropAnimator.SetBool("drop", true);
            }
        }
    }
    public override string GetArchive()
    {
        var specialpaint = new JSONClass();
        specialpaint.Add("section", new JSONData(section));

        return specialpaint.ToString();
    }
    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        var sectionNode = root["section"];
        section = sectionNode.AsInt;
        if (section == 3)
        {
            interoperable = false;
            dropAnimator.SetTrigger("dropped");
        }

    }
    public override void ShowHint()
    {
        if(section<=3)
            showHint = true;
    }
    public override void UnshowHint()
    {
        showHint = false;
    }
}
