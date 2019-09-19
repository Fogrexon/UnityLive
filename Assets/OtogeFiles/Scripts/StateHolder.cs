using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHolder : MonoBehaviour
{
    public static string songname = "BrainPower";
    public static float BPM = 170f;

    public static float GetRhythm()
    {
        return Time.time / 60f * BPM;
    }

}
