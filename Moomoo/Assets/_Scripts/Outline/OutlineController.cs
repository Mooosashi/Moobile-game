using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Outline outlineScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineScript.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineScript.enabled = false;
        }
    }
}
