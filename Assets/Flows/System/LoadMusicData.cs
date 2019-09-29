using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

[Serializable]
public class MusicData
{
    public string MusicName;
    public string MusicAuthor;
    public int BPM;
    public float Offset;
    public Note[] notesL;
    public Note[] notesR;
}

[Serializable]
public class Note
{
    public int type;
    public Vector2 pos;
}
public class LoadMusicData : MonoBehaviour
{


    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/SongData/Firefly.json");
        MusicData md = JsonUtility.FromJson<MusicData>(json);
        StateHolder.SongName = md.MusicName;
        StateHolder.BPM = md.BPM;
        StateHolder.Offset = md.Offset;
        StateHolder.NotesDataL = md.notesL;
        StateHolder.NotesDataR = md.notesR;
        GetComponent<NotesController>().Initialize();
    }
}



