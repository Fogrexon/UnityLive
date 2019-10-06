using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour
{
    Material mat;
    public bool GenerateRing;
    public float lifeTime = 1.0f;
    private float nowTime;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(GenerateRing)
        {
            nowTime = 0;
            GenerateRing = false;
        }

        if(nowTime >= 0)
        {
            float t = Ease.OutQuad(nowTime / lifeTime);
            mat.SetFloat("_Radius", t*50f);
            mat.SetFloat("_Width", (1.0f - t)*5f);
            nowTime += Time.deltaTime;
            if(nowTime > lifeTime)
            {
                nowTime = -1f;
                mat.SetFloat("_Width", 0f);
            }
        }
    }
}
