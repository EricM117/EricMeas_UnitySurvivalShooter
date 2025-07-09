using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public PlayerHealth health;

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Current Lives: "+ health.currentLives.ToString();
    }
}
