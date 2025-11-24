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

    public float spawncooldown = 0.5f;
    List<GameObject> wave = new List<GameObject>();
    int currentWave = 1;
    int deadEnemies = 0;

    public Transform playerTransform;


    /*
     * ==================================================================
     * Coroutines et fonctions (sauf wave qui est tout en bas
     * ==================================================================
     */
    //Pour chaque ennemi qu'on veut faire spawn, le fait apparaître, l'ajoute à la liste et attend 0.5 secondes avant le suivant.
    IEnumerator SpawnBase(int ammount)
    {
        int numberSpawned = 0;
        while (numberSpawned < ammount) 
        { 
            GameObject newmob = Instantiate(enemy1, (cameraposition.position + spawnoffset), spawnrotation);
            wave.Add(newmob);
            yield return new WaitForSeconds(spawncooldown);
            numberSpawned++;
        }
    }

    //Pareil avec l'autre type d'ennemi
    IEnumerator SpawnShield(int ammount)
    {
        int numberSpawned = 0;
        while (numberSpawned < ammount)
        {
            GameObject newmob = Instantiate(enemy2, (cameraposition.position + spawnoffset), spawnrotation);
            wave.Add(newmob);
            yield return new WaitForSeconds(spawncooldown);
            numberSpawned++;
        }
    }

    //Fonction pour reset la position du joueur
    void ResetPlayerPosition()
    {
        playerTransform.position = new Vector3 (cameraposition.position.x, 0,0);
    }

    /*
     * ==================================================================
     * En jeu
     * ==================================================================
     */
    //Active la gestion des vagues dans le start.
    private void Start()
    {
        StartCoroutine(Wave());
    }

    private void Update()
    {
        //Admin command pour faire spawn un enemmi.
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(SpawnBase(1));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(SpawnShield(1));
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ResetPlayerPosition();
        }
        //Check si la vague actuelle a été remportée par le joueur et si oui passe à la suivante.
        deadEnemies = 0;
        foreach (GameObject obj in wave)
        {
            if (obj == null) 
            { 
                deadEnemies++;
            }
        }
        if (deadEnemies == wave.Count)
        {
            wave.Clear();
            currentWave++;
        }
    }

    //Gère les vagues d'ennemis
    IEnumerator Wave()
    {
        StartCoroutine(SpawnBase(2));
        while (currentWave != 2)
        {
            yield return null;
        }
        ResetPlayerPosition ();
        StartCoroutine(SpawnShield(1));
        while (currentWave != 3)
        {
            yield return null;
        }
        ResetPlayerPosition();
        Debug.Log("Skibidi toilets");
    }
}
