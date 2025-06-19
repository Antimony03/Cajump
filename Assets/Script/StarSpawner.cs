using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public float spawnInterval = 3f;
    public float xMin = -7f;
    public float xMax = 7f;
    public float ySpawn = 5f;
    public float minSpeed = 4f;
    public float maxSpeed = 7f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnStar), 1f, spawnInterval);
    }

    void SpawnStar()
    {
        // Random spawn position above screen
        Vector2 spawnPos = new Vector2(Random.Range(xMin, xMax), ySpawn);

        // Create star
        GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);

        // Apply random velocity (angle between 180° and 360°)
        Rigidbody2D rb = star.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleDeg = Random.Range(180f, 360f); // Downward hemisphere
            float angleRad = angleDeg * Mathf.Deg2Rad;

            float speed = Random.Range(minSpeed, maxSpeed);
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            rb.linearVelocity = direction * speed;
        }
    }
}
