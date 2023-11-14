using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable, IClickable
{
    [SerializeField] private string variableName;
    [SerializeField] private string itemName;
    [SerializeField] private GameObject model;

    public void OnClick()
    {
        SetState(InteractableState.interacting);
        GameManager.instance.currentInteractable = this.gameObject;
        GameManager.instance.SetGameState(GameState.interacting);
    }

    public void AddItem()
    {
        Debug.Log("Added to inventory: " + itemName);

        model.SetActive(false);

        // Add to inventory logic goes here cutie :)
    }
}
