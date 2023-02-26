using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] AudioClip gameOverSfx;
    [SerializeField] Canvas gameOver;
    [SerializeField] Canvas resumeCanvas;
    GameSession gameSession;
    playerMovement player;
    EnemyMovement enemy;
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NextScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MakeMeActive()
    {
        GetComponent<AudioSource>().PlayOneShot(gameOverSfx);
        gameOver.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.RestartGameSession();
    }

    public void PreviousScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        enemy = FindObjectOfType<EnemyMovement>();
        player = FindObjectOfType<playerMovement>();
        enemy.speed = player.enemySpeed;
        resumeCanvas.gameObject.SetActive(false);
        player.isAlive = true;
    }

    public void Pause()
    {
        if (!gameOver.isActiveAndEnabled) { resumeCanvas.gameObject.SetActive(true); }
        
    }

}
