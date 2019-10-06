using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JujiColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
