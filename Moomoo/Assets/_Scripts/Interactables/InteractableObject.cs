using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable, IClickable
{
    public void OnClick()
    {
        SetState(InteractableState.interacting);
        GameManager.instance.AddTargetGroupMember(this.gameObject);
        GameManager.instance.SetGameState(GameState.interacting);
    }
}
