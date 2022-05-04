using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    //Variables for enemy positionning
    private Vector3 spawnMark = new Vector3(-14.37f, 0, 41.0f);
    private float gapBetweenLanes = 9.67f;
    private GameObject[] lanes = new GameObject[4];

    private bool isReadyToSpawn = false;

    //Variable for difficulty and score count
    private float spawnCooldown;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        SetDifficulty(1);
        StartCoroutine(NextSpawnCooldown(spawnCooldown));

    }

    // Update is called once per frame
    void Update()
    {
        //for test purpose
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isReadyToSpawn)
        {
            InstanciateEnemy(enemyPrefab[0]);            
        }


    }

    private void InstanciateEnemy(GameObject enemy)
    {
        int randomLane = Random.Range(0, lanes.Length);
        if (lanes[randomLane] == null)
        {
            Vector3 lanePosition = new Vector3(spawnMark.x + randomLane * gapBetweenLanes, spawnMark.y, spawnMark.z);
            lanes[randomLane] = Instantiate(enemy, lanePosition, enemy.transform.rotation);
            isReadyToSpawn = false;
            StartCoroutine(NextSpawnCooldown(spawnCooldown));
        }
    }


    IEnumerator NextSpawnCooldown(float timer)
    {
        yield return new WaitForSeconds(timer);
        isReadyToSpawn = true;
    }

    private void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                spawnCooldown = 1.0f;
                break;
        }
    }

}

    
/*
 * Spawn positions :
 *-14.37 -4.7
 *9.67
 *
 * instanciation:
 * tire au hasard une voie de circulation
 * on instancie le véhicule
 * 
 * si un véhicule est sur une voie de circulation on ne peux pas instancier à cette position
*/
