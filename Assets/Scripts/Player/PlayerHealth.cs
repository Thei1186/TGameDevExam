using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private float currentHealth;

    private void Start()
    {
        currentHealth = GameManager.GetInstance().playerHealth;
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            GameManager.GetInstance().SetGameOver();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        gameObject.GetComponentInParent<Animator>().SetTrigger("GetHurt");
        currentHealth -= damage;
    }

    public void SavePlayerHealthValue()
    {
        GameManager.GetInstance().playerHealth = currentHealth;
    }

}
