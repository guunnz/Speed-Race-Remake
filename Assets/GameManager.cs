using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyCar;
    public Transform SpawnPoint;

    public GameObject Bomberos;

    private List<CarEnemy> carEnemyList = new List<CarEnemy>();

    private float NextSpawn;
    private float LastSpawnRandomDif;

    public Vector2 SpawnRandomDifference;

    public void Update()
    {
        if (SpawnPoint.transform.position.y >= NextSpawn)
        {
            if (carEnemyList.Count < 16)
            {
                GameObject carEnemy = Instantiate(EnemyCar, new Vector2(Random.Range(-2f, Bomberos.activeSelf ? 1.5f : 4.6f), SpawnPoint.position.y), Quaternion.identity, null);

                carEnemyList.Insert(0, carEnemy.GetComponent<CarEnemy>());
            }
            else
            {
                CarEnemy carEnemy = carEnemyList.Last();

                carEnemyList.RemoveAt(carEnemyList.Count - 1);

                carEnemy.transform.position = new Vector2(Random.Range(-3f, 3f), NextSpawn);

                carEnemy.Respawn();


                carEnemyList.Insert(0, carEnemy);

            }
            LastSpawnRandomDif = Random.Range(SpawnRandomDifference.x, SpawnRandomDifference.y);

        }
        NextSpawn = carEnemyList.First().transform.position.y + LastSpawnRandomDif;
    }
}