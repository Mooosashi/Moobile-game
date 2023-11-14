using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable, IClickable
{
    public void OnClick()
    {
        SetState(InteractableState.interacting);
        
        gameManager.currentInteractable = this.gameObject;
        gameManager.SetGameState(GameState.interacting);
    }
}
