using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Firefly))]
public class FireflyRenderer : MonoBehaviour
{
    #region Parameters
    public Vector3 ObjectScale = new Vector3(1f,1f,1f);
    #endregion

    #region Script References
    public Firefly FireflyScript;
    #endregion

    #region Built-in Resources
    public Mesh InstanceMesh;
    public Material InstanceRenderMaterial;
    #endregion

    #region Private Variables
    uint[] args = new uint[5]{ 0, 0, 0, 0, 0 };
    ComputeBuffer argsBuffer;
    #endregion


    void RenderInstancedMesh()
    {
        if(InstanceRenderMaterial == null || FireflyScript == null || !SystemInfo.supportsInstancing) return;

        uint numIndices = (InstanceMesh != null) ?
                (uint)InstanceMesh.GetIndexCount(0) : 0;
        args[0] = numIndices;
        args[1] = (uint) FireflyScript.GetMaxObjectNum();
        argsBuffer.SetData(args);

        InstanceRenderMaterial.SetBuffer("_FireflyDataBuffer",
            FireflyScript.GetFireflyDataBuffer());
        
        InstanceRenderMaterial.SetVector("_ObjectScale", ObjectScale);

        var bounds = new Bounds
        (
            FireflyScript.GetSimulationAreaCenter(), // 中心
            FireflyScript.GetSimulationAreaSize()    // サイズ
        );

        Graphics.DrawMeshInstancedIndirect
        (
            InstanceMesh,
            0,
            InstanceRenderMaterial,
            bounds,
            argsBuffer
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), 
            ComputeBufferType.IndirectArguments);
    }

    // Update is called once per frame
    void Update()
    {
        RenderInstancedMesh();
    }

    void OnDisable()
    {
        if(argsBuffer != null )
        {
            argsBuffer.Release();
            argsBuffer = null;
        }
    }
}
