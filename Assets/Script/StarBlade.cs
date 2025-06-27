using UnityEngine;

public class StarBlade : MonoBehaviour
{
    private bool hasScored = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null || hasScored) return;

        float verticalDistance = player.position.y - transform.position.y;
        float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);

        // Tighter check: player must be close horizontally and clearly above
        bool isAbove = verticalDistance > 0.6f;
        bool isAligned = horizontalDistance < 0.4f;

        // Optional: only score if player is falling
        bool isFalling = player.GetComponent<Rigidbody2D>().linearVelocity.y < 0;

        if (isAbove && isAligned && isFalling)
        {
            hasScored = true;
            ScoreManager.instance?.AddScore(1);
            Destroy(gameObject);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameOverManager>()?.TriggerGameOver();
        }
    }
}
