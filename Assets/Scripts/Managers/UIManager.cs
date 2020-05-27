using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Each button represents an ability")]
    private Button[] abilityButtons;

    private KeyCode action1;

    // The time to compare with the time gotten at game start to check whether the ability is ready.
    private float nextFireTime = 0;

    // A list of cooldown times based on index
    private List<float> cooldownTime = new List<float>();

    private void Start()
    {
        action1 = KeyCode.Alpha1;

        // Loops through all of the players abilities and adds their cooldown value to list of cooldowns
        // This is used to keep track of the individual ability cooldown, the indexes must match.
        GameObject[] abilities = GameObject.FindObjectOfType<PlayerCombat>().abilities;
        for (int i = 0; i < abilities.Length; i++)
        {
            cooldownTime.Add(abilities[i].GetComponent<AbilityManager>().abilityCooldown); 
        }
    }
    private void Update()
    {
        // handles the firing of each ability
        if(Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(action1))
            {
                AbilityButtonOnClick(0);
                nextFireTime = Time.time + cooldownTime[0];
            }
        }
    }

    // Uses the ability tied to a button based on index
    private void AbilityButtonOnClick(int btnIndex)
    {
        if (Time.time > nextFireTime)
        {
            abilityButtons[btnIndex].onClick.Invoke();
        }
    }
}
