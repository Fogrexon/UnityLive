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
    public void Initialize()
    {
        score = 0;
        notesdataL = StateHolder.NotesDataL;
        notesdataR = StateHolder.NotesDataR;
        
        nv = GetComponent<NotesViewer>();
        nv.Initialize();
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
        float t = Mathf.Max(source.time - StateHolder.Offset, 0.0f);
        int gp2 = Mathf.Min(Mathf.FloorToInt((t + 10f) / delta), notesdataL.Length);
        if(gp2 != generatePos)
        {
            generatePos = gp2;
        }
        int ep2 = Mathf.Min(Mathf.FloorToInt(t / delta), notesdataL.Length);
        if(ep2 < notesdataL.Length && ep2 != endPos)
        {
            CheckNotes(endPos);
            endPos = ep2;
        }
        nv.MoveNotes(endPos, generatePos, t, delta);
    }

    void CheckNotes(int epos)
    {
        if(notesdataL[epos].type != 0){
            if(Vector2.Distance(new Vector2(handL.position.x,handL.position.y), notesdataL[epos].pos) < 0.3f)
            {
                score += 100;
            }
        }
        if(notesdataR[epos].type != 0){
            if(Vector2.Distance(new Vector2(handR.position.x,handR.position.y), notesdataR[epos].pos) < 0.3f)
            {
                score += 100;
            }
        }
        nv.DestroyNotes(epos);
    }
}
