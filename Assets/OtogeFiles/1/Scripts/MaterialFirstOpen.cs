using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFirstOpen : MonoBehaviour
{
    public Shader shader;
    
    public Color _Color;
    public Color _Emission;
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material = new Material(shader);
        rend.material.SetFloat("_NowTime", Time.time);
        rend.material.SetColor("_Color", _Color);
        rend.material.SetColor("_Emission", _Emission);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
