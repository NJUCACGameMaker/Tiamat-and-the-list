using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public string sceneName = "";
    public float loadingTime = 1.0f;
	private string currentSceneName;
	private AsyncOperation operation;
	private bool loading = false;
	private float timer = 0;
	// Use this for initialization
	void Awake () {
		//DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0){
			timer -= Time.deltaTime;
			if (timer <= 0){
				operation.allowSceneActivation = true;
				SceneManager.UnloadSceneAsync("Loading");
				SceneManager.UnloadSceneAsync(currentSceneName);
				timer = 0;
			}
		}
	}

	public void LoadScene(string name){
		sceneName = name;
		currentSceneName = SceneManager.GetActiveScene().name;
		Debug.Log(currentSceneName);
		SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
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
