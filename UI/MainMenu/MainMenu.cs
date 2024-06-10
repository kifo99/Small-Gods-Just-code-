using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField]
    private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField]
    private Button newGameBtn;

    [SerializeField]
    private Button continueGameBtn;

    [SerializeField]
    private Button settingsBtn;

    [SerializeField]
    private Button quitBtn;

    [SerializeField]
    private Button backBtn;

    [SerializeField]
    private Button tutorialBtn;

    [SerializeField]
    private Button loadGameBtn;

    [SerializeField]
    private GameObject optionsPanel;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameBtn.interactable = false;
            loadGameBtn.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnTutorialClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("TutorialScene");
    }

    public void OnSettingsClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        optionsPanel.active = true;
    }

    public void OnQuitGameClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        Application.Quit();
    }

    public void OnBackClicked()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.selectUI);
        optionsPanel.active = false;
    }

    private void DisableMenuButtons()
    {
        newGameBtn.interactable = false;
        continueGameBtn.interactable = false;
        settingsBtn.interactable = false;
        quitBtn.interactable = false;
        tutorialBtn.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
