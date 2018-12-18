using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stone : Interoperable {

    public string dialogSection;
    public string nextSceneName = "";
    // Use this for initialization
    void Start () {
        InputManager.AddOnInteract(OnInteract);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnInteract()
    {
        if (NearPlayer)
        {
            DialogManager.ShowDialog(dialogSection);
            interoperable = false;
            transform.position = new Vector3(25, 9, 0);

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
}
