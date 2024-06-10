using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HollyPush : MonoBehaviour
{
    public SpellScriptableObject SpellToCast;
    private Rigidbody2D rb;
    

    [SerializeField]
    private float force;
    private Vector3 mousPos;
    private Vector3 direction;

    private void Awake()
    {  

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        
        Destroy(this.gameObject, SpellToCast.Lifetime);
    }

    private void Start()
    {
        ThrowTorn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject
                .GetComponent<EnemyHealth>()
                .UpdateEnemyHealth(SpellToCast.DamageAmount);
        }
        Destroy(this.gameObject);
    }

    private void ThrowTorn()
    {
        rb = GetComponent<Rigidbody2D>();
        mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousPos - transform.position).normalized;
        Vector3 rotation = transform.position - mousPos;

         rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
         float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(0,0,rot + 90);
    }

}
