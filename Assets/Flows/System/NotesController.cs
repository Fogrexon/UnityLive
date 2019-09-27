using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public int score;
    public Note[] notesdataL;
    public Note[] notesdataR;
    public Transform handL;
    public Transform handR;
    float bpm;
    float delta;
    public int generatePos;
    public int endPos;
    AudioSource source;
    NotesViewer nv;
    void Start()
    {
        score = 0;
        notesdataL = StateHolder.NotesDataL;
        notesdataR = StateHolder.NotesDataR;
        
        nv = GetComponent<NotesViewer>();
        nv.GenerateNoteObjects();
        bpm = StateHolder.BPM;
        delta = 60f / bpm;
        source = GetComponent<AudioSource>();
        generatePos = 0;
        endPos = 0;

    }

    // Update is called once per frame
    void Update()
    {
        int gp2 = Mathf.FloorToInt((source.time + 20f) / delta);
        if(gp2 < notesdata.Length && gp2 != generatePos)
        {
            generatePos = gp2;
        }
        int ep2 = Mathf.FloorToInt((source.time) / delta);
        if(ep2 < notesdata.Length && ep2 != generatePos)
        {
            CheckNotes(endPos);
            endPos = ep2;
        }
        nv.MoveNotes(endPos, generatePos, source.time, delta);
    }

    void CheckNotes(int epos)
    {
        if(notesdataL[epos].type != 0){
            if(Vector2.Distance(handL.position.xy, notesdataL[epos]) < 0.3f)
            {
                score += 100;
            }
        }
        if(notesdataR[epos].type != 0){
            if(Vector2.Distance(handR.position.xy, notesdataR[epos]) < 0.3f)
            {
                score += 100;
            }
        }
        nv.DestroyNotes();
    }
}
