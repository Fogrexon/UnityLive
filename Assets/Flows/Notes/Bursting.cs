using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortLife : MonoBehaviour
{
    public float LifeTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
