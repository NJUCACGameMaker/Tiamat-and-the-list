using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionArchive {

	
}

public class NotePiece
{
    NotePiece(string shortLine, string detail, bool collected)
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
    public string shortLine;
    public string detail;
    public string picPath;
    public bool collected;
}

public class CGPiece
{
    public string shortLine;
    public string picPath;
    public bool collected;
}

public class MusicPiece
{
    public string path;
    public bool collected;
}
