using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiveUtil
{
    public  class Translator : MonoBehaviour
    {
        public static Matrix4x4 TranslateMatrix(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            Matrix4x4 m = Matrix4x4.identity;
            m.SetTRS(pos,Quaternion.Euler(rot.x,rot.y,rot.z),scale);
            return m;
        }
        public static Matrix4x4 Rotate(Vector3 rot)
        {
            return Matrix4x4.Rotate(Quaternion.Euler(rot.x,rot.y,rot.z));
        }
    }
}
