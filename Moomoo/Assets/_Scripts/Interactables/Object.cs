using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : Interactable, IClickable
{
    [Header("Interaction")]
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    public void OnClick()
    {
        Debug.Log("Interacting with " + this.gameObject.name);

        SetState(InteractableState.interacting);
        GameManager.instance.SetState(GameState.interacting);

        GameManager.instance.AddTargetGroupMember(this.gameObject.transform);
    }
}
