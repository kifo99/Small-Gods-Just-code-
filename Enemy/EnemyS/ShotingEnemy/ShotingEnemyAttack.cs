using System.Transactions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingEnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform instantiatePos;

    [SerializeField]
    private EnemySO enemyStats;

    [SerializeField]
    private Transform target;

    private float timer = 0f;
    private float nextAttackTime = 0f;

    [SerializeField]
    private Animator animator;
    private bool isInRange = false;

    private void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        timer = Time.time;

        if (distance < enemyStats.Range && !target.gameObject.GetComponent<PlayerHealthSystem>().isDead)
        {
            if (nextAttackTime < timer)
            {
                Debug.Log("Attack");
                Shoot();
                nextAttackTime = enemyStats.AttackRate + timer;
            }
            isInRange = true;
        }
        else
        {
            isInRange =  false;
        }

        Vector2 direction = target.position - transform.position;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetBool("isShooting", isInRange);

        
    }

    private void Shoot()
    {
        Instantiate(bullet,instantiatePos.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(AudioManager.instance.enemyAttack);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,enemyStats.Range);
    }
}
