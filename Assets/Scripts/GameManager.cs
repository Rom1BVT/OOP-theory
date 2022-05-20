using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private GameObject player;

    //Variables for enemy positionning
    private Vector3 roadSpawnMark = new Vector3(-14.37f, 0, 41.0f);
    private Vector3 planeSpawnMark = new Vector3(-32, 0, -18);
    private float gapRoadLanes = 9.67f;
    private float gapPlaneLanes = 56.0f;
    private GameObject[] lanes = new GameObject[6];
    private bool isReadyToSpawn = false;

    //Variable for difficulty and score count
    private Difficulty difficultyManager;
    [SerializeField] protected TextMeshProUGUI scoreText;
    private int numberEnemyOfThisType;
    private int maxEnemyOfThisTypeAllowed;
    private int currentDifficulty;
    private int score;
    private string scoreTextPath = "/Canvas/Stars/Star";
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
    public bool isGameOver {  get; private set; }

    [SerializeField]private GameObject gameOverUI;
    private int titleScene = 0;

    private void Awake()
    {
        difficultyManager = new Difficulty();
        player = GameObject.Find("Player");
    }
    void Start()
    {
        isGameOver = false;
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
            int randomEnemy = Random.Range(0,enemyPrefab.Length);
            InstanciateEnemy(enemyPrefab[randomEnemy]);
        }

        if(score >= difficultyManager.pointToReach)
        {
            currentDifficulty++;
            difficultyManager.SetDifficulty(currentDifficulty);
            GameObject.Find(scoreTextPath + currentDifficulty).SetActive(true);
        }


        if (player == null)
        {
            GameOver();
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



        //Plane lanes = 0 or 6 / van and tank lanes = 1 to 5
        int randomLane;
        if (enemy.gameObject.name == "Plane")
        {
            randomLane = Random.Range(0, 2) * (lanes.Length - 1);
        }
        else
        {
            randomLane = Random.Range(1, lanes.Length - 1);
        }

        //Instanciate an ennemy if the lane is empty and if the max is not reach
        if (lanes[randomLane] == null && currentEnemies < difficultyManager.maxEnemyInScene)
        {
            if (numberEnemyOfThisType < maxEnemyOfThisTypeAllowed)
            {
                Vector3 lanePosition;
                if (enemy.gameObject.name == "Plane")
                {
                    lanePosition = new Vector3(planeSpawnMark.x + randomLane / (lanes.Length -1 ) * gapPlaneLanes, planeSpawnMark.y, planeSpawnMark.z);
                }
                else 
                { 
                    lanePosition = new Vector3(roadSpawnMark.x + (randomLane - 1) * gapRoadLanes, roadSpawnMark.y, roadSpawnMark.z); 
                }

                lanes[randomLane] = Instantiate(enemy, lanePosition, enemy.transform.rotation);
                isReadyToSpawn = false;
                StartCoroutine(NextSpawnCooldown(difficultyManager.spawnCooldown));
            }
        }
        else if (currentEnemies >= difficultyManager.maxEnemyInScene)
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


    private void GameOver()
    {
        isGameOver = true;
        gameOverUI.gameObject.SetActive(true);
        DataPersistence.Instance.UpdateLeaderboard(score);
        DataPersistence.Instance.SaveScore();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(titleScene);
    }
}

    

