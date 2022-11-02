using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyCar;
    public Transform SpawnPoint;

    public TrackManager TrackManager;
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

                float randomPos = TrackManager.CurrentTrack == TrackType.Bridge ? (Random.Range(0, 2) == 0 ? Random.Range(-3f, -1.5f) : Random.Range(3, 1.5f)) : Random.Range(-3f, Bomberos.activeSelf ? 1.5f : 3f);
                GameObject carEnemy = Instantiate(EnemyCar, new Vector2(randomPos, SpawnPoint.position.y), Quaternion.identity, null);

                carEnemyList.Insert(0, carEnemy.GetComponent<CarEnemy>());
            }
            else
            {
                CarEnemy carEnemy = carEnemyList.Last();

                carEnemyList.RemoveAt(carEnemyList.Count - 1);
                float randomPos = TrackManager.CurrentTrack == TrackType.Bridge ? (Random.Range(0, 2) == 0 ? Random.Range(-3f, -1.5f) : Random.Range(3, 1.5f)) : Random.Range(-3f, Bomberos.activeSelf ? 1.5f : 3f);

                carEnemy.transform.position = new Vector2(randomPos, NextSpawn);

                carEnemy.Respawn();


                carEnemyList.Insert(0, carEnemy);

            }
            LastSpawnRandomDif = Random.Range(SpawnRandomDifference.x, SpawnRandomDifference.y);

        }
        NextSpawn = carEnemyList.First().transform.position.y + LastSpawnRandomDif;
    }
}