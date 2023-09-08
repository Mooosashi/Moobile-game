using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public enum GameState { test, freeMovement, interacting, pause, dead }

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Debugging")]
    [SerializeField] private TextMeshProUGUI debugGameState;
    [SerializeField] private TextMeshProUGUI debugPlayerState;
    public void SetDebugGameStateText(GameState state) { debugGameState.text = "Game state: " + state + "."; }
    public void SetDebugPlayerStateText(PlayerState state) { debugPlayerState.text = "Player state: " + state + "."; }


    [Header("Systems")]
    [SerializeField] public UIManager UIManager;
    [SerializeField] public DialogueManager dialogueManager;
    [SerializeField] public InputManager inputManager;

    [Header("General references")]
    [SerializeField] private GameObject UIJoystick;

    [Header("Interaction parameters")]
    [SerializeField] private GameObject currentInteractable;
    [SerializeField] private GameObject closeUpCamera;
    [SerializeField] private CinemachineTargetGroup closeUpTargetGroup;

    [SerializeField] private float targetGroupWeight = 2f;
    public float TargetGroupWeight { get => targetGroupWeight; [SerializeField] private set => targetGroupWeight = value; }

    [SerializeField] private float targetGroupRadius = 2f;
    public float TargetGroupRadius { get => targetGroupRadius; [SerializeField] private set => targetGroupRadius = value; }

    private GameState currentGameState;

    public GameState CurrentGameState {
        get => currentGameState;
        
        private set
        {
            currentGameState = value;

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

    private void Start()
    {
        SetGameState(GameState.freeMovement);
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
    }


    public void AddTargetGroupMember(GameObject interactableObject)
    {
        currentInteractable = interactableObject;
        closeUpTargetGroup.AddMember(currentInteractable.transform, TargetGroupWeight, TargetGroupRadius);
    }
    private void RemoveTargetGroupMember()
    {
        if (currentInteractable)
        {
            closeUpTargetGroup.RemoveMember(currentInteractable.transform);
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
        SetDebugGameStateText(GameState.freeMovement);
    }

    private void InteractingState()
    {
        closeUpCamera.SetActive(true);
        UIJoystick.SetActive(false);
        UIManager.ShowInteractionMenu();
        dialogueManager.EnterDialogue(currentInteractable.GetComponent<Object>().inkJSON);
        SetDebugGameStateText(GameState.interacting);
    }
}
