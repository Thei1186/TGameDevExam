using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public float playerHealth;
    public bool isPlatformer;
 
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

    public void SetGameOver()
    {
            SceneManager.LoadScene("GameOver");
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
