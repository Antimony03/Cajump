using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public float spawnInterval = 5f;
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
        if (starPrefab == null)
        {
            Debug.LogError("Star prefab is null! Cannot spawn.");
            CancelInvoke(nameof(SpawnStar)); // Stop spawner
            return;
        }

        Vector2 spawnPos = new Vector2(Random.Range(xMin, xMax), ySpawn);
        GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = star.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleDeg = Random.Range(180f, 360f);
            float angleRad = angleDeg * Mathf.Deg2Rad;

            float speed = Random.Range(minSpeed, maxSpeed);
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            rb.linearVelocity = direction * speed;
        }
    }

}
