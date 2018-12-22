using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class CollectionArchive {

	private static CollectionArchive Init()
    {
        if (instance == null)
        {
            instance = new CollectionArchive();
        }
        instance.ContentInit();
        LoadArchive();
        return instance;
    }
    private static CollectionArchive instance;
    private static string archivePath = Application.persistentDataPath + "\\collectionArchive.json";

    private Dictionary<string, NotePiece> notes;
    private Dictionary<string, CollectionPiece> collections;
    private Dictionary<string, CGPiece> cgs;
    private Dictionary<string, MusicPiece> musics;

    private CollectionArchive()
    {
        notes = new Dictionary<string, NotePiece>();
        collections = new Dictionary<string, CollectionPiece>();
        cgs = new Dictionary<string, CGPiece>();
        musics = new Dictionary<string, MusicPiece>();
    }

    //在这里记录所有出现在笔记里的内容
    private void ContentInit()
    {
        notes.Add("L1S1Note1", new NotePiece("教学关纸片1", "人本身没有罪，而是有人觉得他们有罪。", false));
        notes.Add("L1S3NoteLeft", new NotePiece("第一关左展示台纸条", "待定", false));
        notes.Add("L1S3NoteRight", new NotePiece("第一关右展示台纸条", "待定", false));
        collections.Add("Flashlight", new CollectionPiece("手电筒", "一个有一些年份的手电筒，不过竟然还能亮",
            "EquipmentSprite/Stage00_shoudiantong", false));
        cgs.Add("Apkal_serious", new CGPiece("Apkal", "CharacterTachie/Apkal_serious", false));
        cgs.Add("A_default", new CGPiece("A", "CharacterTachie/A_default", false));
        cgs.Add("Geshta_default", new CGPiece("Geshta", "CharacterTachie/Geshta_default", false));
        cgs.Add("Geshta_smile", new CGPiece("Geshta", "CharacterTachie/Geshta_smile", false));
        musics.Add("THE PIANO LADY", new MusicPiece("<size=32>THE PIANO LADY (LENA ORSA) - \r\n Water Sparks in a Sunbeam</size>", "DynamicAudios/ThePianoLady", true));
        musics.Add("MAYA FILIPI", new MusicPiece("MAYA FILIPIC - Rose", "DynamicAudios/MayaFilipi-Rose", false));
        musics.Add("GARSUMENE", new MusicPiece("<size=32>GARSUMENE - Chopin Piano Waltz \r\n in B minor, Op. 69, No. 2</size>", "DynamicAudios/GarsuMene", false));
    }

    public static void LoadArchive()
    {
        try
        {
            var reader = new StreamReader(archivePath, System.Text.Encoding.UTF8);
            LoadArchive(reader.ReadToEnd());
            reader.Close();
        }catch(IOException e)
        {
            Debug.Log("CollectionArchive doesn't Exist");
        }
    }
    public static void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        if (root != null)
        {
            foreach (var node in root["Notes"].Childs)
            {
                instance.notes[node].collected = true;
            }
            foreach (var node in root["Collections"].Childs)
            {
                instance.collections[node].collected = true;
            }
            foreach (var node in root["CGs"].Childs)
            {
                instance.cgs[node].collected = true;
            }
            foreach (var node in root["Musics"].Childs)
            {
                instance.musics[node].collected = true;
            }
        }
    }

    public static void SaveArchive()
    {
        var writer = new StreamWriter(archivePath, false, System.Text.Encoding.UTF8);
        writer.WriteLine(GetArchive());
        writer.Flush();
        writer.Close();
    }
    public static string GetArchive()
    {
        if (instance == null) { Init(); }
        var noteNode = new JSONArray();
        var collectionNode = new JSONArray();
        var cgNode = new JSONArray();
        var musicNode = new JSONArray();
        var root = new JSONClass()
        {
            { "Notes", noteNode },
            { "Collections", collectionNode },
            { "CGs", cgNode },
            { "Musics", musicNode }
        };

        foreach (var key in instance.notes.Keys)
        {
            if (instance.notes[key].collected)
            {
                noteNode.Add(new JSONData(key));
            }
        }
        foreach (var key in instance.collections.Keys)
        {
            if (instance.collections[key].collected)
            {
                collectionNode.Add(new JSONData(key));
            }
        }
        foreach (var key in instance.cgs.Keys)
        {
            if (instance.cgs[key].collected)
            {
                cgNode.Add(new JSONData(key));
            }
        }
        foreach (var key in instance.musics.Keys)
        {
            if (instance.musics[key].collected)
            {
                musicNode.Add(new JSONData(key));
            }
        }

        return root.ToString();
    }

    public static List<NotePiece> GetNotes()
    {
        if (instance == null) { Init(); }
        List<NotePiece> result = new List<NotePiece>();
        foreach (NotePiece notePiece in instance.notes.Values)
        {
            if (notePiece.collected)
            {
                result.Add(notePiece);
            }
        }
        return result;
    }

    public static List<CollectionPiece> GetCollections()
    {
        if (instance == null) { Init(); }
        var result = new List<CollectionPiece>();
        foreach (CollectionPiece collectionPiece in instance.collections.Values)
        {
            if (collectionPiece.collected)
            {
                result.Add(collectionPiece);
            }
        }
        return result;
    }

    public static List<CGPiece> GetCGs()
    {
        if (instance == null) { Init(); }
        var result = new List<CGPiece>();
        foreach (CGPiece cgPiece in instance.cgs.Values)
        {
            if (cgPiece.collected)
            {
                result.Add(cgPiece);
            }
        }
        return result;
    }

    public static List<MusicPiece> GetMusics()
    {
        if (instance == null) { Init(); }
        var result = new List<MusicPiece>();
        foreach (MusicPiece musicPiece in instance.musics.Values)
        {
            if (musicPiece.collected)
            {
                result.Add(musicPiece);
            }
        }
        return result;
    }
    public static void NoteCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance.notes[key] != null)
        {
            instance.notes[key].collected = true;
        }
    }

    public static void CollectionCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance.collections[key] != null)
        {
            instance.collections[key].collected = true;
        }
    }

    public static void CGCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance.cgs[key] != null)
        {
            instance.cgs[key].collected = true;
        }
    }

    public static void MusicCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance.musics[key] != null)
        {
            instance.musics[key].collected = true;
        }
    }
}

public class NotePiece
{
    public NotePiece(string shortLine, string detail, bool collected)
    {
        this.shortLine = shortLine;
        this.detail = detail;
        this.collected = collected;
    }
    public string shortLine;
    public string detail;
    public bool collected;
}

public class CollectionPiece
{
    public CollectionPiece(string shortLine, string detail, string picPath, bool collected)
    {
        this.shortLine = shortLine;
        this.detail = detail;
        this.picPath = picPath;
        this.collected = collected;
    }
    public string shortLine;
    public string detail;
    public string picPath;
    public bool collected;
}

public class CGPiece
{
    public CGPiece(string shortLine, string picPath, bool collected)
    {
        this.shortLine = shortLine;
        this.picPath = picPath;
        this.collected = collected;
    }
    public string shortLine;
    public string picPath;
    public bool collected;
}

public class MusicPiece
{
    public MusicPiece(string shortLine, string path, bool collected)
    {
        this.shortLine = shortLine;
        this.path = path;
        this.collected = collected;
    }
    public string shortLine;
    public string path;
    public bool collected;
}
