using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveGame : Menu
{
    [SerializeField]
    private TMP_InputField input;

    [SerializeField]
    private List<SaveSlot> saveSlots;

    public static SaveGame instance { get; private set; }

    private void Awake()
    {
        if (this != null)
        {
            Debug.Log("Ther is more than one instance of SaveGame");
        }

        instance = this;
    }
    public void OnSaveClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        DataPersistenceManager.instance.ChangeSelectedProfileId(input.text);
        DataPersistenceManager.instance.SaveGame();
        this.gameObject.SetActive(false);
    }
}
