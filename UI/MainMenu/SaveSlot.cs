using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField]
    private string profileId = "";

    [Header("Content")]
    [SerializeField]
    private GameObject noDataContent;

    [SerializeField]
    private GameObject hasDataContent;

    [SerializeField]
    private TextMeshProUGUI saveGameName;

    private Button saveSlotButton;
    private void Awake() {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);


            saveGameName.text = this.profileId;
        }
    }


    public string GetProfileId()
    {
        return this.profileId;
    }

    public SaveSlot GetSaveSlot()
    {
        return this;
    }
    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
}
