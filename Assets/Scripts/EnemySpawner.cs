using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float heightRange = 5.0f;
    public Transform tutorialSpawn;
    public Transform leftSpawn;
    public Transform rightSpawn;
    public GameObject fishPrefab;
    public GameObject blowfishPrefab;

    void Start()
    {
    }
    
    void Update()
    {
        
    }

    private void SpawnFish()
    {
        float sidePicker = Random.Range(0.0f, 1.0f);
        float randomHeight = Random.Range(-heightRange, heightRange);
        if (sidePicker > 0.5f)
        {
            Vector3 spawnPosition = new Vector3(leftSpawn.position.x, leftSpawn.position.y, leftSpawn.position.z + randomHeight);
            GameObject thisFish = Instantiate(fishPrefab, spawnPosition, fishPrefab.transform.rotation);
            thisFish.GetComponent<EnemyFishController>().SetSpeed(3);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(rightSpawn.position.x, rightSpawn.position.y, rightSpawn.position.z + randomHeight);
            GameObject thisFish = Instantiate(fishPrefab, spawnPosition, fishPrefab.transform.rotation);
            thisFish.GetComponent<EnemyFishController>().SetSpeed(-3);
        }
    }

    private void SpawnFishTutorial()
    {
        GameObject thisFish = Instantiate(fishPrefab, tutorialSpawn.position, fishPrefab.transform.rotation);
        thisFish.GetComponent<EnemyFishController>().SetSpeed(-3);
    }

    private void SpawnBlowfish()
    {
        float sidePicker = Random.Range(0.0f, 1.0f);
        float randomHeight = Random.Range(-heightRange, heightRange);
        if (sidePicker > 0.5f)
        {
            Vector3 spawnPosition = new Vector3(leftSpawn.position.x, leftSpawn.position.y, leftSpawn.position.z + randomHeight);
            GameObject thisFish = Instantiate(blowfishPrefab, spawnPosition, fishPrefab.transform.rotation);
            thisFish.GetComponent<BlowfishController>().SetSpeed(2);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(rightSpawn.position.x, rightSpawn.position.y, rightSpawn.position.z + randomHeight);
            GameObject thisFish = Instantiate(blowfishPrefab, spawnPosition, fishPrefab.transform.rotation);
            thisFish.GetComponent<BlowfishController>().SetSpeed(-2);
        }
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnBlowfish");
        CancelInvoke("SpawnFish");
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnBlowfish", 5, 4);
        InvokeRepeating("SpawnFish", 0, 2);
    }

    public void StartTutorialSpawn()
    {
        InvokeRepeating("SpawnFishTutorial", 2, 6);
    }

    public void EndTutorialSpawn()
    {
        CancelInvoke("SpawnFishTutorial");
    }
}
