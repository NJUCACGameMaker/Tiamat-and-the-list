using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

/*
 * 存档管理器，保存每个可交互道具的状态，具体道具存档格式由道具自己维护
 * 目前只存了道具，之后人物和持有道具形式出来后将修改。
 */

public class ArchiveManager {

    //存档文件名（不含.json）
    private string archiveName;
    //存档关卡标志（关卡名）
    private string levelTag;
    //存档场景标志（场景名）
    private string sceneTag;

    //构造函数，构造一个对应场景的存档管理器
    ArchiveManager(string archiveName, string levelTag, string sceneTag)
    {
        this.archiveName = archiveName;
        this.levelTag = levelTag;
        this.sceneTag = sceneTag;
    }

    //存档加载，返回一个index与存档内容的字典，若index存在而存档为空表示与关卡布置时道具状态无变化，若index不存在则表示该道具以因使用等
    //原因消失，与关卡布置时相比多余的index表示后续交互中增加的道具。
    Dictionary<int, string> LoadArchive()
    {
        var objectsToLoad = new Dictionary<int, string>();
        try
        {
            FileStream archiveFile = new FileStream(Application.persistentDataPath + "\\" + archiveName + ".json", FileMode.Open);
            JSONNode root = JSON.Parse(archiveFile.ToString());
            JSONNode sceneNode = root[levelTag][sceneTag];

            foreach (JSONNode itemNode in sceneNode.Childs)
            {
                objectsToLoad.Add(itemNode["index"].AsInt, itemNode["archive"]);
            }
        }
        catch (IOException e)
        {
            Debug.Log("Archive Doesn't Exist");
        }

        return objectsToLoad;
    }

    //保存存档
    void SaveArchive(List<GameObject> objectsToSave)
    {
        var sceneArchive = new JSONArray();
        for (int i = 0; i < objectsToSave.Count; i++)
        {
            Interoperable interoperable = objectsToSave[i].GetComponent<Interoperable>();
            if (interoperable.GetArchive() != null)
            {
                var archivePiece = new JSONClass
                {
                    { "index", new JSONData(interoperable.Index) },
                    { "archive", new JSONData(interoperable.GetArchive()) }
                };
                sceneArchive.Add(archivePiece);
            }
            else
            {
                var archivePiece = new JSONClass
                {
                    { "index", new JSONData(interoperable.Index) },
                    { "archive", new JSONData("") }
                };
                sceneArchive.Add(archivePiece);
            }
        }

        FileStream archiveFile = new FileStream(Application.persistentDataPath + "\\" + archiveName + ".json", FileMode.OpenOrCreate);
        JSONNode levelNode = JSON.Parse(archiveFile.ToString())[levelTag];

        levelNode.Remove(sceneTag);
        levelNode.Add(sceneArchive);
    }

}
