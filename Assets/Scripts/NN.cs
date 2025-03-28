using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NN : MonoBehaviour
{
    public Layer hidden1;
    public Layer hidden2;
    public Layer output;

    public void Awake()
    {
        Debug.Log("NN");
        hidden1 = new Layer(2, 4);
        hidden2 = new Layer(4, 4);
        output = new Layer(4, 1);
    }

    public float[] Brain(float[] inputs)
    {
        hidden1.Forward(inputs);
        hidden1.Activation();
        hidden2.Forward(hidden1.nodeArray);
        hidden2.Activation();
        output.Forward(hidden2.nodeArray);
        //output.Activation();
        Debug.Log(output.nodeArray[0]);

        return (output.nodeArray);
    }

    public void Mutate(int points)
    {
        hidden1.MutateLayer(points);
        hidden2.MutateLayer(points);
        output.MutateLayer(points);
    }

    public void SetBestWeights()
    {
        Array.Copy(hidden1.weightsArray, hidden1.bestWeights, hidden1.weightsArray.Length);

        Array.Copy(hidden2.weightsArray, hidden2.bestWeights, hidden2.weightsArray.Length);
        //hidden1.bestWeights = hidden1.weightsArray;
        Array.Copy(output.weightsArray, output.bestWeights, output.weightsArray.Length);
        //output.bestWeights = output.weightsArray;

    }
    public void SetBestBiases()
    {
        Array.Copy(hidden1.biasesArray, hidden1.bestBiases, hidden1.biasesArray.Length);

        Array.Copy(hidden2.biasesArray, hidden2.bestBiases, hidden2.biasesArray.Length);
        //hidden1.bestBiases = hidden1.biasesArray;
        Array.Copy(output.biasesArray, output.bestBiases, output.biasesArray.Length);
        //output.bestBiases = output.biasesArray;
    }
}
