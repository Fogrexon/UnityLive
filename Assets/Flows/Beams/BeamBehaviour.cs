using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject beam;
    public float lifeTime = 3.0f;
    public bool isRestart = false;
    public float maxAngle = 90f;
    float startTime;
    Vector3 startAngle;
    float process = 0f;
    float delta;

    public float radius = 1.0f;
    void Start()
    {
        beam = this.gameObject;
        startTime = -10000f;
        delta = 60f / StateHolder.BPM;
        startAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRestart)
        {
            isRestart = false;
            startTime = Time.time;
        }
        if(Time.time - startTime > lifeTime * delta)
        {
            transform.localScale = Vector3.zero;
            return;
        }
        float t = Ease.OutCubic(process);
        float t2 = Ease.InQuint(process);
        Vector3 scale = new Vector3((1f - t2)*0.3f,Mathf.Min(1000f,process * 1000f), (1f - t2)*0.3f);

        process = (Time.time - startTime) / (lifeTime * delta);
        transform.eulerAngles = startAngle - Vector3.right * t * maxAngle;
        transform.localScale = scale;
    }

}
