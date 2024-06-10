using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private EnemySO enemyStats;

    [SerializeField]
    private HealthBar healthBar;

    private float currentHealth;

    private void Awake() {
        currentHealth = enemyStats.EnemyMaxHealth;
        healthBar.SetHealth((int)enemyStats.EnemyMaxHealth);
    }


    public void UpdateEnemyHealth(float damageToApplay)
    {
        currentHealth -= damageToApplay;
        healthBar.SetHealth((int)currentHealth);
        if(currentHealth <= 0f)
        {
            MonsterSlayCount.GetInstance().StartCount(enemyStats.EnemyName);
            MonsterSlayCount.GetInstance().AddToMonstersDictionary();

            Destroy(this.gameObject);
            
        }
    }

}
