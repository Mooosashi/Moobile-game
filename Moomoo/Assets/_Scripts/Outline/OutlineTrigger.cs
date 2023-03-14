using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Outline outlineScript;
    [SerializeField] private Interactable interactableScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineScript.enabled = true;
            interactableScript.SetState(InteractableState.interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineScript.enabled = false;
            interactableScript.SetState(InteractableState.neutral);
        }
    }
}
