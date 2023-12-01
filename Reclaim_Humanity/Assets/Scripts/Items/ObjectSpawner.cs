using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "IteratorNeverReturns")]
public class ObjectSpawner : MonoBehaviour {
    
    [SerializeField] private GameObject playerInventory;
    
    [SerializeField] private List<GameObject> possibleItemsPrefabs;
    [SerializeField] private int minItems = 5;
    [SerializeField] private int maxItems = 12;
    [SerializeField] private float minSpawnDelay = 15f;
    [SerializeField] private float maxSpawnDelay = 25f;

    private PolygonCollider2D spawnAreaCollider;
    
    [SerializeField] private bool enabled = true;
    
    private float spawnTimer;
    
    private List<GameObject> spawnedItems = new List<GameObject>();

    public bool Enabled {
        get => enabled;
        set => enabled = value;
    }

    private void Start() {
        spawnedItems = new List<GameObject>();
        spawnAreaCollider = GetComponent<PolygonCollider2D>();
        if (spawnAreaCollider != null && enabled) { SpawnItems(); }
    }
    
    IEnumerator SpawnItemsRoutine() {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        if (spawnedItems.Count == 0) { SpawnItems(); }
    }

    private void SpawnItems() {
        for (var i = 0; i < Random.Range(minItems, maxItems + 1); i++) {
            var newItem = Instantiate(GetRandomElement(possibleItemsPrefabs), GetRandomSpawnPositionWithinCollider(),
                    Quaternion.identity, gameObject.transform);
            
            var itemBehaviour = newItem.GetComponent<ItemBehaviour>();
            itemBehaviour.SetParentObjectReference(gameObject);
            itemBehaviour.SetPlayerInventoryInstance(playerInventory);
            
            spawnedItems.Add(newItem);
        }
    }
    
    private GameObject GetRandomElement(List<GameObject> objectsList) { return objectsList[Random.Range(0, objectsList.Count)]; }

    private Vector2 GetRandomSpawnPositionWithinCollider() {
        var bounds = spawnAreaCollider.bounds;
        
        var foundPoint = false;
        var maxAttempts = 100;
        
        Vector2 foundPosition = Vector2.zero;

        while (!foundPoint && maxAttempts > 0) {
            foundPosition =  new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
            foundPoint = spawnAreaCollider.OverlapPoint(foundPosition);

            maxAttempts --;
        }
        return foundPosition;
    }

    public void ItemGotPickedUp(GameObject item) {
        if (spawnedItems.Contains(item)) { spawnedItems.Remove(item); }
        if (spawnedItems.Count == 0) { StartCoroutine(SpawnItemsRoutine()); }
    }
}
