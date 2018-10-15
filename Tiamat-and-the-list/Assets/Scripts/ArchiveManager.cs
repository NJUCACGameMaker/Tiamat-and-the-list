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
    private readonly string archiveName;
    //存档关卡标志（关卡名）
    private readonly string levelTag;
    //存档场景标志（场景名）
    private readonly string sceneTag;

    //构造函数，构造一个对应场景的存档管理器
    public ArchiveManager(string archiveName, string levelTag, string sceneTag)
    {
        this.archiveName = archiveName;
        this.levelTag = levelTag;
        this.sceneTag = sceneTag;
    }

    //存档加载，参数为场景内所有初始布局的列表，根据存档修正场景布局，生成额外物品。
    public void LoadArchive(List<Interoperable> interoperables)
    {
        try
        {
            FileStream archiveFile = new FileStream(Application.persistentDataPath + "\\" + archiveName + ".json", FileMode.Open);
            JSONNode root = JSON.Parse(archiveFile.ToString());
            JSONNode sceneNode = root[levelTag][sceneTag];

            foreach (JSONNode itemNode in sceneNode.Childs)
            {
                int index = itemNode["index"].AsInt;
                string archiveLine = itemNode["archive"];
                if (index != -1 && index < interoperables.Count)
                {
                    interoperables[index].LoadArchive(archiveLine);
                }
                else if (index == -1)
                {
                    string resourcesPath = itemNode["resources-path"];
                    GameObject pickableItem = Object.Instantiate(Resources.Load(resourcesPath)) as GameObject;
                    pickableItem.GetComponent<Interoperable>().LoadArchive(archiveLine);
                    pickableItem.GetComponent<Interoperable>().generated = true;
                }
            }
        }
        catch (IOException e)
        {
            Debug.Log("Archive Doesn't Exist: " + e.HelpLink);
        }
    }

    //保存存档
    public void SaveArchive(List<GameObject> objectsToSave)
    {
        var sceneArchive = new JSONArray();
        for (int i = 0; i < objectsToSave.Count; i++)
        {
            Interoperable interoperable = objectsToSave[i].GetComponent<Interoperable>();
            if (!interoperable.generated)
            {
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
            else
            {
                Pickable pickable = interoperable as Pickable;
                if (pickable != null)
                {
                    var archivePiece = new JSONClass
                    {
                        { "index", new JSONData(-1) },
                        { "resources-path", new JSONData(pickable.resourcesPath) },
                        { "archive", new JSONData(interoperable.GetArchive()) }
                    };
                }
            }
        }

        FileStream archiveFile = new FileStream(Application.persistentDataPath + "\\" + archiveName + ".json", FileMode.OpenOrCreate);
        JSONNode levelNode = JSON.Parse(archiveFile.ToString())[levelTag];

        levelNode.Remove(sceneTag);
        levelNode.Add(sceneArchive);
    }

}
