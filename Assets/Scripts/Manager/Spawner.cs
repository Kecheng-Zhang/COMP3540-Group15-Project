using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyList;
    public float spawnRadiusMin;
    public float spawnRadiusMax;
    public float enemyHPGrouth;

    private int waveNum;
    private int enemyNum;

    private GameManager gameManager;
    private BuffManager buffManager;

    // Start is called before the first frame update
    void Start()
    {
        waveNum = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buffManager = GameObject.Find("GameManager").GetComponent<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyNum == 0)
        {
            waveNum++;
            gameManager.addLevel();
            
            // spawn enemies
            int enemyNormNum = waveNum + 2;
            int enemyFastNum = waveNum / 2;
            int enemyShooterNum = waveNum / 3;
            spawnWave(enemyNormNum, 0, spawnRadiusMax + waveNum * 2);
            spawnWave(enemyFastNum, 1, spawnRadiusMax + waveNum);
            spawnWave(enemyShooterNum, 2, spawnRadiusMax + waveNum * 2 / 3);
        }
    }

    /**
     * <summary>
     * Spawn an enemy of the given index.
     * </summary>
     * <param name="index">The index of the enemy to spawn.</param>
     * <param name="rMax">The maximum radius to spawn the enemy.</param>
     */
    private void spawnIndex(int index, float rMax)
    {
        float spawnRadius = Random.Range(spawnRadiusMin, rMax);
        float randRad = Random.Range(0f, 2*Mathf.PI);
        float xPos = spawnRadius * Mathf.Cos(randRad);
        float zPos = spawnRadius * Mathf.Sin(randRad);
        Vector3 spawnCoord = new Vector3(xPos, 0, zPos);

        GameObject enemy = Instantiate(enemyList[index], spawnCoord, transform.rotation);
        EnemyControl enemyControl = enemy.GetComponent<EnemyControl>();
        enemyControl.maxHP *= (1f + Mathf.Pow(waveNum * enemyHPGrouth, 2));
        enemyControl.HP = enemyControl.maxHP;
    }

    /**
     * <summary>
     * Spawn a wave of enemies.
     * </summary>
     * <param name="numToSpawn">The number of enemies to spawn.</param>
     * <param name="enemyIndex">The index of the enemy to spawn.</param>
     * <param name="rMax">The maximum radius to spawn the enemies.</param>
     */
    private void spawnWave(int numToSpawn, int enemyIndex, float rMax)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            spawnIndex(enemyIndex, rMax);
        }
    }
}
