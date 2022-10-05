using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public Enemy enemy;
    private List<Enemy> enemies;

    [Range(0, 100)]
    public int numberOfEnemies;
    private float range = 150.0f;

    public int deadEnemies;

    void Start()
    {
        int difficolta = PlayerPrefs.GetInt("Difficolta");

        switch (difficolta)
        {
            case 0: numberOfEnemies = 30;
                break;
            case 1: numberOfEnemies = 60;
                break;
            case 2: numberOfEnemies = 100;
                break;
            default:
                numberOfEnemies = 30;
                break;
        }

        enemies = new List<Enemy>(); // init as type
        deadEnemies = 0;

        for (int index = 0; index < numberOfEnemies; index++)
        {
            Enemy spawned = Instantiate(enemy, RandomNavmeshLocation(range), Quaternion.identity) as Enemy;
            enemies.Add(spawned);
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