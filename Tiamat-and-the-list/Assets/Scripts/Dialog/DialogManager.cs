using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;
    public GameObject BranchPrefab;
    public GameObject DialogPrefab;
    public GameObject DialogBox;               // 对话框+立绘整体
    public float textSpeed = 0.04f;            // 文字显示速度
    
    private Dialog currentDialog;              // 当前对话
    private int id;                            // temp 测试用
    private DialogLoader loader;
    private List<Dialog> currentDialogSection; // 当前加载的section
    
    private List<GameObject> branchButtons;    // 用于加载分支选项的按钮

    private string tempDialog;                 // 逐字显示用
    private bool dialogFlag;                   // 判断是否在逐字显示
    private float timer;
    private bool animationLock;                // 在播放特定动画的时候锁死交互
    
    private bool branchLock;                   // 在出现选项支的时候锁死交互
    private float pauseTime = 0f;              // 用于会话停顿
    private int soundIndex = 0;

    public AudioClip typingSound;
    private AudioSource audioSource;

    //委托，当对话结束时调用
    public delegate void NoneParaVoid();
    private NoneParaVoid OnEnd;

    public static bool IsDialogOn()
    {
        return instance._IsDialogOn();
    }
    private bool _IsDialogOn()
    {
        return DialogBox != null;
    }

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = typingSound;
    }

    //显示对话栏
    public static void ShowDialog(string section)
    {
        instance.InitDialog(section);
    }
    public static void ShowDialog(string section, NoneParaVoid OnEnd)
    {
        instance.OnEnd += OnEnd;
        instance.InitDialog(section);
    }
    public void InitDialog(string section)
    {
        if (DialogBox == null)
        {
            currentDialogSection = new List<Dialog>();
            foreach (Dialog d in loader.context)
            {
                if (d.section == section)
                    currentDialogSection.Add(d);
            }
            if (currentDialogSection.Count == 0)
                return;
            DialogBox = Instantiate(DialogPrefab) as GameObject;
            currentDialog = currentDialogSection[0];
            id = 0;
            dialogFlag = false;
            branchLock = false;
            DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().text = "";
            DialogBox.transform.Find("DialogPanel").Find("DialogText").GetComponent<Text>().text = "";
            Image characterImage = DialogBox.transform.Find("Character").GetComponent<Image>();
            characterImage.color = new Color(1, 1, 1, 0);
            animationLock = true;
            StartCoroutine(initializeAnimation());
        }
    }

    private void displayDialog(Dialog dialog)
    {
        Image characterImage = DialogBox.transform.Find("Character").GetComponent<Image>();
        if (dialog.imagePath == "0")
        {
            characterImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            characterImage.color = new Color(1, 1, 1, 1);
            Sprite sp = Resources.Load("CharacterTachie\\" + dialog.imagePath, typeof(Sprite)) as Sprite;
            characterImage.sprite = sp;
        }
        tempDialog = "";
        timer = 0;
        dialogFlag = true;

        if (currentDialog.branchNum > 0)
        {
            branchLock = true;
            initBranches();
        }
    }

    public void DestoryDialog()
    {
        if (DialogBox != null) {
            StartCoroutine(destroyAnimation());
        }
    }

    public bool IsEmptyDialog()
    {
        if (GameObject.Find("Dialogbox(Clone)"))
        {

            return false;
        }
        else
        {
            return true;
        }

    }

    private void initBranches()
    {
        int width = Screen.width;
        int height = Screen.height;
        branchLock = true;
        branchButtons = new List<GameObject>();
        for (int i = 0; i < currentDialog.branchNum; i++) {
            GameObject btn = Instantiate(BranchPrefab) as GameObject;
            btn.transform.position = new Vector3(width/2, Mathf.Lerp(height * 0.7f, height * 0.5f, i*1.0f / (currentDialog.branchNum - 1)), 0);
            Color c = btn.GetComponent<Image>().color;
            btn.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0);
            btn.GetComponent<Branch>().switch_section = currentDialog.branches[i].switch_section;
            btn.GetComponent<Branch>().text = currentDialog.branches[i].text;

            btn.transform.Find("Text").GetComponent<Text>().text = currentDialog.branches[i].text;
            c = btn.transform.Find("Text").GetComponent<Text>().color;
            btn.transform.Find("Text").GetComponent<Text>().color = new Color(c.r, c.g, c.b, 0);

            btn.GetComponent<Button>().onClick.AddListener(BranchOnClick);

            btn.transform.SetParent(DialogBox.transform);
            branchButtons.Add(btn);
        }
        StartCoroutine(initializeBranchAnimation());
    }

    private void BranchOnClick()
    {
        currentDialogSection = new List<Dialog>();
        foreach (Dialog d in loader.context)
        {
            GameObject buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (d.section == buttonSelf.GetComponent<Branch>().switch_section)
                currentDialogSection.Add(d);
        }
        id = -1;

        StartCoroutine(destroyBranchAnimation());

        setNextDialog();
        branchLock = false;
    }
    void Start()
    {
        InputManager.AddOnNextDialog(OnNextDialog);
        loader = new DialogLoader();
        loader.loadData();
        //initDialog("Scene1");

        textSpeed = PlayerPrefs.GetFloat("TypingSpeed", 0.04f);
    }

    void Update()
    {
        if (DialogBox != null) {
            var s = GameObject.Find("SpecialTextSound");
            if (s != null && !s.GetComponent<AudioSource>().isPlaying)
                Destroy(s.gameObject);
            if (tempDialog == currentDialog.text)
            {
                dialogFlag = false;
            }
            if (dialogFlag)
            {
                if (pauseTime > 0f)
                {
                    pauseTime -= Time.deltaTime;
                } else { 
                    Text dialogText = DialogBox.transform.Find("DialogPanel").Find("DialogText").GetComponent<Text>();
                    if (timer > textSpeed)
                    {
                        timer = 0;
                        if (currentDialog.text[tempDialog.Length] == '#')
                            pauseTime = 0.3f;
                        tempDialog = currentDialog.text.Substring(0, tempDialog.Length + 1);
                    }
                    dialogText.text = tempDialog.Replace("#","").Replace("$","");
                    timer += Time.deltaTime;
                   
                    // 播放音效
                    if (!audioSource.isPlaying && (tempDialog.Length == 0 || (tempDialog.Length > 0 && tempDialog[tempDialog.Length-1]!='#')))
                    {
                        audioSource.Play();
                    }

                    // 播放特定音效
                    if (tempDialog.Length > 0 && tempDialog[tempDialog.Length - 1] == '$' && soundIndex < currentDialog.sounds.Count)
                    {
                        GameObject specialSound = new GameObject();
                        specialSound.name = "SpecialTextSound";
                        AudioSource specialAudio = specialSound.AddComponent<AudioSource>();
                        AudioClip specialClip = Resources.Load<AudioClip>("DynamicAudios\\" + currentDialog.sounds[soundIndex]);
                        soundIndex++;
                        specialAudio.clip = specialClip;
                        specialAudio.Play();
                    }
                }
            }
        }
    }

    private IEnumerator initializeAnimation()
    {
        Sprite initialSprite = Resources.Load("CharacterTachie\\" + currentDialog.imagePath, typeof(Sprite)) as Sprite;
        DialogBox.transform.Find("Character").GetComponent<Image>().sprite = initialSprite;
        Vector3 targetNamePanelPosition = DialogBox.transform.Find("NamePanel").transform.position;
        Vector3 targetDialogPanelPosition = DialogBox.transform.Find("DialogPanel").transform.position;
        Color targetNamePanelColor = DialogBox.transform.Find("NamePanel").GetComponent<Image>().color;
        Color targetDialogPanelColor = DialogBox.transform.Find("DialogPanel").GetComponent<Image>().color;
        Vector3 targetCharacterPosition = DialogBox.transform.Find("Character").transform.position;
        Color targetCharacterColor = DialogBox.transform.Find("Character").GetComponent<Image>().color;
        for (float t = 0; t <= 1f; t += 0.05f)
        {
            DialogBox.transform.Find("NamePanel").transform.position = Vector3.Lerp(targetNamePanelPosition - new Vector3(0, 20f, 0), targetNamePanelPosition, EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("DialogPanel").transform.position = Vector3.Lerp(targetDialogPanelPosition - new Vector3(0, 20f, 0), targetDialogPanelPosition, EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("Character").transform.position = Vector3.Lerp(targetCharacterPosition - new Vector3(40f, 0, 0), targetCharacterPosition, EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("NamePanel").GetComponent<Image>().color = Color.Lerp(new Color(targetNamePanelColor.r, targetNamePanelColor.g, targetNamePanelColor.b, 0), targetNamePanelColor, EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("DialogPanel").GetComponent<Image>().color = Color.Lerp(new Color(targetDialogPanelColor.r, targetDialogPanelColor.g, targetDialogPanelColor.b, 0), targetDialogPanelColor, EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("Character").GetComponent<Image>().color = Color.Lerp(new Color(targetCharacterColor.r, targetCharacterColor.g, targetCharacterColor.b, 0), targetCharacterColor, EasingFuncs.QuartInOut(t));
            yield return null;
            yield return new WaitForSeconds(0.03f);
        }
        StartCoroutine(nameAnimation("", currentDialog.characterName));
        displayDialog(currentDialog);
    }

    private IEnumerator nameAnimation(string name1, string name2)
    {
        GameObject replacedName = Instantiate(DialogBox.transform.Find("NamePanel").Find("NameText").gameObject) as GameObject;
        replacedName.transform.position = DialogBox.transform.Find("NamePanel").Find("NameText").position - new Vector3(20f, 0, 0);
        replacedName.transform.localScale = DialogBox.transform.localScale;
        replacedName.GetComponent<Text>().text = name2;
        Color c1 = DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().color;
        Color c2 = replacedName.GetComponent<Text>().color;
        c2 = new Color(c2.r, c2.g, c2.b, 0f);
        replacedName.GetComponent<Text>().color = c2;
        replacedName.transform.SetParent(DialogBox.transform.Find("NamePanel"));

        Vector3 targetNameTextPosition = DialogBox.transform.Find("NamePanel").Find("NameText").position + new Vector3(20f, 0, 0);
        Vector3 targetReplacedNamePosition = DialogBox.transform.Find("NamePanel").Find("NameText").position;

        for (float t = 0; t <= 1f; t += 0.05f)
        {
            DialogBox.transform.Find("NamePanel").Find("NameText").transform.position = Vector3.Lerp(targetNameTextPosition - new Vector3(20f, 0, 0), targetNameTextPosition, EasingFuncs.QuartInOut(t));
            replacedName.transform.position = Vector3.Lerp(targetReplacedNamePosition - new Vector3(20f, 0, 0), targetReplacedNamePosition, EasingFuncs.QuartInOut(t));
            
            DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().color = Color.Lerp(c1, new Color(c1.r, c1.g, c1.b, 0f), EasingFuncs.QuartInOut(t));
            replacedName.GetComponent<Text>().color = Color.Lerp(c2, new Color(c2.r, c2.g, c2.b, 1f), EasingFuncs.QuartInOut(t));
            yield return null;//下一帧继续执行for循环
            yield return new WaitForSeconds(0.006f);//0.006秒后继续执行for循环
        }
        DialogBox.transform.Find("NamePanel").Find("NameText").transform.position -= new Vector3(20f, 0, 0);
        DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().color = new Color(c1.r, c1.g, c1.b, 1.0f);
        DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().text = name2;
        Destroy(replacedName);
        animationLock = false;
    }

    private IEnumerator destroyAnimation()
    {
        Vector3 targetNamePanelPosition = DialogBox.transform.Find("NamePanel").transform.position;
        Vector3 targetDialogPanelPosition = DialogBox.transform.Find("DialogPanel").transform.position;
        Color targetNamePanelColor = DialogBox.transform.Find("NamePanel").GetComponent<Image>().color;
        Color targetDialogPanelColor = DialogBox.transform.Find("DialogPanel").GetComponent<Image>().color;
        Vector3 targetCharacterPosition = DialogBox.transform.Find("Character").transform.position;
        Color targetCharacterColor = DialogBox.transform.Find("Character").GetComponent<Image>().color;
        Vector3 targetNameTextPosition = DialogBox.transform.Find("NamePanel").Find("NameText").transform.position;
        Color targetNameTextColor = DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().color;
        Vector3 targetDialogTextPosition = DialogBox.transform.Find("DialogPanel").Find("DialogText").transform.position;
        Color targetDialogTextColor = DialogBox.transform.Find("DialogPanel").Find("DialogText").GetComponent<Text>().color;

        for (float t = 0; t <= 1f; t += 0.05f)
        {
            DialogBox.transform.Find("NamePanel").transform.position = Vector3.Lerp(targetNamePanelPosition, targetNamePanelPosition - new Vector3(0, 20f, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("DialogPanel").transform.position = Vector3.Lerp(targetDialogPanelPosition, targetDialogPanelPosition - new Vector3(0, 20f, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("Character").transform.position = Vector3.Lerp(targetCharacterPosition, targetCharacterPosition + new Vector3(40f, 0, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("NamePanel").Find("NameText").transform.position = Vector3.Lerp(targetNameTextPosition, targetNameTextPosition - new Vector3(0, 20f, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("DialogPanel").Find("DialogText").transform.position = Vector3.Lerp(targetDialogTextPosition, targetDialogTextPosition - new Vector3(0, 20f, 0), EasingFuncs.QuartInOut(t));

            DialogBox.transform.Find("NamePanel").GetComponent<Image>().color = Color.Lerp(targetNamePanelColor, new Color(targetNamePanelColor.r, targetNamePanelColor.g, targetNamePanelColor.b, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("DialogPanel").GetComponent<Image>().color = Color.Lerp(targetDialogPanelColor, new Color(targetDialogPanelColor.r, targetDialogPanelColor.g, targetDialogPanelColor.b, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("Character").GetComponent<Image>().color = Color.Lerp(targetCharacterColor, new Color(targetCharacterColor.r, targetCharacterColor.g, targetCharacterColor.b, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("NamePanel").Find("NameText").GetComponent<Text>().color = Color.Lerp(targetNameTextColor, new Color(targetNameTextColor.r, targetNameTextColor.g, targetNameTextColor.b, 0), EasingFuncs.QuartInOut(t));
            DialogBox.transform.Find("DialogPanel").Find("DialogText").GetComponent<Text>().color = Color.Lerp(targetDialogTextColor, new Color(targetDialogTextColor.r, targetDialogTextColor.g, targetDialogTextColor.b, 0), EasingFuncs.QuartInOut(t));

            yield return null;
            yield return new WaitForSeconds(0.03f);
        }

        Destroy(DialogBox);
        DialogBox = null;

        //调用对话结束时委托
        if (OnEnd != null)
        {
            OnEnd();
            OnEnd = null;
        }
    }

    private IEnumerator initializeBranchAnimation()
    {
        for (int i = 0; i < branchButtons.Count; i++)
        {
            GameObject currentButton = branchButtons[i];
            StartCoroutine(initializeSingleBranchAnimation(currentButton));
            yield return null;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator initializeSingleBranchAnimation(GameObject currentButton)
    {
        Vector3 targetPos = currentButton.transform.position;
        Color targetColor = currentButton.GetComponent<Image>().color;
        Color targetTextColor = currentButton.transform.Find("Text").GetComponent<Text>().color;
        for (float t=0; t<=1f; t += 0.05f)
        {
            currentButton.transform.position = Vector3.Lerp(targetPos - new Vector3(20f, 0, 0), targetPos, EasingFuncs.QuartInOut(t));
            currentButton.GetComponent<Image>().color = Color.Lerp(targetColor, new Color(targetColor.r, targetColor.g, targetColor.b, 0.5f), EasingFuncs.QuartInOut(t));
            currentButton.transform.Find("Text").GetComponent<Text>().color = Color.Lerp(targetTextColor, new Color(targetTextColor.r, targetTextColor.g, targetTextColor.b, 1), EasingFuncs.QuartInOut(t));
            yield return null;
            yield return new WaitForSeconds(0.006f);
        }
    }

    private IEnumerator destroyBranchAnimation()
    {
        for (int i = 0; i < branchButtons.Count; i++)
        {
            GameObject currentButton = branchButtons[i];
            StartCoroutine(destroySingleBranchAnimation(currentButton));
            yield return null;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator destroySingleBranchAnimation(GameObject currentButton)
    {
        Vector3 targetPos = currentButton.transform.position;
        Color targetColor = currentButton.GetComponent<Image>().color;
        Color targetTextColor = currentButton.transform.Find("Text").GetComponent<Text>().color;
        for (float t = 0; t <= 1f; t += 0.05f)
        {
            currentButton.transform.position = Vector3.Lerp(targetPos, targetPos + new Vector3(20f, 0, 0), EasingFuncs.QuartInOut(t));
            currentButton.GetComponent<Image>().color = Color.Lerp(targetColor, new Color(targetColor.r, targetColor.g, targetColor.b, 0), EasingFuncs.QuartInOut(t));
            currentButton.transform.Find("Text").GetComponent<Text>().color = Color.Lerp(targetTextColor, new Color(targetTextColor.r, targetTextColor.g, targetTextColor.b, 0), EasingFuncs.QuartInOut(t));
            yield return null;
            yield return new WaitForSeconds(0.006f);
        }
        Destroy(currentButton);
    }

    private void setNextDialog()
    {
        string name1 = currentDialog.characterName;
        id++;
        soundIndex = 0;
        if (id >= currentDialogSection.Count)
        {
            animationLock = true;
            DestoryDialog();
            return;
        }
        currentDialog = currentDialogSection[id];
        if (name1 != currentDialog.characterName)
        {
            animationLock = true;
            StartCoroutine(nameAnimation(name1, currentDialog.characterName));
        }
        displayDialog(currentDialog);
    }

    private void OnNextDialog()
    {
        if (!branchLock &&!animationLock && DialogBox != null) {
            // 如果当前文字已经全部出现，则进入下一句
            // 否则将当前这句话直接显示出来
            if (tempDialog == currentDialog.text) {
                setNextDialog();
            }
            else { 
                tempDialog = currentDialog.text;
                Text dialogText = DialogBox.transform.Find("DialogPanel").Find("DialogText").GetComponent<Text>();
                dialogText.text = tempDialog.Replace("#", "").Replace("$", "");
            }
        }
    }
}