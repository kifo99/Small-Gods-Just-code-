using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    [SerializeField]
    public SpellScriptableObject[] SpellObj;

    public SpellScriptableObject ActiveSpellObj;

    [SerializeField]
    public GameObject[] SpellToCast;

    public GameObject ActiveSpell;

    [SerializeField]
    private Transform castPoint;

    public int currentPiety;

    [SerializeField]
    private int pietyRecharge;

    [SerializeField]
    private Image firstSpell;

    [SerializeField]
    private Image secondSpell;

    private void Awake()
    {
        ActiveSpell = SpellToCast[0]; // Postavljam moć sa indeksa 0 kao osnovnu moć
        ActiveSpellObj = SpellObj[0];// Postavljam podatke za moć sa indeksa 0 kao osnovnu moć
        firstSpell.enabled = true;
        secondSpell.enabled = false;

        currentPiety = this.gameObject.GetComponent<PlayerPietySystem>().GetPlayerPiety();
    }

    public void Active()
    {
        if (InputManager.GetInstance().GetDefaultSpellPressed())
        {
            ActiveSpell = SpellToCast[0];
            ActiveSpellObj = SpellObj[0];
            firstSpell.enabled = true;
            secondSpell.enabled = false;
        }
        else if (InputManager.GetInstance().GetHolySpellPressed())
        {
            ActiveSpell = SpellToCast[1];
            ActiveSpellObj = SpellObj[1];
            firstSpell.enabled = false;
            secondSpell.enabled = true;
        }
    }
    public void CastSpell(GameObject spell)
    {
        if (ActiveSpell == SpellToCast[0])
        {
            if (InputManager.GetInstance().GetSpellCastPressed())
            {
                Instantiate(spell, castPoint.position, castPoint.rotation);
                currentPiety -= (int)ActiveSpellObj.ManaCost;
                
                AudioManager.instance.PlaySFX(AudioManager.instance.castSpell);
                this.gameObject
                    .GetComponent<PlayerPietySystem>()
                    .UpdatePlayerPiety(-(int)ActiveSpellObj.ManaCost);
                Debug.Log("Holy Torn Spell Casted");
            }
        }
        else if (ActiveSpell == SpellToCast[1])
        {
            if (InputManager.GetInstance().GetSpellCastPressed())
            {
                Instantiate(spell, castPoint.position, castPoint.rotation);
                currentPiety -= (int)ActiveSpellObj.ManaCost;
                
                AudioManager.instance.PlaySFX(AudioManager.instance.castSpell);
                this.gameObject
                    .GetComponent<PlayerPietySystem>()
                    .UpdatePlayerPiety(-(int)ActiveSpellObj.ManaCost);
                Debug.Log("Holy Spirit Spell Casted");
            }
        }
    }
}
