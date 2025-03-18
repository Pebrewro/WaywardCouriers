using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> spawnPoints;

    private List<GameObject> activeEnemies = new List<GameObject>();
    
    [Header("Next Level")]
    [SerializeField] private Animator anim;

    private void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        CheckAllEnemiesDead();
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject newEnemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(newEnemy);
        }
    }

    private void CheckAllEnemiesDead()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count == 0)
        {
            anim.SetBool("isQuest", true);
        }
    }

}
