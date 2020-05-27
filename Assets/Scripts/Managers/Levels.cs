using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    // True if player is at the entrance to the next level
    private bool isAtEntrance;
    
    // Popup message with instructions
    public TextMeshProUGUI interactText;
    private void Start()
    {
        isAtEntrance = false;
        string sceneName = SceneManager.GetActiveScene().name;
        CheckIfPlatformerScene(sceneName);
    }
    private void Update()
    {
        if (isAtEntrance == true && Input.GetKeyDown(KeyCode.E))
        {
            // Persist health when changing scene
            FindObjectOfType<PlayerHealth>().SavePlayerHealthValue();
            
            // Changes scene to the next one based on index in build
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }
    }

    // Checks if player is triggering the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetText("Press E to enter");
            interactText.gameObject.SetActive(true);
            isAtEntrance = true;
        }
    }

    // Checks if player is leaving the trigger collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
            isAtEntrance = false;
        }
    }

    // Checks if the scene needs platformer controls
    private void CheckIfPlatformerScene(string sceneName)
    {
        List<string> platformerScenes = new List<string>
        {
            "Cave", "ForestBossArena"
        };
        GameManager.GetInstance().isPlatformer = platformerScenes.Contains(sceneName);
    }
}
