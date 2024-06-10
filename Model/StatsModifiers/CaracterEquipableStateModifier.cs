using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CaracterEquipableStateModifier : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        SpellScriptableObject playerSpells = character.gameObject.GetComponent<Spell>().ActiveSpellObj;
        playerSpells.DamageAmount += val;
    }
}
