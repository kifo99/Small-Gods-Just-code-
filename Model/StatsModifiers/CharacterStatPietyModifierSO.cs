using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CharacterStatPietyModifierSO : CharacterStatModifierSO
{
   public override void AffectCharacter(GameObject character, float val)
    {
        PlayerPietySystem playerHealth = character.GetComponent<PlayerPietySystem>();
        if(playerHealth != null)
        {
            playerHealth.UpdatePlayerPiety((int)val);
        }
    }
}
