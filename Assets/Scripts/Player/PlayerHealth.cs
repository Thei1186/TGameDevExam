using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private float currentHealth;
    private GameManager gmInstance;

    private void Start()
    {
        gmInstance = GameManager.GetInstance();
        currentHealth = gmInstance.playerHealth;
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            gmInstance.SetGameOver();
            Destroy(gameObject);
        }
    }

    public void GetHealed(float heal)
    {
        float maxHealth = gmInstance.playerHealth;
        if ((currentHealth + heal) >  maxHealth)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth += heal;
        }
        
    }

    public void TakeDamage(float damage)
    {
        gameObject.GetComponentInParent<Animator>().SetTrigger("GetHurt");
        currentHealth -= damage;
    }

    public void SavePlayerHealthValue()
    {
        gmInstance.playerHealth = currentHealth;
    }

}
