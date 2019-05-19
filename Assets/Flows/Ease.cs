using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ease
{
    public static float linear(float t){
        return t;
    }
    public static float InQuad(float t){
        return t*t;
    }
    public static float OutQuad(float t){
        return t*(2.0f-t);
    }
    public static float InOutQuad(float t){
        return t<.5f ? 2.0f*t*t : -1+(4-2*t)*t;
    }
    public static float InCubic(float t){
        return t*t*t;
    }
    public static float OutCubic(float t){
        return (--t)*t*t+1.0f;
    }
    public static float InOutCubic(float t){
        return t<.5f ? 4.0f*t*t*t : (t-1.0f)*(2.0f*t-2.0f)*(2.0f*t-2.0f)+1.0f;
    }
    public static float InQuart(float t){
        return t*t*t*t;
    }
    public static float OutQuart(float t){
        return 1.0f-(--t)*t*t*t;
    }
    public static float InOutQuart(float t){
        return t<.5f ? 8.0f*t*t*t*t : 1.0f-8.0f*(--t)*t*t*t;
    }
    public static float InQuint(float t){
        return t*t*t*t*t;
    }
    public static float OutQuint(float t){
        return 1.0f+(--t)*t*t*t*t;
    }
    public static float InOutQuint(float t){
        return t<.5f ? 16.0f*t*t*t*t*t : 1.0f+16.0f*(--t)*t*t*t*t;
    }
}
