using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public float lifetime;
    private float life;
    // Start is called before the first frame update
    void Start()
    {
        life = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if(life < 0f) Destroy(this.gameObject);
    }
}
