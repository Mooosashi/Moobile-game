using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerState { moving, still }

public class PlayerCharacter : MonoBehaviour
{
    public GameManager gameManager;
    private PlayerState currentPlayerState;

    [Header("References")]
    [SerializeField] private PlayerCharacterMovement playerCharacterMovement;


    public PlayerState CurrentPlayerState
    {
        get => currentPlayerState;
        
        private set
        {
            currentPlayerState = value;

            switch (value)
            {
                case PlayerState.still:
                    StillState();
                    break;

                case PlayerState.moving:
                    MovingState();
                    break;

                default:
                    break;
            }
        }
    }

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    public void SetPlayerState(PlayerState state)
    {
        CurrentPlayerState = state;
    }

    private void MovingState()
    {
        gameManager.SetDebugPlayerStateText(PlayerState.moving);
    }

    private void StillState()
    {
        gameManager.SetDebugPlayerStateText(PlayerState.still);
    }
}
