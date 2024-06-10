using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Params")]
    [SerializeField]
    private float movingSpeed = 5f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    private Vector2 movment;
    private int prayingRecharge = 35;
    private int healingRecharge = 35;

    [SerializeField]
    private Image selectedImagePraying;

    [SerializeField]
    private Image selectedImageHealing;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject spellPanel;
    private float nextPrayingStart = 0f;
    private float nextHealingStart = 0f;
    private float prayingTimer = 0f;
    private float healingTimer = 0f;

    private void Update()
    {
        animator.SetFloat("Horizontal", movment.x);
        animator.SetFloat("Vertical", movment.y);
        animator.SetFloat("Speed", movment.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialoguePlaying)
        {
            return;
        }
        PlayerMovment();
        Praying();
        Healing();
        OpenPauseMenu();
    }

    private void PlayerMovment()
    {
        movment = InputManager.GetInstance().GetMoveDirection();

        rb.MovePosition(rb.position + movment * movingSpeed * Time.deltaTime);
    }

    private void Praying()
    {
        prayingTimer = Time.time;

        if (InputManager.GetInstance().GetPrayPressed())
        {
            if (nextPrayingStart < prayingTimer)
            {
                selectedImagePraying.enabled = true;
                this.gameObject
                    .GetComponent<PlayerPietySystem>()
                    .UpdatePlayerPiety(prayingRecharge);
                AudioManager.instance.PlaySFX(AudioManager.instance.praying);
                nextPrayingStart = 20 + prayingTimer;
            }
        }
        else
        {
            selectedImagePraying.enabled = false;
        }
    }

    private void OpenPauseMenu()
    {
        if (InputManager.GetInstance().GetPausePressed())
        {
            pauseMenu.SetActive(true);
            spellPanel.SetActive(false);
            this.gameObject.GetComponent<Spell>().enabled = false;
            this.gameObject.GetComponent<SpellController>().enabled = false;
        }
    }

    private void Healing()
    {
        healingTimer = Time.time;
        if (InputManager.GetInstance().GetHealingSpellPressed())
        {
            if (this.gameObject.GetComponent<PlayerPietySystem>().GetPlayerPiety() >= 20)
            {
                if (nextHealingStart < healingTimer)
                {
                    selectedImageHealing.enabled = true;
                    this.gameObject
                        .GetComponent<PlayerHealthSystem>()
                        .UpdatePlayerHealth(healingRecharge);
                    this.gameObject.GetComponent<PlayerPietySystem>().UpdatePlayerPiety(-20);
                    AudioManager.instance.PlaySFX(AudioManager.instance.healing);
                    nextHealingStart = 20 + healingTimer;
                }
            }
        }
        else
        {
            selectedImageHealing.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Chest")
        {
            Debug.Log("Got your prize");
        }
    }

    
}
