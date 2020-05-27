using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar;

    // Set healthbar value for the boss life
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }

    // Set healthbar value and max value for the boss life
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
}
