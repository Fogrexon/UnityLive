using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    private float[] posY;
    private int childCount;
    public float AreaRange = 6.0f;
    public float Speed = 2.0f;
    public float Rot = 120.0f;
    
    void Start()
    {
        childCount = this.transform.childCount;
        posY = new float[childCount];
        for(float i=0;i<(float)childCount;i++)
        {
            posY[(int)i] = i * AreaRange * 2f / ((float)(childCount)) - AreaRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        Transform trans;
        for(int i=0;i<childCount;i++)
        {
            trans = this.transform.GetChild(i).transform;
            pos = trans.localPosition;
            posY[i] = (posY[i] + AreaRange * 3.0f + Time.deltaTime * Speed)%(AreaRange * 2.0f) - AreaRange;
            pos.y = posY[i];
            trans.localPosition = pos;
            trans.Rotate(Vector3.forward * Rot * Time.deltaTime, Space.World);
        }
    }
}
