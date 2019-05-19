using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Firefly : MonoBehaviour
{
    [System.Serializable]
    struct FireflyData
    {
        public Vector3 Position;
    }

    const int SIMULATION_BLOCK_SIZE = 256;

    #region Fireflies Parameters
    [Range(256, 32768)]
    public int MaxObjectNum = 12384;

    public float Speed = 1.0f;

    public Vector3 WallSize = new Vector3(30f,30f,30f);
    public Vector3 WallCenter = new Vector3(0f,0f,0f);
    
    #endregion

    #region Built-in Resources
    public ComputeShader FireflyCS;
    #endregion
    
    #region Private Variables
    ComputeBuffer ff_position;
    #endregion

    public ComputeBuffer GetFireflyDataBuffer()
    {
        return this.ff_position != null ? this.ff_position : null;
    }

    public int GetMaxObjectNum()
    {
        return this.MaxObjectNum;
    }

    public Vector3 GetSimulationAreaCenter()
    {
        return this.WallCenter;
    }

    public Vector3 GetSimulationAreaSize()
    {
        return this.WallSize;
    }
    

    FireflyData randpos()
    {
        Vector3 s = WallSize;
        Vector3 c = WallCenter;
        FireflyData fd;
        fd.Position = new Vector3(
            Random.Range(-s.x + c.x, s.x + c.x),
            Random.Range(-s.y + c.y, s.y + c.y),
            Random.Range(-s.z + c.z, s.z + c.z)
        );
        return fd;
    }

    void InitBuffer()
    {
        ff_position = new ComputeBuffer(MaxObjectNum,Marshal.SizeOf(typeof(FireflyData)));

        FireflyData[] pos = new FireflyData[MaxObjectNum];

        for( int i = 0; i < MaxObjectNum; i++)
        {
            pos[i] = randpos();
        }
        ff_position.SetData(pos);

        pos = null;
    }

    void Simulation(){
        ComputeShader cs = FireflyCS;
        int id = cs.FindKernel("KernelPosition");

        int threadGroupSize = Mathf.CeilToInt(MaxObjectNum / SIMULATION_BLOCK_SIZE);

        cs.SetInt("_MaxFireflyObjectNum", MaxObjectNum);
        cs.SetFloat("_Speed", Speed);
        cs.SetFloat("_Time", Time.time);
        cs.SetBuffer(id, "_FireflyDataBufferRead", ff_position);
        cs.SetBuffer(id, "_FireflyDataBufferWrite", ff_position);
        cs.Dispatch(id, threadGroupSize, 1, 1);
    }


    void Start()
    {
        InitBuffer();
    }

    void Update()
    {
        Simulation();
    }

    void OnDestroy()
    {
        if( ff_position != null )
        {
            ff_position.Release();
            ff_position = null;
        }
    }

    void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(WallCenter, WallSize);
        }
}
