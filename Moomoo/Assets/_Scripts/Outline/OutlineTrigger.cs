using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Outline outlineScript;
    [SerializeField] private Interactable interactableScript;

    [Header("Parameters")]
    [SerializeField] private float outlineSpeed = 5f;

    private bool shouldOutline = false;
    private float startingWidth = 0f;


    private void Start()
    {
        startingWidth = outlineScript.OutlineWidth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineScript.enabled = true;
            shouldOutline = true;
            outlineScript.OutlineWidth = 0f;
            interactableScript.SetState(InteractableState.interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldOutline = false;
            outlineScript.OutlineWidth = startingWidth;
            interactableScript.SetState(InteractableState.neutral);
        }
    }

    private void Update()
    {
        if (shouldOutline)
            outlineScript.OutlineWidth = Mathf.MoveTowards(outlineScript.OutlineWidth, startingWidth, outlineSpeed * Time.deltaTime);
        else if (!shouldOutline && outlineScript.OutlineWidth != 0)
            outlineScript.OutlineWidth = Mathf.MoveTowards(outlineScript.OutlineWidth, 0f, outlineSpeed * Time.deltaTime);
            
    }
}
