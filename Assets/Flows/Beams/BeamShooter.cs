using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace BeamShooters
{


public class BeamShooter : MonoBehaviour
{
    private BeamBehaviour[] childBeam;
    private int childCount;
    private float delta;
    private float startTime;
    public bool shotAll = false;
    public bool startLaser = false;
    public float duration = 2f;
    private int moveLaser = -2;
    
    void Start()
    {
        delta  = 60f / StateHolder.BPM;
        childCount = this.transform.childCount;
        childBeam = new BeamBehaviour[childCount];
        for(int i=0;i<childCount;i++)
        {
            childBeam[i] = this.transform.GetChild(i).gameObject.GetComponent<BeamBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startLaser)
        {
            startLaser = false;
            moveLaser = -1;
            startTime = Time.time;
        }
        if(moveLaser >= -1){
            if(!shotAll)SwitchLaser();
            else ShotLaser();
        }
    }

    void SwitchLaser()
    {
        int pointer = Mathf.FloorToInt((Time.time - startTime) / (delta * duration));
        if(pointer == childBeam.Length)
        {
            moveLaser = -2;
            return;
        }
        if(pointer != moveLaser)
        {
            moveLaser = pointer;
            childBeam[moveLaser].isRestart = true;
        }
    }

    void ShotLaser()
    {
        for(int i=0;i<childCount;i++)
        {
            childBeam[i].isRestart = true;
        }
        moveLaser = -2;
    }
}

}
