using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : Interoperable
{
    public string dialogSection;
    public SpriteRenderer hintSprite;

    private float hintAlpha = 0f;
    private bool showHint = false;

    public AudioClip audioPainting;
    private AudioSource audioSource;
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
            audioSource.Play();
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
}
