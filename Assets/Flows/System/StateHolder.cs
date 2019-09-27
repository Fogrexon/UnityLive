using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateHolder
{

    public struct Note
    {
        public int type;
        public Vector2 pos;
    }

    public class StateHolder : MonoBehaviour
    {
        public static string songname = "BrainPower";
        public static float BPM = 170f;
        public static Note[] notesdataL;
        public static Note[] notesdataR;

        public static float GetRhythm()
        {
            return Time.time / 60f * BPM;
        }
    }

}
