using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDeleter : MonoBehaviour
{

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Note") Destroy(col.gameObject);
    }
}
