using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueVariables dialogueVariables;
    private DialogueInkExternalFunctions dialogueInkExternalFunctions;
    [Header("Globals Ink file (Load globals JSON file)")]
    [SerializeField] private TextAsset loadGlobalsTextAsset;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choiceButtons;
    [SerializeField] private GameObject dialogueChoicesPanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private TextMeshProUGUI dialogueSpeakerText;

    [Header("Parameters")]
    [SerializeField] private float exitDelay = 0.2f;
    [SerializeField] private int maxChoices = 4;
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSoundRegular;
    [SerializeField] private AudioClip typingSoundVoice1;
    [SerializeField] [Range(1, 5)] private int frequencyLevel = 2;

    private Story currentStory;
    private Coroutine displayLineCoroutine;
    private bool isDisplayingLine = false;
    private string currentLine = "";
    private bool hasRichText = false;

    private const string SPEAKER_TAG = "speaker";
    private const string TYPINGSOUND_TAG = "typing sound";
    private const string FUNCTION_TAG = "function";


    private void Awake()
    {
        gameManager = GameManager.instance;

        gameManager.onInteractingState += EnterDialogue;

        dialogueVariables = new DialogueVariables(loadGlobalsTextAsset);
        dialogueInkExternalFunctions = new DialogueInkExternalFunctions();
    }

    private void Start()
    {
        dialogueText.text = "";
    }


    // DIALOGUE

    public void EnterDialogue(GameObject interactable)
    {
        dialogueSpeakerText.text = "";

        TextAsset inkJSON;
        inkJSON = interactable.GetComponent<Interactable>().inkJSON;
        currentStory = new Story(inkJSON.text);
        dialogueVariables.StartListening(currentStory);
        dialogueInkExternalFunctions.Bind(currentStory);

        OnContinue();
        continueButton.SetActive(true);
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(exitDelay);

        dialogueVariables.StopListening(currentStory);
        dialogueInkExternalFunctions.Unbind(currentStory);

        dialogueText.text = "";
        gameManager.SetGameState(GameState.freeMovement);
    }

    public void OnContinue()
    {
        if (isDisplayingLine)
        {
            StopCoroutine(displayLineCoroutine);
            isDisplayingLine = false;
            dialogueText.maxVisibleCharacters = currentLine.Length;
        }
        else
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
                StopCoroutine(displayLineCoroutine);

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    dialogueSpeakerText.text = tagValue;
                    break;

                case TYPINGSOUND_TAG:
                    switch (tagValue)
                    {
                        case "Regular":
                            audioSource.clip = typingSoundRegular;
                            break;

                        case "Voice1":
                            audioSource.clip = typingSoundVoice1;
                            break;
                    }
                    break;

                case FUNCTION_TAG:
                    switch (tagValue)
                    {
                        case "AddItem":
                            gameManager.currentInteractable.GetComponent<Item>().AddItem();
                            break;
                    }
                    break;
            }
        }
    }

    private IEnumerator DisplayLine (string line)
    {
        //dialogueText.text = "";
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        isDisplayingLine = true;
        currentLine = line;
        foreach (char letter in line.ToCharArray())
        {

            if (letter == '<' || hasRichText)
            {
                hasRichText = true;
                // dialogueText.maxVisibleCharacters++;
                // dialogueText.text += letter;
                if (letter == '>')
                    hasRichText = false;
            }
            else
            {
                PlayTypingSound(dialogueText.maxVisibleCharacters);
                // dialogueText.text += letter;
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        isDisplayingLine = false;
    }

    private void PlayTypingSound(int currenDisplayedCharacterCount)
    {
        if (currenDisplayedCharacterCount % frequencyLevel == 0)
        {
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.Play();
        }
    }


    // CHOICES

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > maxChoices)
            Debug.LogError("The number of choices in the Ink story is greater than the number of choices supported. Max number of choices supported: " + maxChoices + ".");

        int index = 0;
        if (currentChoices.Count > 0)
        {
            foreach (Choice choice in currentChoices)
            {
                choiceButtons[index].SetActive(true);
                choiceButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                index++;
            }
            continueButton.SetActive(false);
        }
    }

    public void SelectChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].SetActive(false);
        }
        continueButton.SetActive(true);
        OnContinue();
    }



    // VARIABLES

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
            Debug.LogWarning("Ink variable was found to be null: " + variableName);

        return variableValue;
    }



    private void OnDestroy()
    {
        gameManager.onInteractingState -= EnterDialogue;
    }
}
