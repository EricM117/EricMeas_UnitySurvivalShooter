using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int score;
    public static int highScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        Instance = this;
        scoreText = GetComponent<TextMeshProUGUI>();
        score = 0;
        LoadGameState();
    }

    public void SaveGameState()
    {
        // Save variable
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void LoadGameState()
    {
        // Load variable
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void ShowScore()
    {
        scoreText.text = "Score: " + score;
        if (score >= highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore.ToString();
            SaveGameState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SaveGameState();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGameState();
        }
    }
}
