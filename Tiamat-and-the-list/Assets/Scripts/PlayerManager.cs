using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using SimpleJSON;

public class PlayerManager : MonoBehaviour {

    public List<float> maxX;
    public List<float> minX;
    //剧本，作用是获得进入关卡后初始位置
    public Scenario scenario;

    //移动速度
    public float moveSpeed = 8.0f;
    //手电筒
    public GameObject torchPrefab;
    //判断是否使用道具
    bool itemOn = false;
    //当前道具
    private EquipmentType currentEquipType = EquipmentType.None;

    //高度层，最低为0，向上递增，用于判断是否与道具在同一层从而判断是否可交互。
    public int floorLayer = 0;

    //角色动画控制器
    public Animator playerAnima;

    private bool isLeft = false;
    // Use this for initialization
    void Start () {
        InputManager.AddOnLeftMove(LeftMove);
        InputManager.AddOnRightMove(RightMove);
        InputManager.AddOnSwitchItemState(UseEquip);
        InputManager.AddAfterMove(AfterMove);
        InputManager.AddBeforMove(BeforeMove);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
    void LeftMove()
    {
        float playerX = transform.localPosition.x;
        float bg_1_x = minX[floorLayer];
        //transform.LookAt(new Vector3(transform.position.x-5,transform.position.y,transform.position.z));
        if (!isLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isLeft = true;
        }
        if (playerX >= bg_1_x)
        {
            transform.Translate(Time.deltaTime * Vector3.left * moveSpeed, Space.World);
        }
        
    }

    void RightMove()
    {
        float playerX = transform.localPosition.x;
        float bg_1_x = maxX[floorLayer];
        //transform.LookAt(new Vector3(transform.position.x + 5, transform.position.y, transform.position.z));
        if (isLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isLeft = false;
        }
        if (playerX <= bg_1_x)
        {
            transform.Translate(Time.deltaTime * Vector3.right * moveSpeed, Space.World);
        }
        
    }
    
    void BeforeMove()
    {
        playerAnima.SetFloat("MoveSpeed", 1.0f);
    }

    void AfterMove()
    {
        playerAnima.SetFloat("MoveSpeed", 0f);
    }


    public void setEquip(EquipmentType equipmentType)
    {
        var existedTorch = transform.Find("Torch(Clone)");
        if (existedTorch != null)
        {
            Destroy(existedTorch.gameObject);
        }
        itemOn = true;
        switch (equipmentType) {
            case EquipmentType.FlashLight:
                currentEquipType = EquipmentType.FlashLight;
                GameObject torch = Instantiate(torchPrefab) as GameObject;
                if(!isLeft)
                    torch.transform.position = new Vector3(transform.position.x + 3.1f, transform.position.y, transform.position.z);
                else
                    torch.transform.position = new Vector3(transform.position.x - 3.1f, transform.position.y, transform.position.z);
                torch.transform.parent = transform;
            break;
        }
        UseEquip();
    }

    void UseEquip()
    {
        switch (currentEquipType) {
            case EquipmentType.FlashLight:
                if (itemOn)
                {
                    turnOffTorch();
                    itemOn = false;
                }
                else
                {

                    turnOnTorch();
                    itemOn = true;
                }
                break;
        }
    }

    void turnOnTorch()
    {
        Debug.Log(transform.childCount);
        transform.Find("Torch(Clone)").GetComponent<FlashLightEquipment>().TurnOnTorch();
    }
    void turnOffTorch()
    {
        transform.Find("Torch(Clone)").GetComponent<FlashLightEquipment>().TurnOffTorch();
    }

    private PlayerSave CreateSavePlayer()
    {
        PlayerSave save = new PlayerSave();
        save.x = transform.position.x;
        save.y = transform.position.y;
        save.z = transform.position.z;
        save.floorLayer = floorLayer;

        save.currentEquipType = currentEquipType;
        save.itemOn = itemOn;

        return save;
    }

    public void SavePlayer()
    {
        PlayerSave save = new PlayerSave();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/player.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Player Saved");
    }

    public void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        transform.position = new Vector3(root["position"][0].AsFloat, root["position"][1].AsFloat, root["position"][2].AsFloat);
        floorLayer = root["floorLayer"].AsInt;
        setEquip((EquipmentType)Enum.Parse(typeof(EquipmentType), root["currentEquipType"]));
        itemOn = root["itemOn"].AsBool;

        switch (currentEquipType)
        {
            case EquipmentType.FlashLight:
                UIManager.SetEquipmentIcon("EquipmentSprite\\Stage00_shoudiantong");
                break;
        }

        string lastSceneName = PlayerPrefs.GetString("LastSceneName");
        if (lastSceneName != SceneItemManager.GetLevelName() + "-" + SceneItemManager.GetSceneName())
        {
            transform.position = scenario.getPlayerInitPos(lastSceneName);
        }
    }

    public string SaveArchive()
    {
        var pos = new JSONArray()
        {
            { new JSONData(transform.position.x) },
            { new JSONData(transform.position.y) },
            { new JSONData(transform.position.z) }
        };
        JSONClass root = new JSONClass()
        {
            { "position", pos },
            { "floorLayer", new JSONData(floorLayer) },
            { "currentEquipType", new JSONData(currentEquipType.ToString()) },
            { "itemOn", new JSONData(itemOn) }
        };

        return root.ToString();
    }
}
