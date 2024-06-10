using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowLevelDemonAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private EnemySO enemyStats;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject player;

    private float nextAttackTime = 0f;
    private float timer = 0f;

    private void FixedUpdate() 
    {
        Attack();
    }

    public void Attack()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        timer = Time.time;
        

        if(distance < enemyStats.Range)
        {
            if(nextAttackTime < timer )
            {
                Debug.Log("Attack");
                player.gameObject.GetComponent<PlayerHealthSystem>().UpdatePlayerHealth((int)-enemyStats.EnemyDemage);
                nextAttackTime = enemyStats.AttackRate + timer;
            }
            
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,enemyStats.Range);
    }
}
