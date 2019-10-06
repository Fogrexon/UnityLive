using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesViewer : MonoBehaviour
{
    NotesController nc;
    public GameObject noteL;
    public GameObject noteR;
    GameObject[] notesL;
    GameObject[] notesR;
    public void Initialize()
    {
        nc = GetComponent<NotesController>();
    }

    public void GenerateNoteObjects()
    {
        notesL = new GameObject[nc.notesdataL.Length];
        notesR = new GameObject[nc.notesdataR.Length];
        Vector2 nl,nr;

        for(int i=0;i<nc.notesdataL.Length;i++)
        {
            if(nc.notesdataL[i].type != 0)
            {
                notesL[i] = Instantiate(noteL);
                nl = nc.notesdataL[i].pos;
                notesL[i].transform.position = new Vector3(nl.x,nl.y,0.0f) * StateHolder.PlaySize + StateHolder.BasePosition;
                notesL[i].SetActive(false);
            }else{
                notesL[i] = null;
            }
        }

        for(int i=0;i<nc.notesdataR.Length;i++)
        {
            if(nc.notesdataR[i].type != 0)
            {
                notesR[i] = Instantiate(noteR);
                nr = nc.notesdataR[i].pos;
                notesR[i].transform.position = new Vector3(-nr.x,-nr.y,0.0f) * StateHolder.PlaySize + StateHolder.BasePosition;
                notesR[i].SetActive(false);
            }else{
                notesR[i] = null;
            }
        }
    }

    public void DestroyNotesL(int gpos)
    {
        if(!!notesL[gpos]) notesL[gpos].SetActive(false);
    }
    public void DestroyNotesR(int gpos)
    {
        if(!!notesR[gpos]) notesR[gpos].SetActive(false);
    }

    public void MoveNotesL(int start, int end, float t)
    {
        float p;
        Vector3 pos;
        for(int i=start;i<end;i++)
        {
            p = (nc.notesdataL[i].time - t) * StateHolder.Speed;
            pos = notesL[i].transform.position;
            pos.z = p + StateHolder.BasePosition.z;
            notesL[i].transform.position = pos;
            notesL[i].SetActive(true);
        }
    }
    public void MoveNotesR(int start, int end, float t)
    {
        float p;
        Vector3 pos;
        for(int i=start;i<end;i++)
        {
            p = (nc.notesdataR[i].time - t) * StateHolder.Speed;
            pos = notesR[i].transform.position;
            pos.z = p + StateHolder.BasePosition.z;
            notesR[i].transform.position = pos;
            notesR[i].SetActive(true);
        }
    }
}
