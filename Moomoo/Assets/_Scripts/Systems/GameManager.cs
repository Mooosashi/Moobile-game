using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum GameState { freeMovement, interacting, pause, dead }
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Systems")]
    [SerializeField] private UIManager UIManager;

    [Header("General references")]
    [SerializeField] private GameObject UIJoystick;

    [Header("Interaction parameters")]
    [SerializeField] private Transform currentInteractable;
    [SerializeField] private GameObject closeUpCamera;
    [SerializeField] private CinemachineTargetGroup closeUpTargetGroup;

    [SerializeField] private float targetGroupWeight = 2f;
    public float TargetGroupWeight { get => targetGroupWeight; [SerializeField] private set => targetGroupWeight = value; }

    [SerializeField] private float targetGroupRadius = 2f;
    public float TargetGroupRadius { get => targetGroupRadius; [SerializeField] private set => targetGroupRadius = value; }


    private GameState currentState;

    public GameState CurrentState {
        get => currentState;
        
        private set {
            currentState = value;

            switch (value)
            {
                case GameState.freeMovement:
                    FreeMovementState();
                    break;

                case GameState.interacting:
                    InteractingState();
                    break;

                case GameState.pause:
                    Debug.Log("Pause state");
                    break;

                default:
                    break;
            }
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SetState(GameState state)
    {
        CurrentState = state;
    }

    public void AddTargetGroupMember(Transform interactableObject)
    {
        currentInteractable = interactableObject;
        closeUpTargetGroup.AddMember(currentInteractable, TargetGroupWeight, TargetGroupRadius);
    }
    private void RemoveTargetGroupMember()
    {
        if (currentInteractable)
        {
            closeUpTargetGroup.RemoveMember(currentInteractable);
            currentInteractable.gameObject.GetComponent<Object>().SetState(InteractableState.interactable);
            currentInteractable = null;
        }
    }


    // ON GAME STATE CHANGE LOGIC
    private void FreeMovementState()
    {
        closeUpCamera.SetActive(false);
        UIJoystick.SetActive(true);
        RemoveTargetGroupMember();
        UIManager.HideInteractionMenu();
    }

    private void InteractingState()
    {
        closeUpCamera.SetActive(true);
        UIJoystick.SetActive(false);
        UIManager.ShowInteractionMenu();
    }
}
