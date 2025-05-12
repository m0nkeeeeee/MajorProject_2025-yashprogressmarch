using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectPrefab;
    public GameObject ObjectPrefab2;
    public BoxCollider spawnArea; // Assign in Inspector
    public float lifetime = 10f; // Time before destruction
    private int currentSpawned = 0;

    public void SpawnSphere()
    {
        if (spawnArea == null)
        {
            Debug.LogError("Spawn area BoxCollider not assigned!");
            return;
        }
        Vector3 spawnPosition = GetRandomPointInBounds(spawnArea.bounds);

        // 🔀 Randomly choose between ObjectPrefab and ObjectPrefab2
        GameObject prefabToSpawn = (Random.value < 0.5f) ? ObjectPrefab : ObjectPrefab2;

        GameObject inst = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        currentSpawned++;

        if (lifetime > 0)
        {
            Destroy(inst, lifetime);
        }
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
