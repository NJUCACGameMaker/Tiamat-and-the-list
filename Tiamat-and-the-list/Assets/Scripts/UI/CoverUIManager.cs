using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoverUIManager : MonoBehaviour {

    public RectTransform newGameConfirmTrans;
    public Button continueButton;
    public GameObject backgroundMusic;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("HasArchive", 0) == 0)
        {
            continueButton.interactable = false;
        }
        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //新的游戏button
    public void NewGame()
    {
        if (PlayerPrefs.GetInt("HasArchive", 0) == 1)
        {
            newGameConfirmTrans.localPosition = Vector3.zero;
        }
        else
        {
            backgroundMusic.GetComponent<AudioManager>().SceneChange();
            SceneManager.LoadScene("Tutorial-Scene1");
        }
    }

    //继续游戏button
    public void ContinueGame()
    {
        backgroundMusic.GetComponent<AudioManager>().SceneChange();
        SceneManager.LoadScene(PlayerPrefs.GetString("LastSceneName", "Tutorial-Scene1"));
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }

    //退出游戏button
    public void Exit()
    {
        Application.Quit();
    }

    //确认新游戏button
    public void NewGameConfirmTrue()
    {
        File.Delete(Application.persistentDataPath + "\\" + "Normal-Archive.json");
        PlayerPrefs.SetInt("HasArchive", 0);
        PlayerPrefs.Save();
        backgroundMusic.GetComponent<AudioManager>().SceneChange();
        SceneManager.LoadScene("Tutorial-Scene1");
    }

    //退出游戏button
    public void NewGameConfirmFalse()
    {
        newGameConfirmTrans.localPosition = new Vector3(2000.0f, 0.0f, 0.0f);
    }

}
