using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public static PowerUpSpawn instance = null;

    public int hitCount;

    float timeRate = 1f;
    float nextTime;

    public static Transform spawnPosition;

    //Stack<PowerUp>...

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeLimit();
    }

    public void CountingHits(Transform tokenPos)
    {
        if (hitCount == 0)
            nextTime = Time.time;
            spawnPosition = tokenPos;
        if ((Time.time - nextTime) < timeRate)
        {
            hitCount++;
            nextTime = Time.time;
        }
    }

    void CheckTimeLimit()
    {
        if ((Time.time - nextTime) > timeRate)
        {
            switch (hitCount)
            {
                case 5:
                    SpawnStar();
                    hitCount = 0;
                    break;
                case 8:
                    SpawnSpeedUp();
                    hitCount = 0;
                    break;
                case 10:
                    SpawnSuperJump();
                    hitCount = 0;
                    break;
                default:
                    hitCount = 0;
                    break;
            }
        }
    }

    private void SpawnSuperJump()
    {
        Debug.Log("SpawnSuperJump");
    }

    private void SpawnSpeedUp()
    {
        Debug.Log("SpawnSpeedUp");
    }

    private void SpawnStar()
    {
        Debug.Log("SpawnStar");
    }
}
