using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class SpawnerLevel3 : MonoBehaviour
{
    public Enemy enemyHuman;
    public Enemy enemyAndroid;
    private List<Enemy> enemiesHuman;
    private List<Enemy> enemiesAndroid;

    [Range(0, 100)]
    public int numberOfEnemies;
    private float range = 150.0f;

    public int deadEnemies;

    public int indexSpawnEnemies;

    void Start()
    {
        int difficolta = PlayerPrefs.GetInt("Difficolta");

        switch (difficolta)
        {
            case 0:
                numberOfEnemies = 30;
                break;
            case 1:
                numberOfEnemies = 60;
                break;
            case 2:
                numberOfEnemies = 100;
                break;
            default:
                numberOfEnemies = 30;
                break;
        }


        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            numberOfEnemies *= 2;
        }


        enemiesHuman = new List<Enemy>(); // init as type
        enemiesAndroid = new List<Enemy>(); // init as type
        deadEnemies = 0;


        for (indexSpawnEnemies = 0; indexSpawnEnemies < numberOfEnemies / 2; indexSpawnEnemies++)
        {
            Enemy spawned = Instantiate(enemyHuman, RandomNavmeshLocation(range), Quaternion.identity) as Enemy;
            enemiesHuman.Add(spawned);
            spawned.generateHumans = true;
        }

        while (indexSpawnEnemies < numberOfEnemies)
        {
            Enemy spawned = Instantiate(enemyAndroid, RandomNavmeshLocation(range), Quaternion.identity) as Enemy;
            enemiesAndroid.Add(spawned);
            indexSpawnEnemies++;
            spawned.generateHumans = false;
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
