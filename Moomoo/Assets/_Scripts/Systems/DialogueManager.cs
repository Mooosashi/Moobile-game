using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choiceButtons;
    [SerializeField] private GameObject dialogueChoicesPanel;

    [Header("Parameters")]
    [SerializeField] private float exitDelay = 0.2f;
    [SerializeField] private int maxChoices = 4;

    private Story currentStory;


    private void Awake()
    {
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += ContinueStory;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= ContinueStory;
    }


    private void Start()
    {
        dialogueText.text = "";
    }


    // DIALOGUE

    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        ContinueStory(new Vector2(0, 0));
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(exitDelay);

        dialogueText.text = "";
        gameManager.SetGameState(GameState.freeMovement);
    }

    private void ContinueStory(Vector2 position)
    {
        if (gameManager.CurrentGameState != GameState.interacting)
            return;

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue(); // Updates the text component in the canvas
            DisplayChoices();
        }
        else
        {

            StartCoroutine(ExitDialogue());
        }
    }


    // CHOICES

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > maxChoices)
            Debug.LogError("The number of choices in the Ink story is greater than the number of choices supported. Max number of choices supported: " + maxChoices + ".");

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choiceButtons[index].SetActive(true);
            choiceButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            index++;
        }
    }

    public void SelectChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
