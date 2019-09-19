using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    public GameObject beam;

    public float duration = 1.0f;
    public bool isParent = true;
    float prevTime;
    // Start is called before the first frame update
    void Start()
    {
        prevTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - prevTime > duration ){
            GameObject b = Instantiate(beam, this.transform.position, Quaternion.identity);
           if(isParent) b.transform.parent = this.transform;
            b.transform.Rotate(this.transform.eulerAngles);
            prevTime = Time.time;
        }
    }
}
