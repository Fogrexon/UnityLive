using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeemParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(Mathf.Sin(Time.time*0.03f)*500f, Time.time, Mathf.Cos(Time.time * 0.01f)*900f);
    }
}
