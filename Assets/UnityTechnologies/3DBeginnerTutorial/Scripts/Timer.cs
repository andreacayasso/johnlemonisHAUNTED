using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeLimit = 120.0f; 
    private float currentTime;
    public GameObject timerText;
    public GameObject levelCompleteUI; 
    private bool isLevelComplete = false;
    private GameManager gameManager;

    void Start()
    {
        currentTime = timeLimit;
        gameManager = FindObjectOfType<GameManager>(); 
    }

    void Update()
    {
        if (!isLevelComplete && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            if (!isLevelComplete)
            {
                Debug.Log("Time's up! Game Over.");
                if (gameManager != null)
                {
                    gameManager.RestartLevel(); 
                }
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null) 
        {
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            string timerString = string.Format("{0:0}:{1:00}", minutes, seconds); 
            timerText.GetComponent<Text>().text = "Time: " + timerString;

            if (currentTime < 10.0f)
            {
                timerText.GetComponent<Text>().color = Color.yellow;
            }
        }
        else
        {
            Debug.LogError("TimerText is not assigned!");
        }
    }

    public void LevelComplete()
    {
        isLevelComplete = true;
        currentTime = 0; 
        levelCompleteUI.SetActive(true); 
        Debug.Log("Level Complete!");
    }
}
