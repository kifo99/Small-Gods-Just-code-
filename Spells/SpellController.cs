using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{

    [SerializeField]
    private Spell spell;

    [SerializeField]
    private float spellCastRadius;

    [SerializeField]
    private float cantCastRadius;

    
   


    void Update() 
    { 

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spell.Active();
        bool hasMana = this.gameObject.GetComponent<PlayerPietySystem>().GetPlayerPiety() - spell.ActiveSpellObj.ManaCost >= 0f;


        if(hasMana )
        {
            if(Vector2.Distance(mousePosition,transform.position) < spellCastRadius && Vector2.Distance(mousePosition,transform.position) > cantCastRadius)
            {
                spell.CastSpell(spell.ActiveSpell);
            }
        }
    }


    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spellCastRadius);
        

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, cantCastRadius);
        
    }
}
