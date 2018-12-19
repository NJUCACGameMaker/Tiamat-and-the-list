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
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(nextSceneName);
        }
    }
    
}
