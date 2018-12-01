using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Door : Interoperable
{
    
    public string nextSceneName = "";
    public string dialogSection;
    public bool test;
    private float hintAlpha = 0f;
    private bool showHint = false;
    private bool sceneChanged = false;
    
    public AudioClip audioDoor;
    private AudioSource audioSource;
    private GameObject doorAudio;
    

    // Use this for initialization
    void Start()
    {
        InputManager.AddOnInteract(OnInteract);
        InputManager.AddOnPick(OnPick);
        doorAudio = GameObject.Find("DoorAudio");
        if (doorAudio == null) { 
            doorAudio = new GameObject();
            doorAudio.name = "DoorAudio";
            audioSource = doorAudio.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioSource);
            audioSource.clip = audioDoor;
        } else
        {
            audioSource = doorAudio.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showHint && hintAlpha < 1.0f)
        {
            hintAlpha += Time.deltaTime * 4;
            if (hintAlpha > 1.0f)
                hintAlpha = 1.0f;
            hintRender.color = new Color(hintRender.color.r, hintRender.color.g, hintRender.color.b, hintAlpha);
        }
        if (!showHint && hintAlpha > 0f)
        {
            hintAlpha -= Time.deltaTime * 4;
            if (hintAlpha < 0f)
                hintAlpha = 0.0f;
            hintRender.color = new Color(hintRender.color.r, hintRender.color.g, hintRender.color.b, hintAlpha);
        }
        if (sceneChanged && !audioSource.isPlaying) 
            Destroy(this.gameObject);
    }
    void OnInteract()
    {
        if (NearPlayer)
        {
            DialogManager.ShowDialog(dialogSection);
        }
    }
    void OnPick()
    {
        if (NearPlayer)
        {
            audioSource.Play();
            SceneItemManager.SaveArchive();
            //先不管这些，本来想试试这样能不能做加载页面，结果资源太少了闪过去了，先放着吧——NA
            SceneManager.LoadScene("Loading");
            StartCoroutine(LoadAnotherScene(nextSceneName));
        }
    }

    IEnumerator LoadAnotherScene(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        yield return asyncOperation;
    }

    public SpriteRenderer hintRender;

    public override void ShowHint()
    {
        showHint = true;
    }
    public override void UnshowHint()
    {
        showHint = false;
    }
}
