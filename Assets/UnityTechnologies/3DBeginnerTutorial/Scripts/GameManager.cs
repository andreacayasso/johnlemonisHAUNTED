using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool hasWon = false;
    public Transform targetDestination;

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void CheckWinCondition()
    {
        
        float distanceToTarget = Vector3.Distance(transform.position, targetDestination.position);

        if (distanceToTarget < 2f)
        {
            hasWon = true;
            RestartGame(); 
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (!hasWon)
        {
            CheckWinCondition();
        }
    }
}
