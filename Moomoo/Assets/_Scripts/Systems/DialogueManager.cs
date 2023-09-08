using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;


    private void Start()
    {
        dialogueText.text = "";
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        ContinueStory();
    //    }
    //}

    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        ContinueStory();
    }

    public void ExitDialogue()
    {
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue(); // Updates the text component in the canvas
        }
        else
        {
            ExitDialogue();
        }
    }
}
