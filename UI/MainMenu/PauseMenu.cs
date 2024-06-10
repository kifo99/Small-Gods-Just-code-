using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject spellPanel;

    public void OnResumeClicked()
    {
        pauseMenu.SetActive(false);
        spellPanel.SetActive(true);
        player.gameObject.GetComponent<Spell>().enabled = true;
        player.gameObject.GetComponent<SpellController>().enabled = true;
    }

    public void OnQuiteClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void OnSettingsClicked()
    {
        optionsPanel.active = true;
    }
    public void OnBackClicked()
    {
        optionsPanel.active = false;
    }
    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }
}
