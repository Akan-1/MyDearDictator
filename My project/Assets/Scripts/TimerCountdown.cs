using UnityEngine;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float countdownTime = 5f;
    private float currentTime;

    private void Start()
    {
        currentTime = countdownTime;
        UpdateTimerText();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            currentTime = 0;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        timerText.text = "Игра перезапустится через " + seconds.ToString() + " сек.";
    }
}
