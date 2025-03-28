using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public Rigidbody2D player;
    public GameObject[] pipePrefabs;
    public List<GameObject> spawnedPipes;
    public Transform finalPipe;
    private Vector2 lastSpawnPos;
    private float space = 5;
    private Vector2 referencePoint;

    private Vector2[] refPoints = new Vector2[4];
    private int currentPipeNumber = 4;
    private int nextPipe = 0;

    private void Awake()
    {
        SetUpStartPipes();
    }

    private void SetUpStartPipes()
    {
        for(int i = 0; i < 4; i++)
        {
            //make sure the referencepoint is set at 4 steps earlier
            GameObject obj = Instantiate(pipePrefabs[i], new Vector2(i * space, 0), Quaternion.identity);
            referencePoint = obj.transform.GetChild(0).position;
            spawnedPipes.Add(obj);
            refPoints[i] = referencePoint;
            lastSpawnPos = obj.transform.position;
        }
    }
    public void SpawnPipe()
    {
        int rnd = Random.Range(0, pipePrefabs.Length);
        Vector2 newSpawnPos = lastSpawnPos;
        newSpawnPos.x += space;
        GameObject obj = Instantiate(pipePrefabs[rnd], newSpawnPos, Quaternion.identity);
        spawnedPipes.Add(obj);
        referencePoint = obj.transform.GetChild(0).position;
        currentPipeNumber++;
        if(currentPipeNumber >= 4)
            currentPipeNumber = 0;
        refPoints[currentPipeNumber] = referencePoint;
        nextPipe++;
        if (nextPipe >= 4)
            nextPipe = 0;
        lastSpawnPos = newSpawnPos;
    }

    public Vector2 GetNextRefPoint()
    {
        //foreach(Vector2 v in refPoints)
            //Debug.Log(v);
        //Debug.Log("point: " + refPoints[nextPipe]);
        return refPoints[nextPipe];
    }

    public void Reset()
    {
        foreach(GameObject go in spawnedPipes)
        {
            Destroy(go);
        }
        lastSpawnPos.x = 15;
        spawnedPipes.Clear();
        nextPipe = 0;
        currentPipeNumber = 4;
        SetUpStartPipes();
    }
}
