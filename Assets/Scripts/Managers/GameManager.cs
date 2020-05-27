using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public float playerHealth;
    public bool isPlatformer;
    private bool gameOver;
    private bool victory;

    void Start()
    {
        gameOver = false;
        victory = false;
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool GameWon()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            return false;
        } else
        {
            return true;
        }
    }

    public void SetGameOver()
    {
            gameOver = true;
            SceneManager.LoadScene("GameOver");
    }

    public bool IsGameOver()
    {
        if (gameOver)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void setVictory()
    {
        SceneManager.LoadScene("Victory");
    }
}
