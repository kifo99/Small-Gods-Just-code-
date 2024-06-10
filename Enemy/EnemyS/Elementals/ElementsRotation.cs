using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsRotation : MonoBehaviour
{
    private Vector3 zAxis = new Vector3(0, 0, 1);

    [SerializeField]
    private Transform target;

    private void FixedUpdate() {
        
    }

    private void Rotate()
    {
        transform.RotateAround(target.position, zAxis, 2f);
    }



    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthSystem>().UpdatePlayerHealth(-10);
            
        }
    }
}
