using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    UIManager UI;
    [SerializeField] int playerLives;
    [SerializeField] TextMeshProUGUI bulletTxt;
    [SerializeField] TextMeshProUGUI livesTxt;
    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeBulletCount(int noOfBullets)
    {
        bulletTxt.text = noOfBullets.ToString();
    }

    void Start()
    {
        livesTxt.text = playerLives.ToString();
        bulletTxt.text = 0.ToString();
    }

    public void processPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            UI = FindObjectOfType<UIManager>();
            UI.MakeMeActive();
            livesTxt.text = 0.ToString();
        }
    }

    void TakeLife()
    {
        playerLives--;
        livesTxt.text = playerLives.ToString();
        StartCoroutine(Restart());
    }

    public void RestartGameSession()
    {
        FindObjectOfType<ScencePersist>().ResetScencePersist();
        SceneManager.LoadScene(1); 
        Destroy(gameObject);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScenceIndex);
    }

}
