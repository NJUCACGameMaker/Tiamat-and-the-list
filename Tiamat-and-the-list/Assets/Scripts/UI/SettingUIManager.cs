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

    public RectTransform mainRightTrans;

    private RectTransform currentLeft;
    private RectTransform currentRight;

    private void Start()
    {
        currentLeft = mainLeftTrans;
        currentRight = mainRightTrans;
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

    public void BackMain()
    {
        ShowSectionLeft(mainLeftTrans);
        ShowSectionRight(mainRightTrans);
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

    public void ShowSectionRight(RectTransform section)
    {
        var tempPos = section.position;
        section.position = currentRight.position;
        currentRight.position = tempPos;
        currentRight = section;
    }

}
