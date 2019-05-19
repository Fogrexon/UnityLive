using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject beam;
    public float lifeTime = 1.0f;
    public float delay = 2.0f;
    float startTime;
    float process = 0f;

    public float radius = 1.0f;
    void Start()
    {
        beam = this.gameObject;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > lifeTime) {
            Destroy(beam);
            return;
        }
        process = ( Time.time - startTime) / lifeTime;
        float sq = (1.0f - Ease.InQuint(process)) * radius;
        Vector3 s = new Vector3(sq, Ease.OutQuint(process)*1000f, sq);
        beam.transform.localScale = s;
    }
}
