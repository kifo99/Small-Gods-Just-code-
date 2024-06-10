using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingEnemyBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private EnemySO enemyStats;

    [SerializeField]
    private float force;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rotation = MathF.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);

        Destroy(this.gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject
                .GetComponent<PlayerHealthSystem>()
                .UpdatePlayerHealth((int)-enemyStats.EnemyDemage);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
