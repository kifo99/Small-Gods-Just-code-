using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDataPersistence
{    private int playerHealth = 0;

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform spawnPoint;


    [SerializeField]
    private HealthBar healthBar;
    public bool isDead = false;
    private void Awake()
    {
        playerHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealth += amount;

        healthBar.SetHealth(playerHealth);

        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
            
        }
        else if (playerHealth <= 0f)
        {
            playerHealth = 0;
            isDead = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.death);
            animator.SetBool("IsDead",isDead);
            AbilityController(false);
            StartCoroutine(Respawn());
        }
    }

    public void LoadData(GameData data)
    {
        this.playerHealth = data.playerHealth;
        healthBar.SetHealth(playerHealth);
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = this.playerHealth;
        healthBar.SetHealth(playerHealth);
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.transform.position = new Vector3(spawnPoint.position.x,spawnPoint.position.y, spawnPoint.position.z);
        isDead = false;
        AbilityController(true);
        UpdatePlayerHealth(maxHealth);
        int tempPiety = this.gameObject.GetComponent<PlayerPietySystem>().GetMaxPiety();
        this.gameObject.GetComponent<PlayerPietySystem>().UpdatePlayerPiety(tempPiety);
        animator.SetBool("IsDead", isDead);
    }



    public void AbilityController(bool status)
    {
        this.gameObject.GetComponent<PlayerController>().enabled = status;
        this.gameObject.GetComponent<Spell>().enabled = status;
        this.gameObject.GetComponent<SpellController>().enabled = status;
        this.gameObject.GetComponent<InventoryController>().enabled = status;
    }
}

