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
    private static readonly string ArchivePath = Application.persistentDataPath + "\\collectionArchive.json";

    private readonly Dictionary<string, NotePiece> notes;
    private readonly Dictionary<string, CollectionPiece> collections;
    private readonly Dictionary<string, CgPiece> cgs;
    private readonly Dictionary<string, MusicPiece> musics;

    private CollectionArchive()
    {
        notes = new Dictionary<string, NotePiece>();
        collections = new Dictionary<string, CollectionPiece>();
        cgs = new Dictionary<string, CgPiece>();
        musics = new Dictionary<string, MusicPiece>();
    }

    //在这里记录所有出现在笔记里的内容
    private void ContentInit()
    {
        notes.Add("L1S1Note1", new NotePiece("教学关纸条1", "人本身没有罪，而是有人觉得他们有罪。", false));
        notes.Add("L1S3NoteLeft", new NotePiece("第一关左展示台纸条", "神生而为父，便觉得自己无事不知，子自当言听计从。殊不知，其子也有成为父亲的一天，神民同等。", false));
        notes.Add("L1S3NoteRight", new NotePiece("第一关右展示台纸条", "断罪者终断去自己的罪。断罪者终无法断去自己的罪。", false));
        collections.Add("Flashlight", new CollectionPiece("手电筒", "一个有一些年份的手电筒，不过竟然还能亮",
            "EquipmentSprite/Stage00_shoudiantong", false));
        cgs.Add("Apkal_serious", new CgPiece("Apkal", "CharacterTachie/Apkal_serious", false));
        cgs.Add("A_default", new CgPiece("A", "CharacterTachie/A_default", false));
        cgs.Add("Geshta_default", new CgPiece("Geshta", "CharacterTachie/Geshta_default", false));
        cgs.Add("Geshta_smile", new CgPiece("Geshta", "CharacterTachie/Geshta_smile", false));
        musics.Add("THE PIANO LADY", new MusicPiece("<size=32>THE PIANO LADY (LENA ORSA) - \r\n Water Sparks in a Sunbeam</size>", "DynamicAudios/ThePianoLady", true));
        musics.Add("MAYA FILIPI", new MusicPiece("MAYA FILIPIC - Rose", "DynamicAudios/MayaFilipi-Rose", false));
        musics.Add("GARSUMENE", new MusicPiece("<size=32>GARSUMENE - Chopin Piano Waltz \r\n in B minor, Op. 69, No. 2</size>", "DynamicAudios/GarsuMene", false));
    }

    private static void LoadArchive()
    {
        try
        {
            var reader = new StreamReader(ArchivePath, System.Text.Encoding.UTF8);
            LoadArchive(reader.ReadToEnd());
            reader.Close();
        }catch(IOException e)
        {
            Debug.Log("CollectionArchive doesn't Exist");
        }
    }

    private static void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        if (root == null) return;
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

    public static void SaveArchive()
    {
        var writer = new StreamWriter(ArchivePath, false, System.Text.Encoding.UTF8);
        writer.WriteLine(GetArchive());
        writer.Flush();
        writer.Close();
    }

    private static string GetArchive()
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

        if (instance == null) return root.ToString();
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

    public static IEnumerable<NotePiece> GetNotes()
    {
        if (instance == null) { Init(); }
        var result = new List<NotePiece>();
        if (instance == null) return result;
        foreach (NotePiece notePiece in instance.notes.Values)
        {
            if (notePiece.collected)
            {
                result.Add(notePiece);
            }
        }

        return result;
    }

    public static IEnumerable<CollectionPiece> GetCollections()
    {
        if (instance == null) { Init(); }
        var result = new List<CollectionPiece>();
        if (instance == null) return result;
        foreach (var collectionPiece in instance.collections.Values)
        {
            if (collectionPiece.collected)
            {
                result.Add(collectionPiece);
            }
        }

        return result;
    }

    public static IEnumerable<CgPiece> GetCGs()
    {
        if (instance == null) { Init(); }
        var result = new List<CgPiece>();
        if (instance == null) return result;
        foreach (var cgPiece in instance.cgs.Values)
        {
            if (cgPiece.collected)
            {
                result.Add(cgPiece);
            }
        }

        return result;
    }

    public static IEnumerable<MusicPiece> GetMusics()
    {
        if (instance == null) { Init(); }
        var result = new List<MusicPiece>();
        if (instance == null) return result;
        foreach (var musicPiece in instance.musics.Values)
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
        if (instance != null && instance.notes[key] != null)
        {
            instance.notes[key].collected = true;
        }
    }

    public static void CollectionCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance != null && instance.collections[key] != null)
        {
            instance.collections[key].collected = true;
        }
    }

    public static void CgCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance != null && instance.cgs[key] != null)
        {
            instance.cgs[key].collected = true;
        }
    }

    public static void MusicCollect(string key)
    {
        if (instance == null) { Init(); }
        if (instance != null && instance.musics[key] != null)
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
    public readonly string shortLine;
    public readonly string detail;
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
    public readonly string shortLine;
    public readonly string detail;
    public readonly string picPath;
    public bool collected;
}

public class CgPiece
{
    public CgPiece(string shortLine, string picPath, bool collected)
    {
        this.shortLine = shortLine;
        this.picPath = picPath;
        this.collected = collected;
    }
    public readonly string shortLine;
    public readonly string picPath;
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
    public readonly string shortLine;
    public readonly string path;
    public bool collected;
}
