using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableState { neutral, interactable, interacting }
public class Interactable : MonoBehaviour
{
    public InteractableState currentState = InteractableState.neutral;
    public InteractableState CurrentState { get => currentState; private set => currentState = value; }

    private float targetGroupWeight = 2f;
    public float TargetGroupWeight { get => targetGroupWeight; [SerializeField] private set => targetGroupWeight = value; }

    public void SetState (InteractableState state)
    {
        CurrentState = state;
    }

    private void Start()
    {
        currentState = InteractableState.neutral;
    }

    // I'm testing this branching thingy
}
