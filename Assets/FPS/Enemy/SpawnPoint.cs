using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate;
    private void Start()
    {
        StartCoroutine(SpawnEnemyEveryX());
    }
    IEnumerator SpawnEnemyEveryX()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
