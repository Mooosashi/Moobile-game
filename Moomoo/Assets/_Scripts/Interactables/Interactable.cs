using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableState { neutral, interactable, interacting }
public class Interactable : MonoBehaviour
{
    [SerializeField] public string interactableName;
    [SerializeField] public TextAsset inkJSON;

    protected GameManager gameManager;

    public InteractableState currentState = InteractableState.neutral;
    public InteractableState CurrentState
    {     
        get => currentState;

        private set
        {
            currentState = value;

            switch (value)
            {
                case InteractableState.neutral:
                    gameManager.OutRangeOfInteractable();
                    break;

                case InteractableState.interactable:
                    gameManager.InRangeOfInteractable(this.gameObject);
                    break;

                case InteractableState.interacting:
                    gameManager.OutRangeOfInteractable();
                    break;
            }
        }   
    }

    public void SetState (InteractableState state)
    {
        CurrentState = state;
    }

    private void Start()
    {
        currentState = InteractableState.neutral;
        gameManager = GameManager.instance;
    }



    [SerializeField] private float targetGroupWeight = 2f;
    public float TargetGroupWeight { get => targetGroupWeight; [SerializeField] private set => targetGroupWeight = value; }

    [SerializeField] private float targetGroupRadius = 2f;
    public float TargetGroupRadius { get => targetGroupRadius; [SerializeField] private set => targetGroupRadius = value; }

}
