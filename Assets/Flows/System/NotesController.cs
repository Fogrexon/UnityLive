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
    public int generatePosL;
    public int endPosL;
    public int generatePosR;
    public int endPosR;
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
        source = GetComponent<AudioSource>();
        generatePosL = 0;
        endPosL = 0;
        generatePosR = 0;
        endPosR = 0;

    }

    // Update is called once per frame
    void Update()
    {
        float t = source.time;// - StateHolder.Offset / 10000f;
        while(generatePosL < notesdataL.Length && source.time + 10f > notesdataL[generatePosL].time)
        {
            generatePosL ++;
        }
        while(endPosL < notesdataL.Length && source.time > notesdataL[endPosL].time)
        {
            CheckNotesL(endPosL);
            endPosL ++;
        }
        nv.MoveNotesL(endPosL, generatePosL, t);
        while(generatePosR < notesdataR.Length && source.time + 10f > notesdataR[generatePosR].time)
        {
            generatePosR ++;
        }
        while(endPosR < notesdataR.Length && source.time > notesdataR[endPosR].time)
        {
            CheckNotesR(endPosR);
            endPosR ++;
        }
        nv.MoveNotesR(endPosR, generatePosR, t);
    }

    void CheckNotesL(int epos)
    {
        if(notesdataL[epos].type != 0){
            if(Vector2.Distance(new Vector2(handL.position.x,handL.position.y), notesdataL[epos].pos) < 0.3f)
            {
                score += 100;
            }
        }
        nv.DestroyNotesL(epos);
    }
    void CheckNotesR(int epos)
    {
        if(notesdataR[epos].type != 0){
            if(Vector2.Distance(new Vector2(handR.position.x,handR.position.y), notesdataR[epos].pos) < 0.3f)
            {
                score += 100;
            }
        }
        nv.DestroyNotesR(epos);
    }
}
