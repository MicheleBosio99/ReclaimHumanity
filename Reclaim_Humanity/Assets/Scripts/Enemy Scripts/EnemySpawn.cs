using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyTypePrefab; // for every spawn there's only one type of enemy
    [SerializeField] private List<GameObject> spawnedEnemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float maxSpawnDelay = 15f;
    [SerializeField] private float minSpawnDelay = 8f;

    private PolygonCollider2D _spawnAreaCollider;
    
    private void Start() {
        _spawnAreaCollider = GetComponent<PolygonCollider2D>();
        if (_spawnAreaCollider != null && enabled)
        {
            StartCoroutine(SpawnEnemyRoutine());
        }
    }

    IEnumerator SpawnEnemyRoutine() {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        if (spawnedEnemies.Count < maxEnemies) { SpawnEnemies(); }
    }
    
    private void SpawnEnemies()
    {
        minSpawnDelay = 3f * spawnedEnemies.Count;
        
        for (var i = 0; i < Random.Range(1, maxEnemies + 1); i++) 
        {
            var newEnemy = Instantiate(enemyTypePrefab, GetRandomSpawnPositionWithinCollider(), Quaternion.identity, gameObject.transform);
            spawnedEnemies.Add(newEnemy);

            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.SetEnemySquad(spawnedEnemies);
            enemyScript.EnemyRoutine();
        }
        
    }

    private Vector2 GetRandomSpawnPositionWithinCollider() {
        var bounds = _spawnAreaCollider.bounds;
    
        var foundPoint = false;
        var maxAttempts = 100;
    
        Vector2 foundPosition = Vector2.zero;

        while (!foundPoint && maxAttempts > 0) {
            foundPosition =  new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
            foundPoint = _spawnAreaCollider.OverlapPoint(foundPosition);

            maxAttempts --;
        }
        return foundPosition;
    }
    
    
}