using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveForward : MonoBehaviour
{

    public NN nn;
    public Rigidbody2D rigidbody;
    Vector2 startingPos;
    public float speed;
    public PipeManager pipeManager;
    int points = 0;
    bool jumping = false;
    float jumpTimer = 0;
    float TimeBeforeJump = 0.5f;


    ///private Dictionary<float, float[]> weightMap = new Dictionary<float, float[]>();
    float[] bestWeight;
    float bestReward = 0;

    Vector2 referencePoint;

    private void Awake()
    {
        startingPos = transform.position;

        referencePoint = pipeManager.GetNextRefPoint();
    }
    void Update()
    {
        Vector2 vel = rigidbody.velocity;
        vel.x = speed;
        rigidbody.velocity = vel;

        // Connect to NN brain here ?
        

        //

        if (jumpTimer >= TimeBeforeJump)
        {
            float[] inputs = { referencePoint.x - transform.position.x, transform.position.y - referencePoint.y };

            float[] outputArray = nn.Brain(inputs);
            float output = outputArray[0];
            if (output > 0f)
            {
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                jumpTimer = 0;
            }
        }
        jumpTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("open"))
        {
            pipeManager.SpawnPipe(); //These must be correct Order
            referencePoint = pipeManager.GetNextRefPoint(); //These must be correct Order
            points++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("pipe") || collision.gameObject.tag.Equals("floor"))
        {
            Reset();
        }
    }

    private void Reset()
    {
        nn.Mutate((int)bestReward);
        if (points > bestReward)
        {
            Debug.Log("sets new standard reaching: " + points + "compared to previous: " + bestReward);
            nn.SetBestWeights();
            nn.SetBestBiases();
            bestReward = points;
        }

        rigidbody.velocity = Vector2.zero;
        transform.position = startingPos;
        pipeManager.Reset(); //important order
        referencePoint = pipeManager.GetNextRefPoint(); //important order
        points = 0;

    }
}
