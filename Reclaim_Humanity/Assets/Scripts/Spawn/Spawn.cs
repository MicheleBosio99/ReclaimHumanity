using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject spawnPoint;
    private SpawnHandler spawnHandler;
    private float delay=0;
    private float time;
    private bool go=false;
    [SerializeField ] private Enemy enemy;

    private void Awake()
    {
        spawnHandler = FindObjectOfType<SpawnHandler>();
    }

    public void ActivateSpawn()
    {
        if (!spawnPoint.activeInHierarchy)
        {
            spawnPoint.SetActive(true);
            spawnHandler.NumberOfSpawnsActiveIncrement();
            go = true;
        }
            
    }

    private void Update()
    {
        delay += Time.deltaTime;
        if (spawnPoint.activeInHierarchy && go && delay > spawnTimer)
        {
            enemy.SpawnPositionEnemy();
            enemy.ActivateEnemy();
            StartCoroutine(enemy.FOVRoutine());
            delay = 0;
            spawnTimer += spawnTimer;
            go = false;
            
        }

    }

}
