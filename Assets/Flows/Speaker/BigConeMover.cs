using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace BigConeMover
{

class Cone
{
    public Vector3 localPosition;
}

public class BigConeMover : MonoBehaviour
{
    private Cone childTransform;
    private float volume = 0.0f;

    private float[] waveData = new float[1024];
    
    void Start()
    {
        childTransform = new Cone();
        childTransform.localPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetOutputData(waveData, 1);
        volume /= 2.0f;
        volume += waveData.Select(x => x*x).Sum() / waveData.Length * 0.5f;
        this.transform.localPosition = childTransform.localPosition + new Vector3(0.0f,volume*10f,0.0f);
    }
}

}
