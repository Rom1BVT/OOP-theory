using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    //Variables for enemy positionning
    private Vector3 spawnMark = new Vector3(-14.37f, 0, 41.0f);
    private float gapBetweenLanes = 9.67f;
    private GameObject[] lanes = new GameObject[4];
    private bool isReadyToSpawn = false;

    //Variable for difficulty and score count
    private Difficulty difficultyManager;
    [SerializeField] protected TextMeshProUGUI scoreText;
    private int numberEnemyOfThisType;
    private int maxEnemyOfThisTypeAllowed;

    private int currentDifficulty;
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            if (value < 0)
            {
                Debug.Log("You can't set a negative value");
            }
            else
            {
                score = value;
                scoreText.text = $"Score: {score}";
            }
        }       
    }


    private void Awake()
    {
        difficultyManager = new Difficulty();
    }
    void Start()
    {
        currentDifficulty = 0;
        score = 0;
        scoreText.text = $"Score: {score}";
        difficultyManager.SetDifficulty(currentDifficulty);
        StartCoroutine(NextSpawnCooldown(difficultyManager.spawnCooldown));

    }

    // Update is called once per frame
    void Update()
    {

        if (isReadyToSpawn)
        {
            int randomEnemy = Random.Range(0,2);
            InstanciateEnemy(enemyPrefab[randomEnemy]); 
        }

        if(score >= difficultyManager.pointToReach)
        {
            difficultyManager.SetDifficulty(currentDifficulty + 1);
        }

    }

    private void InstanciateEnemy(GameObject enemy)
    {

        // check how many enemies is present in scene
        int currentEnemies = 0;
        for(int i = 0; i < lanes.Length; i++)
        {
            if(lanes[i] != null)
            {
                currentEnemies++;               
            }
        }

        // check how many enemy of each type is present
        Van[] numberOfVans = GameObject.FindObjectsOfType<Van>();
        Tank[] numberOfTanks = GameObject.FindObjectsOfType<Tank>();
        Plane[] numberOfPlanes = GameObject.FindObjectsOfType<Plane>();

        //Depending on the nature of vehicles, check how many is allowed of this type

        switch (enemy.gameObject.name)
        {
            case "Van":
                numberEnemyOfThisType = numberOfVans.Length;
                maxEnemyOfThisTypeAllowed = difficultyManager.maxVanInScene;
                break;

            case "Tank":
                numberEnemyOfThisType = numberOfTanks.Length;
                maxEnemyOfThisTypeAllowed = difficultyManager.maxTankInScene;
                break;

            case "Plane":
                numberEnemyOfThisType = numberOfPlanes.Length;
                maxEnemyOfThisTypeAllowed = difficultyManager.maxPlaneInScene;
                break;
        }



        //Instanciate an ennemy if the lane is empty and if the max is not reach
        int randomLane = Random.Range(0, lanes.Length);
        if (lanes[randomLane] == null && currentEnemies < difficultyManager.maxEnemyInScene)
        {
            if (numberEnemyOfThisType < maxEnemyOfThisTypeAllowed)
            {
                Vector3 lanePosition = new Vector3(spawnMark.x + randomLane * gapBetweenLanes, spawnMark.y, spawnMark.z);
                lanes[randomLane] = Instantiate(enemy, lanePosition, enemy.transform.rotation);
                isReadyToSpawn = false;
                StartCoroutine(NextSpawnCooldown(difficultyManager.spawnCooldown));
            }
        }
        else if (numberOfVans.Length >= difficultyManager.maxEnemyInScene)
        {
            isReadyToSpawn = false;
            StartCoroutine(NextSpawnCooldown(difficultyManager.spawnCooldown));
        }

    }


    IEnumerator NextSpawnCooldown(float timer)
    {
        yield return new WaitForSeconds(timer);
        isReadyToSpawn = true;
    }

    
    public void SetScore(int scoreToAdd)
    {
        score += scoreToAdd;
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
