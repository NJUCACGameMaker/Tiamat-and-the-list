using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollListManager : MonoBehaviour {

    //预制体
    public GameObject pureTextBtn;
    public GameObject picTextBtn;

    public SettingUIManager mainUIManager;
    public GameObject noteDetail;
    public GameObject collectionDetail;
    public GameObject cgDetail;

    private VerticalLayoutGroup layoutGroup;
    //内容
    private List<Button> contents;
    //这个layout的最高度，通过计算得出。
    private float height = 0.0f;
    //宽度，根据layout得到
    private float width = 750.0f;
    //两个组件之间的空隙大小
    private float space = 0.0f;

	// Use this for initialization
	void Awake () {
        width = GetComponent<RectTransform>().rect.width;
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        space = layoutGroup.spacing;
	}

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Add(GameObject btnToShow)
    {
        RectTransform rectTrans = btnToShow.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(width, rectTrans.sizeDelta.y);

        height += rectTrans.sizeDelta.y;
        btnToShow.transform.SetParent(gameObject.transform, false);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void AddNoteButton(string shortLine, string detail)
    {
        GameObject textBtnInstance = Instantiate(pureTextBtn) as GameObject;
        if (textBtnInstance != null)
        {
            textBtnInstance.transform.Find("Text").GetComponent<Text>().text = shortLine;
            textBtnInstance.GetComponent<Button>().onClick.AddListener(() => PureTextOnClick(detail));
            Add(textBtnInstance);
        }
    }

    public void PureTextOnClick(string detail)
    {
        mainUIManager.ShowSectionRight(noteDetail.GetComponent<RectTransform>());
        noteDetail.transform.Find("Panel").Find("Text").GetComponent<Text>().text = detail;
    }

    public void AddCollectionButton(string shortLine, string detail, string picPath)
    {
        GameObject picTextBtnInstance = Instantiate(picTextBtn) as GameObject;
        if (picTextBtnInstance != null)
        {
            picTextBtnInstance.transform.Find("Text").GetComponent<Text>().text = shortLine;
            Sprite sprite = Resources.Load<Sprite>(picPath) as Sprite;
            picTextBtnInstance.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            picTextBtnInstance.transform.Find("Image").GetComponent<ThumbnailPicAdjust>().Adjust();
            picTextBtnInstance.GetComponent<Button>().onClick.AddListener(() => PicTextOnClick(detail, sprite));
            Add(picTextBtnInstance);
        }
    }

    public void PicTextOnClick(string detail, Sprite picSprite)
    {
        mainUIManager.ShowSectionRight(collectionDetail.GetComponent<RectTransform>());
        collectionDetail.transform.Find("Panel").Find("Content Panel").Find("Text").GetComponent<Text>().text = detail;
        collectionDetail.transform.Find("Panel").Find("Content Panel").Find("Image").GetComponent<Image>().sprite = picSprite;
    }
    
    public void AddCGButton(string shortLine, string picPath)
    {
        GameObject picTextBtnInstance = Instantiate(picTextBtn) as GameObject;
        if (picTextBtnInstance != null)
        {
            picTextBtnInstance.transform.Find("Text").GetComponent<Text>().text = shortLine;
            Sprite sprite = Resources.Load<Sprite>(picPath) as Sprite;
            picTextBtnInstance.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            picTextBtnInstance.transform.Find("Image").GetComponent<ThumbnailPicAdjust>().Adjust();
            picTextBtnInstance.GetComponent<Button>().onClick.AddListener(() => PicOnClick(sprite));
            Add(picTextBtnInstance);
        }
    }

    public void PicOnClick(Sprite picSprite)
    {
        mainUIManager.ShowSectionRight(cgDetail.GetComponent<RectTransform>());
        cgDetail.transform.Find("Panel").Find("Image").GetComponent<Image>().sprite = picSprite;
    }

    public void AddMusicButton(string shortLine, string musicPath)
    {
        GameObject pureTextBtnInstance = Instantiate(pureTextBtn) as GameObject;
        if (pureTextBtnInstance != null)
        {
            pureTextBtnInstance.transform.Find("Text").GetComponent<Text>().text = shortLine;
            pureTextBtnInstance.GetComponent<Button>().onClick.AddListener(() => MusicOnClick(musicPath));
            Add(pureTextBtnInstance);
        }
    }

    public void MusicOnClick(string musicFileName)
    {
        GameObject bgm = GameObject.FindGameObjectWithTag("BackgroundMusic");
        bgm.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(musicFileName);
        bgm.GetComponent<AudioSource>().Play();
    }

}
