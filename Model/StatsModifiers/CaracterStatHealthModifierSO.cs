using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CaracterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerHealthSystem playerHealth = character.GetComponent<PlayerHealthSystem>();
        if(playerHealth != null)
        {
            playerHealth.UpdatePlayerHealth((int)val);
        }
    }
}
