using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DialogueManager : MonoBehaviour, IDataPersistence
{
    [Header("Dialogue UI")]
    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI displayNameTxt;

    [SerializeField]
    private Animator portraitAnimator;

    [Header("Globals JSON")]
    [SerializeField]
    private TextAsset loadGlobalsJSON;

    [Header("Choices UI")]
    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField]
    private GameObject player;

    private Story currentStory;

    public bool dialoguePlaying { get; private set; }

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";

    private DialogueVariables dialogueVariables;

    private static DialogueManager instance;

    private InkExternalFunctions inkExternalFunctions;

    [SerializeField]
    private GameObject pietyBar;

    [SerializeField]
    private GameObject healthBar;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }

        instance = this;

        inkExternalFunctions = new InkExternalFunctions();
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialoguePlaying)
        {
            return;
        }
        if (currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            
            ContinueStory();
        }
        
        
    }

    public Story GetCurrentStory()
    {
        return currentStory;
    }

    public void EnterDialogueMod(TextAsset inkJSON)
    {
        player.gameObject.GetComponent<Spell>().enabled = false;
        player.gameObject.GetComponent<SpellController>().enabled = false;
        pietyBar.SetActive(false);
        healthBar.SetActive(false);
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory);

        ContinueStory();
    }

    private void ExiteDialogueMod()
    {
        player.gameObject.GetComponent<Spell>().enabled = true;
        player.gameObject.GetComponent<SpellController>().enabled = true;
        pietyBar.SetActive(true);
        healthBar.SetActive(true);
        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);
        
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExiteDialogueMod();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTags = tag.Split(":");
            if (splitTags.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed.");
            }

            string tagKey = splitTags[0].Trim();
            string tagValue = splitTags[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameTxt.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                default:
                    Debug.Log("Tag came in but is not currently bing handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError(
                "More choices were given than UI can support. Number of choices given: "
                    + currentChoices.Count
            );
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager.GetInstance().RegisterSubmitPressed();
        ContinueStory();
    }
    public void LoadData(GameData data)
    {
        dialogueVariables = new DialogueVariables(loadGlobalsJSON, data.globalVariablesStateJson);
    }

    public void SaveData(ref GameData data)
    {
        string globalStateJson = dialogueVariables.GetGlobalVariablesStateJson();

        data.globalVariablesStateJson = globalStateJson;
    }
}
