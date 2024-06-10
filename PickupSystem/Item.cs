using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public SOItem InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    public string id;

    [ContextMenu("Create GUID for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField]
    private float duration = 0.3f;


    internal void DestroyItem()
    {

        AudioManager.instance.PlaySFX(AudioManager.instance.pickup);
        GetComponent<Collider2D>().enabled = false;
        
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;

        while (currentTime < duration)
        {
            
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
