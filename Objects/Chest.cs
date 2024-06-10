using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField]
    private GameObject visualCue;

    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private InventoryItem item;
    private bool playerInRange;

    private bool isPickedObject = false;

    void Update()
    {
        if(playerInRange && !DialogueManager.GetInstance().dialoguePlaying)
        {
            visualCue.SetActive(true);
            if(InputManager.GetInstance().GetInteractionPressed() && isPickedObject == false)
            {
                inventoryData.AddItem(item);
                AudioManager.instance.PlaySFX(AudioManager.instance.pickup);
                isPickedObject = true;
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
