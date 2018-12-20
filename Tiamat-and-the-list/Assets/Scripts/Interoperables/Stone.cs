using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Stone : Interoperable {

    public string dialogSection;
    public string nextSceneName = "";
    bool isdestroy = false;
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
            isdestroy = true;

            SceneItemManager.SaveArchive();
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(nextSceneName);
        }
    }

    IEnumerator LoadAnotherScene(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        yield return asyncOperation;
    }

    public override string GetArchive()
    {
        JSONClass archive = new JSONClass
        {
            { "stone", new JSONData(isdestroy) }
        };
        return archive.ToString();
    }
    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        var isdestroyNode = root["stone"];
        isdestroy = isdestroyNode.AsBool;
        if (isdestroy)
        {
            transform.position = new Vector3(25, 9, 0);
            interoperable = false;
        }
    }
}
