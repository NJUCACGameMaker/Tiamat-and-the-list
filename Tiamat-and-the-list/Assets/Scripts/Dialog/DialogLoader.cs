using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DialogLoader {
    public List<Dialog> context = new List<Dialog>();
    private string dialogDataPath;
    
    public void loadData()
    {
        var dialogText = Resources.Load<TextAsset>(SceneItemManager.GetLevelName() + "-Dialog");
        dialogDataPath = Application.persistentDataPath + SceneItemManager.GetLevelName() + "-Dialog.txt";
        if (dialogText != null)
        {
            string[] strs = dialogText.text.Split('\n');
            foreach (string rawData in strs)
            {
                if (rawData == "" || rawData == "\r")
                    continue;
                string[] data = rawData.Replace("\\", "\n").Split('|');
                Dialog dialog = new Dialog
                {
                    id = int.Parse(data[0]),
                    section = data[1],
                    characterName = data[2],
                    text = data[3],
                    imagePath = data[4],
                    branchNum = int.Parse(data[5]),
                    branches = new List<Dialog.branch>(),
                    sounds = new List<string>()
                };
                if (dialog.characterName == "0")
                    dialog.characterName = "";
                if (dialog.branchNum > 0)
                {
                    for (int i=0; i<dialog.branchNum; i++)
                    {
                        Dialog.branch branch = new Dialog.branch();
                        branch.switch_section = data[6 + i * 2];
                        branch.text = data[6 + i * 2 + 1];
                        dialog.branches.Add(branch);
                    }
                }
                if (dialog.text.Length - dialog.text.Replace("$","").Length > 0)
                {
                    for (int i = 6 + dialog.branchNum * 2; i < 6 + dialog.branchNum * 2 + dialog.text.Length - dialog.text.Replace("$", "").Length; i++)
                        if (data.Length > i)
                        {
                            dialog.sounds.Add(data[i].Replace("\r", ""));
                        } else
                        {
                            dialog.sounds.Add("");
                        }
                }
                context.Add(dialog);
            }
        }
    }
}
