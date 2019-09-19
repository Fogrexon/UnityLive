using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject g = Instantiate(go);
            g.transform.parent = this.transform;
        }
    }
}
