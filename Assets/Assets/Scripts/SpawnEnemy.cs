using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;
    public Wave[] waves;
    public int timeBetweenWaves = 5;

    public GameManagerBehaviour gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    [System.Serializable]
    //This class is Serializable, which means you can change the values in the Inspector.
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnInterval = 2;
        public int maxEnemies = 20;
    }

    // Use this for initialization
    void Start () {
        lastSpawnTime = Time.time;
        gameManager =
            GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        // Get the index of the current wave, and check if it’s the last one.
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            // calculate how much time passed since the last enemy spawn
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            //check whether timeInterval is bigger than timeBetweenWaves. 
            //Otherwise, you check whether timeInterval is bigger than this wave’s spawnInterval.
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                // If necessary, spawn an enemy by instantiating a copy of enemyPrefab.
                //You also + the enemiesSpawned count. 
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)
                    Instantiate(waves[currentWave].enemyPrefab);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            // check the number of enemies on screen. 
            //If there are none and it was the last enemy in the wave you spawn the next wave
            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                // give the player 10 percent of all gold left at the end of the wave.
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            // Upon beating the last wave this runs the game won animation
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}
