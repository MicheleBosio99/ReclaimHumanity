using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] spawns;
    private int numberOfSpawns;

    void Update()
    {

        if (numberOfSpawns < 2) //numero di nemici massimo che possono spawnare nella zona
        {
            spawns[FindSpawns()].GetComponent<Spawn>().ActivateSpawn();
        }
    }

    private int FindSpawns()
    {
        for (int i = Random.Range(0, spawns.Length); i < spawns.Length; i++)
        {
            if (!spawns[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public void NumberOfSpawnsActiveIncrement()
    {
        numberOfSpawns++;
    }

    public void NumberOfSpawnsActiveDecrement()
    {
        numberOfSpawns--;
    }
}
