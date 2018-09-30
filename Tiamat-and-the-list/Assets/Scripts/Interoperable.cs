using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interoperable : MonoBehaviour {
    
    //判定在主角附近的检测范围，交错按比例计算
    public int detectDist = 2;

    //是否在主角附近
    private bool nearPlayer = false;
    public bool NearPlayer { get; set; }

	public string GetArchive()
    {
        return null;
    }

    public void LoadArchive(string archiveLine)
    {
        
    }

    //显示提示交互，例如浮现一个UI：标有F什么的
    public void ShowHint()
    {

    }

    //提示交互消失
    public void UnshowHint()
    {

    }
}
