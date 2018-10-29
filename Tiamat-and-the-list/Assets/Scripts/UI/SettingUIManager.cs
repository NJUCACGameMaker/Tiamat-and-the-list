using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUIManager : MonoBehaviour {

    public RectTransform mainLeftTrans;
    public RectTransform noteSectionTrans;
    public RectTransform collectionSectionTrans;
    public RectTransform cgSectionTrans;
    public RectTransform musicSectionTrans;

    private RectTransform currentLeft;

    private void Start()
    {
        currentLeft = mainLeftTrans;
    }

    public void ShowNote()
    {
        ShowSectionLeft(noteSectionTrans);
    }

    public void ShowCollection()
    {
        ShowSectionLeft(collectionSectionTrans);
    }

    public void ShowCG()
    {
        ShowSectionLeft(cgSectionTrans);
    }

    public void ShowMusic()
    {
        ShowSectionLeft(musicSectionTrans);
    }

    public void LeftMenu()
    {
        ShowSectionLeft(mainLeftTrans);
    }

    public void Title()
    {
        SceneManager.LoadScene("Cover");
    }

    private void ShowSectionLeft(RectTransform section)
    {
        var tempPos = section.position;
        section.position = currentLeft.position;
        currentLeft.position = tempPos;
        currentLeft = section;
    }

}
