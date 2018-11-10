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

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        InputManager.AddOnPick(OnPick);
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
            if (on)
            {
                on = false;
                StartCoroutine(SetLightAlpha(0.8f));
            }
            else
            {
                on = true;
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
