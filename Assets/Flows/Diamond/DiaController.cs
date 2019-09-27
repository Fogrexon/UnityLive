using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace DiamondControllers
{

class Diamond
{
    public Vector3 position;
    public Vector3 scale;
}

public class DiaController : MonoBehaviour
{
    private Diamond[] childTransform;
    private int childCount;
    private float volume = 0.0f;

    private float[] waveData = new float[1024];
    
    void Start()
    {
        childCount = this.transform.childCount;
        childTransform = new Diamond[childCount];
        for(int i=0;i<childCount;i++)
        {
            childTransform[i] = new Diamond();
            childTransform[i].position = this.transform.GetChild(i).transform.position;
            childTransform[i].scale = this.transform.GetChild(i).transform.localScale;
            Debug.Log(childTransform[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetOutputData(waveData, 1);
        volume /= 2.0f;
        volume += waveData.Select(x => x*x).Sum() / waveData.Length * 0.5f;
        var y = Mathf.Sin(StateHolder.GetRhythm() / 10.0f * Mathf.PI * 2.0f)*0.3f;
        for(int i=0;i<childCount;i++)
        {
            this.transform.GetChild(i).transform.position = childTransform[i].position + new Vector3(0.0f,y,0.0f);
            this.transform.GetChild(i).transform.localScale = childTransform[i].scale * (1.0f + volume * 100.0f);
            this.transform.GetChild(i).transform.Rotate(Vector3.up * Time.deltaTime * 30.0f, Space.World);
        }
    }
}

}
