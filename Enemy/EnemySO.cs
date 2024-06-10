using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    [SerializeField]
    public float EnemyMovmentSpeed;

    [SerializeField]
    public float EnemyMaxSpeed;

    [SerializeField]
    public float EnemyAcceleration;

    [SerializeField]
    public float EnemyDeacceleration;

    [SerializeField]
    public int EnemyMaxHealth;

    [SerializeField]
    public float EnemyDemage;

    [SerializeField]
    public float Range;

    [SerializeField]
    public float AttackRate;

    [SerializeField]
    public string EnemyName;
}
