
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace PileController
{

class Pile
{
    public float scaleY;
    public float localVolume;
}

public class Piles : MonoBehaviour
{
    private Pile[] childTransform;
    private int childCount;
    private float volume = 0.0f;

    private float[] waveData = new float[1024];
    
    void Start()
    {
        childCount = this.transform.childCount;
        childTransform = new Pile[childCount];
        for(int i=0;i<childCount;i++)
        {
            childTransform[i] = new Pile();
            childTransform[i].scaleY = this.transform.GetChild(i).transform.localScale.y;
            childTransform[i].localVolume = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetOutputData(waveData, 1);
        float delta = ((float)waveData.Length) / ((float)childCount);
        float volume = 0f;
        var y = Mathf.Sin(StateHolder.GetRhythm() / 10.0f * Mathf.PI * 2.0f)*0.3f;
        Transform s;
        for(int i=0;i<childCount;i++)
        {
            childTransform[i].localVolume *= 0.99f;
            childTransform[i].localVolume = Mathf.Max(waveData[Mathf.FloorToInt(delta * i)], childTransform[i].localVolume);
            s = this.transform.GetChild(i).transform;
            s.localScale = new Vector3(s.localScale.x, childTransform[i].scaleY * (0.1f + childTransform[i].localVolume ), s.localScale.z);

        }
    }
}

}
