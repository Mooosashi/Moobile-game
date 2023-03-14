using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : Interactable, IClickable
{
    [Header("References")]
    [SerializeField] private GameObject closeUpVirtualCamera;
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    public void Interact()
    {
        Debug.Log(this.gameObject.name + " interaction");

        GameManager.instance.SetState(GameState.interacting);
        cinemachineTargetGroup.AddMember(this.gameObject.transform, TargetGroupWeight, 1);
    }
}
