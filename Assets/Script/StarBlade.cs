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

        // Check if player is reasonably close and above the star
        float verticalDistance = player.position.y - transform.position.y;
        float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);

        if (verticalDistance > 0.5f && horizontalDistance < 1f)
        {
            hasScored = true;
            ScoreManager.instance?.AddScore(1);
            Destroy(gameObject); // or play vanish effect
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
