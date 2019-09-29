using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

[Serializable]
public class Note
{
    public int type;
    public float time;
    public bool isLeft;
    public Color color;
}

[Serializable]
public class SongData
{
    public string songname;
    public string author;
    public float length;
    public List<Note> notes;
}

public class NotesGenerator : MonoBehaviour
{
    private SongData sd;
    public List<Note> notesList;

    private float beforeBPM = 0;
    // Start is called before the first frame update
    void Start()
    {
        FileRead();
    }

    void FileRead()
    {
        string jsondata = "";
         // FileReadTest.txtファイルを読み込む
        FileInfo fi = new FileInfo(Application.dataPath + "/songs/" + StateHolder.songname + "/SongData.json");
        try {
            // 一行毎読み込み
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)){
                jsondata = sr.ReadToEnd();
            }
        } catch (Exception e){
            // 改行コード
            jsondata += SetDefaultText();
        }
        Debug.Log(jsondata);
        sd = JsonUtility.FromJson<SongData>(jsondata);
        notesList = sd.notes;
    }
    string SetDefaultText(){
        return "C#あ\n";
    }
}
