using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public class StateHolder : MonoBehaviour
    {
        public static string SongName = "BrainPower";
        public static float BPM = 60f;
        public static float Offset;
        public static float Speed = 5f;
        public static Note[] NotesDataL;
        public static Note[] NotesDataR;
        
        public static Vector3 BasePosition = new Vector3(0.0f, 1.0f, 2f);
        public static float PlaySize = 0.5f;

        public static float GetRhythm()
        {
            return Time.time / 60f * BPM;
        }
    }

