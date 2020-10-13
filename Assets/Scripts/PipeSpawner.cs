﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Pipe;
    void Start()
    {
        SpawnPipe(5, 5);
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            SpawnPipe(3, 7);
        }
    }

    private void SpawnPipe(int rangeStart, int rangeEnd)
    {
        float rand = Random.Range(rangeStart, rangeEnd);
        GameObject newPipe = Instantiate(Pipe, new Vector3(4, rand, 0), Quaternion.identity);
        Destroy(newPipe, 8);
    }
}