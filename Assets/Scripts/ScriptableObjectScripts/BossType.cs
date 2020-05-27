using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "BossTypes", fileName = "Boss type")]
public class BossType : ScriptableObject
{
    public int startHealth;

    public float minDistance;

    public float attackSpeed;

    public int damage;
}
