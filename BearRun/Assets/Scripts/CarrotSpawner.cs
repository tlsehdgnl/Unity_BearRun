using UnityEngine;

public class CarrotSpawner : MonoBehaviour
{
    public GameObject carrotPrefab;
    public float spawnInterval = 0.5f;
    public Vector2 spawnRangeX = new Vector2(-500, 500);
    public Vector2 spawnRangeZ = new Vector2(-500, 500);
    public Vector2 spawnRangeY = new Vector2(1, 200);

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCarrot), 0f, spawnInterval);
    }

    private void SpawnCarrot()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnRangeX.x, spawnRangeX.y),
            Random.Range(spawnRangeY.x, spawnRangeY.y),
            Random.Range(spawnRangeZ.x, spawnRangeZ.y)
        );

        Instantiate(carrotPrefab, spawnPosition, Quaternion.identity);
    }
}
