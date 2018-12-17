using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUIManager : MonoBehaviour {

    public RectTransform mainLeftTrans;
    public GameObject noteSection;
    public GameObject collectionSection;
    public GameObject cgSection;
    public GameObject musicSection;

    public RectTransform mainRightTrans;

    private RectTransform currentLeft;
    private RectTransform currentRight;

    public GameObject backgroundMusic;

    private void Start()
    {
        currentLeft = mainLeftTrans;
        currentRight = mainRightTrans;

        var noteList = noteSection.transform.Find("Panel").Find("Content Panel").GetComponent<ScrollListManager>();
        foreach (NotePiece notePiece in CollectionArchive.GetNotes())
        {
            noteList.AddNoteButton(notePiece.shortLine, notePiece.detail);
        }

        var collectionList = collectionSection.transform.Find("Panel").Find("Content Panel").GetComponent<ScrollListManager>();
        foreach (CollectionPiece collectionPiece in CollectionArchive.GetCollections())
        {
            collectionList.AddCollectionButton(collectionPiece.shortLine, collectionPiece.detail, collectionPiece.picPath);
        }

        var cgList = cgSection.transform.Find("Panel").Find("Content Panel").GetComponent<ScrollListManager>();
        foreach (CGPiece cgPiece in CollectionArchive.GetCGs())
        {
            cgList.AddCGButton(cgPiece.shortLine, cgPiece.picPath);
        }

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game();
        }
    }

    public void ShowNote()
    {
        ShowSectionLeft(noteSection.GetComponent<RectTransform>());
    }

    public void ShowCollection()
    {
        ShowSectionLeft(collectionSection.GetComponent<RectTransform>());
    }

    public void ShowCG()
    {
        ShowSectionLeft(cgSection.GetComponent<RectTransform>());
    }

    public void ShowMusic()
    {
        ShowSectionLeft(musicSection.GetComponent<RectTransform>());
    }

    public void BackMain()
    {
        ShowSectionLeft(mainLeftTrans);
        ShowSectionRight(mainRightTrans);
    }

    public void Title()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
        Debug.Log(backgroundMusic.name);
        if (backgroundMusic.name != "BackgroundMusic_Cover")
            backgroundMusic.GetComponent<BackgroundAudioManager>().SceneChange();
        SceneItemManager.SaveArchive();
        GameObject sceneManager = GameObject.Find("SceneController");
        if (sceneManager != null)
        {
            sceneManager.GetComponent<SceneItemManager>().Resume();
        }
        SceneManager.LoadScene("Cover");
    }

    public void Game()
    {
        GameObject sceneManager = GameObject.Find("SceneController");
        if (sceneManager != null)
        {
            sceneManager.GetComponent<SceneItemManager>().Resume();
        }
        SceneManager.UnloadSceneAsync("Setting");
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
