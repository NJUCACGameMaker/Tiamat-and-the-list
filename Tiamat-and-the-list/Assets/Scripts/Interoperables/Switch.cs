using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Switch : Interoperable
{
    public SpriteRenderer hintSprite;
    public WholeLightController lightController;
    public string dialogSection;
    public Animator lightAnimator;
    public bool on = true;

    public AudioClip audioSwitchOn;
    public AudioClip audioSwitchOff;
    private AudioSource audioSource;

    private float hintAlpha = 0f;
    private bool showHint = false;

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        InputManager.AddOnPick(OnPick);
        audioSource = GetComponent<AudioSource>();
    }

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
            if (on)
            {
                on = false;
                audioSource.clip = audioSwitchOff;
                audioSource.Play();
                StartCoroutine(SetLightAlpha(0.8f));
            }
            else
            {
                on = true;
                audioSource.clip = audioSwitchOn;
                audioSource.Play();
                StartCoroutine(SetLightAlpha(0f));
            }
            lightAnimator.SetBool("LightOn", on);
        }
    }

    public override string GetArchive()
    {
        JSONClass archive = new JSONClass
        {
            { "state", new JSONData(on) }
        };
        return archive.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        if (archiveLine != null && archiveLine != "")
        {
            JSONNode archive = JSON.Parse(archiveLine);
            on = archive["state"].AsBool;
            if (on)
            {
                lightController.Alpha = 0f;
            }
            else
            {
                lightController.Alpha = 0.8f;
            }
            lightAnimator.SetBool("LightOn", on);
        }
    }

    IEnumerator SetLightAlpha(float targetAlpha)
    {
        float currentAlpha = lightController.Alpha;
        while (Mathf.Abs(currentAlpha - targetAlpha) >= 0.002)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, 0.25f);
            lightController.Alpha = currentAlpha;
            yield return null;
        }
        lightController.Alpha = targetAlpha;
    }
}
