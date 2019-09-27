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
    void Start()
    {
        nc = GetComponent<NotesController>();
    }

    public void GenerateNoteObjects()
    {
        notesL = new GameObject[nc.notesdataL.Length];
        notesR = new GameObject[nc.notesdataR.Length];

        for(int i=0;i<nc.notesdataL.Length;i++)
        {
            if(nc.notesdataL[i].type != 0)
            {
                notesL[i] = Instantiate(noteL);
                notesL[i].transform.position = new Vector3();
                notesL[i].SetActive(false);
            }else{
                notesL[i] = null;
            }
            if(nc.notesdataR[i].type != 0)
            {
                notesR[i] = Instantiate(noteR);
                notesR[i].SetActive(false);
            }else{
                notesR[i] = null;
            }
        }
    }

    public void DestroyNotes(int gpos)
    {
        if(!!notesL[gpos]) notesL[gpos].SetActive(false);
        if(!!notesR[gpos]) notesR[gpos].SetActive(false);
    }

    public void MoveNotes(int start, int end, float t, float d)
    {
        float p;
        for(int i=start;i<end+1;i++)
        {
            p = 1.0f + (((float)i)*d - t) *  StateHolder.Speed;
            if(!!notesL[i]) notesL[i].transform.position.z = p;
            if(!!notesR[i]) notesR[i].transform.position.z = p;
        }
    }
}
