using System.IO;
using UnityEngine;
using UnityEditor;

public class MenuItems : MonoBehaviour {

	[MenuItem("Tools/Archive Manager/Clear Archive")]
    public static void ClearArchive()
    {
        File.Delete(Application.persistentDataPath + "\\" + "Normal-Archive.json");
        PlayerPrefs.DeleteAll();
    }
}
