using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class NoteHolder
{
    public Note note;
    public Transform tf;

    public NoteHolder(Note n, Transform t)
    {
        this.note = n;
        this.tf = t;
    }
}

public class NotesManager : MonoBehaviour
{
    #region publicObjects
    public GameObject note;
    public Transform notes;
    public float speed = 10.0f;
    #endregion

    #region NeedVariable
    private NotesGenerator ng;
    private List<NoteHolder> queue;
    private AudioSource ac;
    private int readline;
    private bool trigLeft;
    private bool trigRight;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        ng = this.GetComponent<NotesGenerator>();
        ac = this.GetComponent<AudioSource>();
        readline = 0;
        queue = new List<NoteHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        AddNote();
        CheckTrigger();
        CalculatePosition();
    }

    void AddNote()
    {
        while(readline < ng.notesList.Count && ng.notesList[readline].time < ac.time + 2f)
        {
            GameObject g = Instantiate(note);
            g.transform.parent = notes;
            queue.Add(new NoteHolder(ng.notesList[readline], g.transform));
            readline ++;
        }
    }

    void CheckTrigger()
    {
        trigLeft = Input.GetKeyDown(KeyCode.S);
        trigRight = Input.GetKeyDown(KeyCode.K);
    }

    void CalculatePosition()
    {
        List<NoteHolder> removeList = new List<NoteHolder>();
        NoteHolder nh;
        float delta = 0;
        bool flag = false;
        bool leftFlag = false;
        bool rightFlag = false;
        for(int i=0; i<queue.Count; i++)
        {
            nh = queue[i];
            delta = nh.note.time - 0.1f - ac.time;
            nh.tf.position = new Vector3((nh.note.isLeft ? -0.5f : 0.5f), 0f, delta*speed);
            flag = false;
            if(!leftFlag && nh.note.isLeft && trigLeft) leftFlag = (flag = CheckTriggerOn(delta)) || leftFlag;
            if(!rightFlag && !nh.note.isLeft && trigRight) rightFlag = (flag = CheckTriggerOn(delta)) || rightFlag;
            if(delta < -0.1f) flag = true;
            if(flag) removeList.Add(nh);
        }
        
        

        for(int i=0; i<Mathf.Min(removeList.Count, 2); i++)
        {
            Destroy(removeList[i].tf.gameObject);
            queue.Remove(removeList[i]);
        }
    }

    bool CheckTriggerOn(float pos)
    {
        if(Mathf.Abs(pos) <= 0.1f){
            Debug.Log(Mathf.Abs(pos));
            return true;
        }
        return false;
    }
}
