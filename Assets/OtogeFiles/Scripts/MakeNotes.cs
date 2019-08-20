using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;


public class MakeNotes : MonoBehaviour
{
    public float len;
    List<Note> notes;
    bool flag = true;
    
    private AudioSource ac;
    // Start is called before the first frame update
    void Start()
    {
        notes = new List<Note>();
        ac = GetComponent<AudioSource>();
        ac.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(flag){
        if(Input.GetKeyDown(KeyCode.S)){
            
            Note noteL = new Note();
            noteL.type = 1;
            noteL.time = ac.time;
            noteL.color = new Color(1.0f,1.0f,1.0f,1.0f);
            noteL.isLeft = true;
            notes.Add(noteL);

        }
        if(Input.GetKeyDown(KeyCode.K)){
            
            Note noteR = new Note();
            noteR.type = 1;
            noteR.time = ac.time;
            noteR.color = new Color(1.0f,1.0f,1.0f,1.0f);
            noteR.isLeft = false;
            notes.Add(noteR);

        }

        if(ac.time >= len)
        {
            Debug.Log("End");
            makeJson();
            flag = false;
        }
        }

    }

    void makeJson()
    {
        SongData sd = new SongData();
        sd.songname = null;
        sd.author = null;
        sd.length = len;
        sd.notes = notes;
        string jsondata = JsonUtility.ToJson(sd);
        jsondata = jsondata.Replace(",", ",\n");
        File.AppendAllText (Application.dataPath + "/songs/SongData.json", jsondata);
        
        Debug.Log("Written");
    }
}
