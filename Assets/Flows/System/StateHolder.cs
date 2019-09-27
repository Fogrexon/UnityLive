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
        public static float Speed = 2f;
        public static Note[] NotesDataL;
        public static Note[] NotesDataR;

        public static float GetRhythm()
        {
            return Time.time / 60f * BPM;
        }
    }

}
