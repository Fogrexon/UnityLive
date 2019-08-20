using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject effect;
    public bool isLeft = true;

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Note")
        {
            GameObject e = Instantiate(effect);
            e.transform.position = col.transform.position;

            Destroy(col.gameObject);
        }
    }
}
