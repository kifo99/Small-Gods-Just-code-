using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    private EnemyMover enemyMover;

    private Vector2 pointerInput, movmentInput;

    public Vector2 PointerInput {get => pointerInput; set { pointerInput = value;}}
    public Vector2 MovmentInput {get => movmentInput; set { movmentInput = value;}}

    

    private void Awake() {
        enemyMover = GetComponent<EnemyMover>();
    }

    private void Update() 
    {
        enemyMover.MovmentInput = movmentInput;

        
        
    }

    public void PerformAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("Attack");
    }

}
