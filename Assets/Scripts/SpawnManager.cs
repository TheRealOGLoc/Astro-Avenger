//using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float randomY;
    private float[] rocketWaveY = {-6.63f, -3.14f, 0.31f, 3.85f, 7.24f};
    private Vector3 randomPosition;
    private Vector3 diamondPosition;
    public GameObject[] objects;
    private PlayerController playerControllerScript;
    private bool rocketWave = false;
    private float enemySpawnTimeInterval = 1.5f;  //1.2f
    private int rocketIndex = 2;
    private int diamondIndex = 3;
    private int weaponIndex = 4;
    private int diamondCount = 0;
    private int diamondNumber = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(InstantiateEnemies()); // start spawn enemy coroutine
        StartCoroutine(SpawnDiamondCountDown()); // start spawn diamond coroutine
        StartCoroutine(SpawnRocketWave()); // start spawn rocket wave coroutine
        StartCoroutine(SpawnWeapon()); // start spawn weapon coroutine
        StartCoroutine(ChangeLevelOfGame()); // start change level coroutine
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // generate a random position within the boundary
    Vector3 GenerateRandomPosition()
    {
        randomY = Random.Range(-8.0f, 6.5f);
        randomPosition = new Vector3(-18.3f, randomY, -2);
        return randomPosition;
    }

    // Generate a enemy
    IEnumerator InstantiateEnemies()
    {
        while(!playerControllerScript.gameOver)
        {
            yield return new WaitForSeconds(enemySpawnTimeInterval);
            if (!rocketWave)
            {
                int randomEnemyIndex = Random.Range(0, 3);
                // if the game is not over and not in rocket wave
                Instantiate(objects[randomEnemyIndex], GenerateRandomPosition(), objects[randomEnemyIndex].transform.rotation);
            }
            
        }
    }

    // start diamond count down
    IEnumerator SpawnDiamondCountDown()
    {
        while (!playerControllerScript.gameOver)
        {
            int randomTime = Random.Range(5, 10);
            diamondCount = 0;
            yield return new WaitForSeconds(randomTime);
            StartCoroutine(SpawnDiamondWave());
        }
    }

    // generate four diamond
    IEnumerator SpawnDiamondWave()
    {
        while (diamondCount != 4)
        {
            yield return new WaitForSeconds(0.3f);
            InstantiateDiamond();
            diamondCount++;
        }

    }

    // generate a rocket wave, which has 4 rocket
    IEnumerator SpawnRocketWave()
    {
        while (!playerControllerScript.gameOver)
        {
            int randomTime = Random.Range(15, 21);
            yield return new WaitForSeconds(randomTime);
            rocketWave = true;
            // wait 2 seconds to let other enemy pass the screen
            yield return new WaitForSeconds(2); 
            InstantiateRocketWave();
            yield return new WaitForSeconds(2);
            rocketWave = false;
        }
    }

    // start a spawn weapon count down
    IEnumerator SpawnWeapon()
    {
        while (!playerControllerScript.gameOver)
        {
            int randomTime = Random.Range(20, 25);
            yield return new WaitForSeconds(randomTime);
            if (!playerControllerScript.playerHasWeapon)
            {
                InstantiateWeapon();
            }
        }
    }

    // generate 4 diamond with same x and z value
    void InstantiateDiamond()
    {
        if (!playerControllerScript.gameOver)
        {
            if (diamondNumber == -1) // generate a positon for these four diamond
            {
                diamondPosition = GenerateRandomPosition();
                diamondNumber = 0;
            }
            Instantiate(objects[diamondIndex], diamondPosition, objects[diamondIndex].transform.rotation);
            diamondNumber++;
            if (diamondNumber == 4) // if diamond number has reached 4, restart the diamond
            {
                diamondNumber = -1;
            }
        }
    }

    // generate object with given y value and object index
    void InstantiateObjectWithY(float yAxis, int objectIndex)
    {
        Vector3 objectPosition = new Vector3(-18.3f, yAxis, -2);
        Instantiate(objects[objectIndex], objectPosition, objects[objectIndex].transform.rotation);
    }

    // generate a rocket wave
    void InstantiateRocketWave()
    {
        int randomGapIndex = Random.Range(0, 5); // a random gap for player to go through
        for (int i = 0; i < rocketWaveY.Length; i++)
        {
            if (i != randomGapIndex)  // leave the gap
            {
                InstantiateObjectWithY(rocketWaveY[i], rocketIndex);
            }
        }
    }

    // generate the weapon with random y value
    void InstantiateWeapon()
    {
        randomY = Random.Range(-6.0f, 6.5f);
        InstantiateObjectWithY(randomY, weaponIndex);
    }

    // Change the level of game
    IEnumerator ChangeLevelOfGame()
    {
        while (!playerControllerScript.gameOver && enemySpawnTimeInterval > 0.5f)
        {
            yield return new WaitForSeconds(10);
            enemySpawnTimeInterval -= 0.15f;
        }
    }
    
}
