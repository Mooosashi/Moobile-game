using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : Interactable, IClickable
{
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
    [SerializeField] public TextAsset inkJSON;

    public void OnClick()
    {
        Debug.Log("Interacting with " + this.gameObject.name);

        SetState(InteractableState.interacting);
        GameManager.instance.AddTargetGroupMember(this.gameObject);
        GameManager.instance.SetGameState(GameState.interacting);
    }
}
