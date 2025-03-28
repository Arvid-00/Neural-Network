using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Layer : MonoBehaviour
{
    public float[,] bestWeights; //added
    public float[] bestBiases; //added

    public float[,] weightsArray;
    public float[] biasesArray;
    public float[] nodeArray;

    public int nodes;
    public int inputs;
    public Layer(int inputs, int nodes)
    {
        this.nodes = nodes;
        this.inputs = inputs;

        bestWeights = new float[nodes, inputs];
        bestBiases = new float[nodes];

        weightsArray = new float[nodes, inputs];
        biasesArray = new float[nodes];
        nodeArray = new float[nodes];
    }

    public void Forward(float[] inputsArray)
    {
        nodeArray = new float[nodes];

        //Debug.Log("ba " + weightsArray.Length);
        //Debug.Log("bw " + bestWeights.Length);
        for (int i = 0; i < nodes; i++)
        {
            for(int j = 0; j < inputs; j++)
            {
                nodeArray[i] += weightsArray[i, j] * inputsArray[j]; //nodeArray[i] += weightsArray[i, j] * inputsArray[j];
            }
            nodeArray[i] += biasesArray[i]; // biasesArray[i];
        }

    }

    public void Activation()
    {
        for(int i = 0; i < nodes; i++)
        {
            if (nodeArray[i] < 0)
            {
                nodeArray[i] = 0;
            }
        }
    }

    public void MutateLayer(int points)
    {
        if (points == 0)
        {
            points = 1;
        }
        //new float[,] newArr = new float[ub];
        //Debug.Log("before: " + weightsArray[0,0] + " " + bestWeights[0,0]);
        //weightsArray = bestWeights;
        Array.Copy(bestWeights, weightsArray, bestWeights.Length);
        //biasesArray = bestBiases;
        Array.Copy(bestBiases, biasesArray, bestBiases.Length);
        //Debug.Log("after  " + weightsArray[0,0] + " " + bestWeights[0, 0]);

        for (int i = 0; i < nodes; i++)
        {
            for(int j = 0; j < inputs; j++)
            {
                weightsArray[i, j] += UnityEngine.Random.Range(-0.20f, 0.20f) / (float) points * 10; //* 10 - points;
            }
            biasesArray[i] += UnityEngine.Random.Range(-0.20f, 0.20f) / (float) points * 10; // * 10 - points;
        }
        //Debug.Log("after mutation " + weightsArray[0, 0] + " " + bestWeights[0, 0]);
    }
}
