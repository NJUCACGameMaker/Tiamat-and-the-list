using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tran_77 : Interoperable {
    
    public string nextSceneName;
    public bool triggered = false;

    public override void WithinRange()
    {
        if (!triggered)
        {
            SceneItemManager.SaveArchive();
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(nextSceneName);
            triggered = true;
        }
    }
}
