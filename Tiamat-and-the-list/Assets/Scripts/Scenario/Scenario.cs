using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//与剧情相关的一次性提示基类

public class Scenario : MonoBehaviour {

    //是否出现剧情相关提示阻隔交互
    [HideInInspector]
    public bool scenarioHintOn = false;

    public virtual string GetArchive()
    {
        return null;
    }

    public virtual void LoadArchive(string archiveLine)
    {

    }

    //得到角色的初始位置，其实这个大约不应该由Scenario来管理，但给player估计要写一个editor上的才比较好。
    //调用此方法前首先确保当前场景和上一场景不同（不是存档加载，而是流程加载）
    public virtual Vector3 getPlayerInitPos(string lastSceneName)
    {
        return Vector3.zero;
    }
}
