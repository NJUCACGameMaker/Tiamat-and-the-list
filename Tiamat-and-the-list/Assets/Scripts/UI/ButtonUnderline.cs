using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUnderline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private int state = 0;
    private bool onButton;
    private float lineLength, uiHeight;
    private float mouseOnTime;
    // 0 - maxT/2: anim1, maxT/2 - T: anim2
    private float maxTime = 1.2f;
    public GameObject UILinePrefab;
    private GameObject UILine;

    // Use this for initialization
    void Start()
    {
        onButton = false;
        //lineLength = gameObject.GetComponent<RectTransform>().rect.width;

        lineLength = CalculateLengthOfText(gameObject.GetComponentInChildren<Text>());

        UILine = Instantiate(UILinePrefab) as GameObject;
        UILine.transform.SetParent(gameObject.transform);
        UILine.name = "UILine";
        UILine.GetComponent<RectTransform>().localPosition = new Vector3(transform.GetComponent<RectTransform>().rect.width/2 - lineLength/2, -transform.GetComponent<RectTransform>().rect.height + 15, 0);
        UILine.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3);
        UILine.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
        
        mouseOnTime = 0;
    }

    void SetPivotLeft()
    {
        UILine.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        UILine.GetComponent<RectTransform>().localPosition = new Vector3(transform.GetComponent<RectTransform>().rect.width / 2 - lineLength / 2, -transform.GetComponent<RectTransform>().rect.height + 15, 0);
    }

    void SetPivotRight()
    {
        UILine.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
        UILine.GetComponent<RectTransform>().localPosition = new Vector3(transform.GetComponent<RectTransform>().rect.width / 2 + lineLength / 2, -transform.GetComponent<RectTransform>().rect.height + 15, 0);
    }

    void Update()
    {

        if (onButton)
        {
            if (mouseOnTime < maxTime / 2)
            {
                mouseOnTime += Time.deltaTime;
                if (mouseOnTime > maxTime / 2)
                    mouseOnTime = maxTime / 2;
                SetPivotLeft();
            }
            if (mouseOnTime > maxTime / 2)
            {
                mouseOnTime -= Time.deltaTime;
            }
        }
        
        if (!onButton && mouseOnTime != 0)
        {
            mouseOnTime += Time.deltaTime;
            if (mouseOnTime > maxTime)
                mouseOnTime = 0;
        }
        if (mouseOnTime < maxTime / 2)
            SetPivotLeft();
        else
            SetPivotRight();
        lineLength = CalculateLengthOfText(gameObject.GetComponentInChildren<Text>());
        if (mouseOnTime <= maxTime / 2)
            UILine.GetComponent<RectTransform>().sizeDelta = new Vector2(EasingFuncs.QuintInOut(mouseOnTime / (maxTime/2)) * lineLength, 5);
        else
            UILine.GetComponent<RectTransform>().sizeDelta = new Vector2(EasingFuncs.QuintInOut((maxTime - mouseOnTime) / ((maxTime / 2))) * lineLength, 5);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        onButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onButton = false;
    }

    public void OnPointerClick(PointerEventData data)
    {
        onButton = false;
        UILine.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }

    int CalculateLengthOfText(Text tex)
    {
        string message = tex.text;
        int totalLength = 0;
        Font myFont = tex.font;  //chatText is my Text component
        myFont.RequestCharactersInTexture(message, tex.fontSize, tex.fontStyle);
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = message.ToCharArray();

        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, tex.fontSize);

            totalLength += characterInfo.advance;
        }

        return totalLength;
    }
}
