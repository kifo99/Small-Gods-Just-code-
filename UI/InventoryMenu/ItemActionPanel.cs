using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour 
    { 
        [SerializeField]
        private GameObject buttonPrefab;
        public void AddButton(string name, Action OnClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => OnClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        public void Toggle(bool val)
        {
            if(val == true)
            {
                RemoveOldButtons();
            }
            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach(Transform transformChildObj in transform)
            {
                Destroy(transformChildObj.gameObject);
            }
        }
    }
}
