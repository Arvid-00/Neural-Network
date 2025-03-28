using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform ball;

    void Update()
    {
        float posX = ball.position.x;
        posX += 5;
        this.transform.position = new Vector3(posX, this.transform.position.y, this.transform.position.z);
    }
}
