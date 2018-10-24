using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//与剧情相关的一次性提示基类

public class Scenario : MonoBehaviour {

    //是否出现剧情相关提示阻隔交互
    [HideInInspector]
    public bool scenarioHintOn = false;

    public string GetArchive()
    {
        return null;
    }

    public void LoadArchive(string archiveLine)
    {

    }
}
