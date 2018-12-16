using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingUIManager : MonoBehaviour {

    public RectTransform mainLeftTrans;
    public GameObject noteSection;
    public GameObject collectionSection;
    public GameObject cgSection;
    public GameObject musicSection;

    public Slider backgroundMusicVolumeSlider;
    public Slider effectMusicVolumeSlider;
    public Slider typingSpeedSlider;

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

        backgroundMusicVolumeSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume", 1.0f);
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.backgroundMusicVolume = backgroundMusicVolumeSlider.value;

        effectMusicVolumeSlider.value = PlayerPrefs.GetFloat("EffectMusicVolume", 1.0f);
        audioManager.effectSoundVolume = effectMusicVolumeSlider.value;

        typingSpeedSlider.value = 1 / PlayerPrefs.GetFloat("TypingSpeed", 0.04f);
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

    public void OnBackgroundMusicVolumeChanged()
    {
        PlayerPrefs.SetFloat("BackgroundMusicVolume", backgroundMusicVolumeSlider.value);
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.backgroundMusicVolume = backgroundMusicVolumeSlider.value;
        PlayerPrefs.Save();
    }

    public void OnEffectMusicVolumeChanged()
    {
        PlayerPrefs.SetFloat("EffectMusicVolume", effectMusicVolumeSlider.value);
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.effectSoundVolume = effectMusicVolumeSlider.value;
        PlayerPrefs.Save();
    }

    public void OnTypingSpeedChanged()
    {
        PlayerPrefs.SetFloat("TypingSpeed", 1 / typingSpeedSlider.value);
        GameObject dialogManager = GameObject.Find("DialogController");
        if (dialogManager != null && dialogManager.GetComponent<DialogManager>() != null)
        {
            dialogManager.GetComponent<DialogManager>().textSpeed = 1 / typingSpeedSlider.value;
        }
        PlayerPrefs.Save();
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
