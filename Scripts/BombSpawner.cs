using UnityEngine;
using System.Collections.Generic;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float spawnInterval = 2f;
    public Vector2 xRange = new Vector2(-8f, 8f);
    public float spawnY = 6f;
    public float minDistanceFromFish = 1.5f; // safe gap from existing fish

    void Start()
    {
        InvokeRepeating("SpawnBomb", 2f, spawnInterval);
    }

    void SpawnBomb()
    {
        // Get all small fish currently in the scene
        GameObject[] smallFish = GameObject.FindGameObjectsWithTag("SmallFish");
        List<float> fishXPositions = new List<float>();

        foreach (GameObject fish in smallFish)
        {
            fishXPositions.Add(fish.transform.position.x);
        }

        const int maxAttempts = 10;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = Random.Range(xRange.x, xRange.y);

            bool isTooCloseToFish = false;
            foreach (float fishX in fishXPositions)
            {
                if (Mathf.Abs(fishX - x) < minDistanceFromFish)
                {
                    isTooCloseToFish = true;
                    break;
                }
            }

            if (!isTooCloseToFish)
            {
                Vector3 spawnPos = new Vector3(x, spawnY, 0f);
                Instantiate(bombPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        // No valid position found after several attempts
        Debug.Log("⚠️ Bomb spawn skipped to avoid overlapping with small fish.");
    }
}
