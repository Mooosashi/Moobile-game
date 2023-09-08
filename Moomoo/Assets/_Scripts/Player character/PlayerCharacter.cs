using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { moving, still }

public class PlayerCharacter : MonoBehaviour
{
    public GameManager gameManager;

    [Header("References")]
    [SerializeField] private PlayerCharacterMovement playerCharacterMovement;

    private PlayerState currentPlayerState;

    public PlayerState CurrentPlayerState
    {
        get => currentPlayerState;
        
        private set
        {
            switch (value)
            {
                case PlayerState.moving:
                    MovingState();
                    break;
                case PlayerState.still:
                    StillState();
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

    }

    private void StillState()
    {

    }
}
