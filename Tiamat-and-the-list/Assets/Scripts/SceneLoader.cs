using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public string sceneName = "";
    public float loadingTime = 3.0f;
	private string currentSceneName;
	private AsyncOperation operation;
	private bool loading = false;
	private float timer = 0;

    public GameObject loadingObject;
	// Use this for initialization
	void Awake () {
		//DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0){
			timer -= Time.deltaTime;
            Debug.Log(timer);
			if (timer <= 0){
				operation.allowSceneActivation = true;
				SceneManager.UnloadSceneAsync(currentSceneName);
				timer = 0;
			}
		}
	}

	public void LoadScene(string name){
		sceneName = name;
		currentSceneName = SceneManager.GetActiveScene().name;
		Debug.Log(currentSceneName);
        GameObject obj = Instantiate(loadingObject) as GameObject;
        obj.GetComponent<Loading>().targetTime = loadingTime + 0.5f;

        StartCoroutine(AsyncLoadingScene());
		timer = loadingTime;
	}

	IEnumerator AsyncLoadingScene(){
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        loading = true;
        yield return null;
    }
}
