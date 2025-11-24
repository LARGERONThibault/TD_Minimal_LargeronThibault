using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public Transform cameraposition;
    public Vector3 spawnoffset;
    Quaternion spawnrotation = new Quaternion (0, 180, 0, 0);

    List<GameObject> wave = new List<GameObject>();

    void SpawnBase()
    {
       GameObject newmob = Instantiate(enemy1, (cameraposition.position + spawnoffset), spawnrotation);
        wave.Add(newmob);
    }

    void SpawnShield()
    {
        Instantiate(enemy2, (cameraposition.position + spawnoffset), spawnrotation);
    }

    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnBase();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnShield();
        }
    }
}
