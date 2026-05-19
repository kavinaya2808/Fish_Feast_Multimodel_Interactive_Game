using UnityEngine;
public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public float spawnInterval = 2f;
    public float yMin = -3.5f;
    public float yMax = 3.5f;
    public float spawnX = 10f;

    void Start()
    {
        InvokeRepeating("SpawnFish", 1f, spawnInterval);
    }

    void SpawnFish()
    {
        int index = Random.Range(0, fishPrefabs.Length); // Pick random fish
        float randomY = Random.Range(yMin, yMax);         // Pick random height
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);

        Instantiate(fishPrefabs[index], spawnPos, Quaternion.identity);
    }
}
