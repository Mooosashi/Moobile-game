using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public enum GameState { test, initialize, transition, freeMovement, interacting, pause, dead }

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
    [SerializeField] public MaskDataUpdater maskDataUpdater;

    [Header("Player")]
    [SerializeField] private GameObject playerCharacterPrefab;
    [SerializeField] public PlayerCharacter playerCharacter;

    [Header("Interaction")]
    [SerializeField] public GameObject currentInteractable;

    [Header("Scene")]
    [SerializeField] public int sceneSpawnID;
    
    [HideInInspector] public Camera mainCamera;
    [SerializeField] public List<SceneTrigger> sceneTriggers;


    private void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        #endregion

        //SceneManager.activeSceneChanged += SetGameState(GameState.initialize);

        mainCamera = Camera.main;

        sceneTriggers = new List<SceneTrigger>();
        sceneTriggers.Clear();
    }


    // STATES
    #region States
    private GameState currentGameState;
    public GameState CurrentGameState {
        get => currentGameState;
        
        private set
        {
            currentGameState = value;

            switch (value)
            {
                case GameState.initialize:
                    InitializeState();
                    break;

                case GameState.transition:
                    TransitionState();
                    break;

                case GameState.freeMovement:
                    FreeMovementState();
                    break;

                case GameState.interacting:
                    InteractingState(currentInteractable);
                    break;

                case GameState.pause:
                    Debug.Log("Pause state");
                    break;
            }
        }
    }
    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
    }


    public event Action onInitializeState;
    private void InitializeState()
    {
        SetDebugGameStateText(GameState.initialize);
        
        sceneTriggers = new List<SceneTrigger>();
        sceneTriggers.Clear();
        
        if (onInitializeState != null)
            onInitializeState();

        SetGameState(GameState.transition);

        StartCoroutine(InstantiatePlayerCharacter());
    }

    public event Action onTransitionState;
    private void TransitionState()
    {
        if (onTransitionState != null)
            onTransitionState();

        SetDebugGameStateText(GameState.transition);
    }

    public event Action onFreeMovementState;
    private void FreeMovementState()
    {
        if (onFreeMovementState != null)
            onFreeMovementState();

        SetDebugGameStateText(GameState.freeMovement);
    }

    public event Action<GameObject> onInteractingState;
    private void InteractingState(GameObject interactable)
    {
        if (onInteractingState != null)
            onInteractingState(interactable);

        SetDebugGameStateText(GameState.interacting);
    }
    #endregion


    // OTHER EVENTS
    #region Other events
    public event Action<GameObject> onInRangeOfInteractable;
    public void InRangeOfInteractable(GameObject interactable)
    {
        if (onInRangeOfInteractable != null)
            onInRangeOfInteractable(interactable);
    }

    public event Action onOutRangeOfInteractable;
    public void OutRangeOfInteractable()
    {
        if (onOutRangeOfInteractable != null)
            onOutRangeOfInteractable();
    }

    public event Action<string> onGoToScene;
    public void GoToScene(string sceneName)
    {
        if (onGoToScene != null)
            onGoToScene(sceneName);
    }

    public event Action<string> onTransitionOut;
    public void TransitionOut(string sceneName)
    {
        if (onTransitionOut != null)
            onTransitionOut(sceneName);
    }
    #endregion


    IEnumerator InstantiatePlayerCharacter()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (SceneTrigger sceneTrigger in sceneTriggers)
        {
            if (sceneTrigger.spawnID == sceneSpawnID)
            {
                Debug.Log("Spawn player character at " + sceneTrigger.spawnPoint.position);
                Instantiate(playerCharacterPrefab, sceneTrigger.spawnPoint.position, sceneTrigger.spawnPoint.rotation, null);
                break;
            }
        }
    }

    private void OnLevelWasLoaded()
    {
        SetGameState(GameState.initialize);
    }

    private void Start()
    {
        SetGameState(GameState.initialize);
        Application.targetFrameRate = 60;
    }
}
