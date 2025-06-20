using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); // Prevent duplicate instances
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    // ✅ This is what you need
    public int GetScore()
    {
        return score;
    }
}
