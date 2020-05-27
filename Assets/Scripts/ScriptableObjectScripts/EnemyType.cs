using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyTypes", fileName ="Enemy types")]
public class EnemyType : ScriptableObject
{
    [Tooltip("The projectile that the enemy fires")]
    [SerializeField] private GameObject projectilePrefab;
    
    [Tooltip("The speed at which the enemy fires")]
    [SerializeField] private float attackSpeed;

    [Tooltip("The amount of life that the enemy has")]
    [SerializeField] private int startHealth;

    [Tooltip("The farthest the enemy can shoot")]
    [SerializeField] private float minDistance;

    [Tooltip("The animator controller for the enemy")]
    [SerializeField] private RuntimeAnimatorController Animator;

    [Tooltip("The range at which the enemy can detect the player")]
    [SerializeField] private float detectRange;

    [Tooltip("The speed at which the enemy's projectile move")]
    [SerializeField] private float shootingPower;
    public float ShootingPower => shootingPower;
    public float AttackSpeed => attackSpeed;
    public int StartHealth => startHealth;

    public float DetectRange => detectRange;

    public float MinDistance => minDistance;
    public GameObject ProjectilePrefab => projectilePrefab;

    public RuntimeAnimatorController RuntimeAnimatorController => Animator;
}
