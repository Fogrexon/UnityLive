using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public ComputeShader computeShader;
    int kernelIndex_KernelFunction_A;
    int kernelIndex_KernelFunction_B;
    ComputeBuffer floatComputeBuffer;
    // Start is called before the first frame update
    void Start()
    {
        this.kernelIndex_KernelFunction_A = this.computeShader.FindKernel("KernelFunction_A");
        this.kernelIndex_KernelFunction_B = this.computeShader.FindKernel("KernelFunction_B");

        this.floatComputeBuffer = new ComputeBuffer(4,sizeof(float));
        this.computeShader.SetBuffer(this.kernelIndex_KernelFunction_A, "floatBuffer", this.floatComputeBuffer);

        this.computeShader.SetFloat("floatValue", Time.time);

        this.computeShader.Dispatch(this.kernelIndex_KernelFunction_A, 1,1,1);

        float[] result = new float[4];

        this.floatComputeBuffer.GetData(result);

        for(int i = 0; i < 4 ; i++){
            Debug.Log(result[i]);
        }

        this.floatComputeBuffer.Release();
    }

    // Update is called once per frame
    void Update()
    {        
        this.floatComputeBuffer = new ComputeBuffer(4,sizeof(float));
        this.computeShader.SetBuffer(this.kernelIndex_KernelFunction_A, "floatBuffer", this.floatComputeBuffer);

        this.computeShader.SetFloat("floatValue", Time.time);
        this.computeShader.Dispatch(this.kernelIndex_KernelFunction_A, 1,1,1);

        float[] result = new float[4];

        this.floatComputeBuffer.GetData(result);

        for(int i = 0; i < 4 ; i++){
            Debug.Log(result[i]);
        }

        this.floatComputeBuffer.Release();
    }
}
