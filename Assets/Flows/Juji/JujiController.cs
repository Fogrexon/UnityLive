using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiveUtil;
using System.Linq;

class Juji
{
    public Vector4 position;
    public Transform trans;
}

public class JujiController : MonoBehaviour
{
    public Vector3 rot;
    private Juji[] childTransform;
    private int childCount;
    
    private float volume = 0.0f;

    private float[] waveData = new float[1024];
    
    void Start()
    {
        childCount = this.transform.childCount;
        childTransform = new Juji[childCount];
        Vector3 v;
        for(int i=0;i<childCount;i++)
        {
            childTransform[i] = new Juji();
            childTransform[i].trans = this.transform.GetChild(i).transform;
            v = childTransform[i].trans.localPosition;
            childTransform[i].position = new Vector4(v.x,v.y,v.z, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetOutputData(waveData, 1);
        volume /= 2.0f;
        volume += waveData.Select(x => x*x).Sum() / waveData.Length * 0.5f;
        Vector3 b = new Vector3(2f,2f,2f);
        Matrix4x4 mat = Matrix4x4.Scale(b - new Vector3(volume, volume, volume)*8f) * Translator.Rotate(rot);
        for(int i=0;i<childCount;i++)
        {
            childTransform[i].trans.localPosition = (Vector3)(mat * childTransform[i].position);
        }
    }
}
