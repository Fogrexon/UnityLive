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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNoteObjects()
    {
        notesL = new GameObject[nc.notesdataL.Length];
        notesR = new GameObject[nc.notesdataR.Length];

        for(int i=0;i<nc.notesdataL.Length;i++)
        {
            if(nc.notesdataL[i].type != 0)
            {
                notesL[i] = Instanciate(noteL);
                notesL[i].SetActive(false);
            }else{
                notesL[i] = null;
            }
            if(nc.notesdataR[i].type != 0)
            {
                notesR[i] = Instanciate(noteLR);
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
}
