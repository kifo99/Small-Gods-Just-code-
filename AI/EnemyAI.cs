using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private EnemySO enemyStats;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.01f;

    public UnityEvent <Vector2>OnMoveInput;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSteering movementDirectionSolver;

    private void Start()
    {
        
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }

    }

    private void Update() 
    {
        EnemyMovment();
    }
   
    private void EnemyMovment ()
    {
        if(aiData.currentTarget != null)
        {
            //OnPointerInput?.Invoke(aiData.currentTarget.position);
            //if(isChasing == false)
            //{
                //isChasing = true;
                StartCoroutine(ChaseAndAttack());

            //}
        }
        else if(aiData.GetTargetsCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }
        OnMoveInput?.Invoke(movementInput);
    }

    private IEnumerator ChaseAndAttack()
    {
        if(aiData.currentTarget == null)
        {
            //Debug.Log("Stopping");
            movementInput = Vector2.zero;
            //isChasing = false;
            yield break;
        }
        else
        {
            //float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);


            // if(distance < enemyStats.Range)
            // {
                
            //     movementInput = Vector2.zero;
            //     //Call attack methode
            //     //OnAttackPressed?.Invoke();
            //     meleCombat.Attack();
            //     yield return new WaitForSeconds(attackDelay);
            //     StartCoroutine(ChaseAndAttack());
            // }
            // else
            // {
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            // }
        }
    }
    
    
}
