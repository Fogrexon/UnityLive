using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float hitTime;
    public float speed;
    public bool isLeft;
    public AudioSource ac;
    // Start is called before the first frame update
    void Start()
    {
        float x = isLeft ? -0.5f : 0.5f;
        transform.position = new Vector3(x, 0f, (hitTime - ac.time)*speed);
    }

    // Update is called once per frame
    void Update()
    {
        float x = isLeft ? -0.5f : 0.5f;
        transform.position = new Vector3(x, 0f, (hitTime - ac.time)*speed);
    }
}
