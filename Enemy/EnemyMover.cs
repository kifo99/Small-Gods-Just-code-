using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private EnemySO enemyStats;

    
    private Vector2 oldMovmentInput;

    public Vector2 MovmentInput { get; set; }

    private float tempSpeed = 0f;
    [SerializeField]
    private Animator animator;
    private Vector2 movment;
    private bool isMoving = false;

    
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        
    }



    private void  FixedUpdate()
    {
        if(MovmentInput.magnitude > 0 && enemyStats.EnemyMovmentSpeed >= 0)
        {
            
            oldMovmentInput = MovmentInput;
            tempSpeed =  enemyStats.EnemyMovmentSpeed + enemyStats.EnemyAcceleration * enemyStats.EnemyMaxSpeed * Time.deltaTime;
            isMoving = true;
        }
        else
        {
            tempSpeed = enemyStats.EnemyMovmentSpeed - enemyStats.EnemyDeacceleration * enemyStats.EnemyMaxSpeed * Time.deltaTime;
        }

        enemyStats.EnemyMovmentSpeed = Mathf.Clamp(enemyStats.EnemyMovmentSpeed, 0, enemyStats.EnemyMaxSpeed);
        rb.velocity = oldMovmentInput * tempSpeed;

        

        animator.SetFloat("Horizontal", MovmentInput.x);
        animator.SetFloat("Vertical", MovmentInput.y);
        animator.SetBool("isRunning", isMoving);
    }
}
